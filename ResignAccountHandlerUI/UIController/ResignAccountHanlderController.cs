using ResignAccountHandlerCore;
using ResignAccountHandlerUI.Adapter;
using ResignAccountHandlerUI.AdExecutioner;
using ResignAccountHandlerUI.Exceptions;
using ResignAccountHandlerUI.Logic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ResignAccountHandlerUI.Model;

namespace ResignAccountHandlerUI.UIController
{
    public enum Code
    {
        I, //infomation
        E, //error
    }

    public enum DbAction
    {
        Update,
        Delete,
        Insert
    }

    public struct Command
    {
        public DbAction ActionType;
        public Resignation Resign;
    }

    public interface IResignAccountHanlderViewer
    {
        IResignAccountHanlderController Controller { get; set; }
        DataGridView DataGridView_Update { get; }
        DataGridView DataGridView_Disable { get; }
        DataGridView DataGridView_Delete { get; }
        DataGridView DataGridView_Query { get; }

        DataGridViewRow AddRecordToUpdateGrid(string subject, DateTime receive, string result, Code c, Color? color = null);

        DataGridViewRow AddToDisableGrid(Resignation resign, string result = null, Color? bgColor = null);

        DataGridViewRow AddToDeleteGrid(Resignation resign, string result = null, Color? bgColor = null);

        DataGridViewRow AddToQueryGrid(Resignation resign, string result = null, Color? bgColor = null);

        DataGridViewRow AddToCommitLog(Resignation resign, string commitResult = null, Color? bgColor = null);
    }

    public interface IResignAccountHanlderController
    {
        List<Resignation> DisableList { get; }
        List<Resignation> DeleteList { get; }
        List<Command> CommandList { get; }

        IResignAccountHanlderViewer Viewer { get; set; }
        IExecutioner Executioner { get; set; }
        IDbAdapter Adapter { get; set; }
        BussiessLogic Logic { get; set; }

        void DoUpdate();

        int DisplayTodayDisables();

        int DisplayTodayDeletes();

        int DisplayQuery(RecordStatus status);

        int DisplayQuery(string ad);

        int DisableAccounts();

        int DeleteAccounts();

        int CommitChanges();

        void ClearDataGrid(DataGridView gridView);

        //bool CheckDuplicate(string ad, out IEnumerable<Resignation> resigns);

        bool Find(string ad, out IEnumerable<Resignation> resigns);
    }

    public class ResignAccountHanlderController : IResignAccountHanlderController, IDisposable
    {
        public Color GetAttentionColor { get; set; } = Color.LightSalmon;
        public Color InfoColor { get; set; } = Color.LightYellow;
        public Color OkColor { get; set; } = Color.White;
        public Color ActionOkColor { get; set; } = Color.LightGreen;
        public Color ActionFailColor { get; set; } = Color.LightSalmon;

        private const string AddToDbFail = "Db error: ";
        private const string AddToDbSuccessful = "Add new record to db.";
        private const string UpdateToDbSuccessful = "Updated to db.";
        private const string AddToDbOlderResignInfo = "Skip -> Older than stored record.";

        public ResignAccountHanlderController(IResignAccountHanlderViewer viewer, IExecutioner executor, IDbAdapter adapter)
        {
            Viewer = viewer;
            Executioner = executor;
            Adapter = adapter;
            Logic = new BussiessLogic(Adapter, ResignAccountHandlerUI.Properties.Settings.Default.DeleteAfter);
        }

        public IResignAccountHanlderViewer Viewer { get; set; }
        public IExecutioner Executioner { get; set; }
        public IDbAdapter Adapter { get; set; }
        public BussiessLogic Logic { get; set; }

        public List<Resignation> DisableList { get; private set; }

        public List<Resignation> DeleteList { get; private set; }

        public List<Command> CommandList { get; } = new List<Command>();

        public void ClearDataGrid(DataGridView gridView)
        {
            gridView.Rows.Clear();
        }

        public int DeleteAccounts()
        {
            ClearDataGrid(Viewer.DataGridView_Delete);
            int count = 0;
            foreach (var resign in DeleteList)
            {
                if (Executioner.DeleteAccount(resign, out string erorr))
                {
                    resign.Status = RecordStatus.Deleted;
                    Viewer.AddToDeleteGrid(resign, erorr, ActionOkColor);
                    if (!Adapter.UpdateRecord(resign, out var error))
                        throw new DbException(error);
                    count++;
                }
                else
                {
                    resign.Status = RecordStatus.Erorr;
                    Viewer.AddToDeleteGrid(resign, erorr, ActionFailColor);
                    resign.AppendErrorMessage(erorr);
                    if (!Adapter.UpdateRecord(resign, out var error))
                        throw new DbException(error);
                }
            }
            DeleteList.Clear();
            return count;
        }

        public int DisableAccounts()
        {
            ClearDataGrid(Viewer.DataGridView_Disable);
            int count = 0;
            foreach (var resign in DisableList)
            {
                if (Executioner.DisableAccount(resign, out string erorr))
                {
                    resign.Status = RecordStatus.Disabled;
                    Viewer.AddToDisableGrid(resign, erorr, ActionOkColor);
                    if (!Adapter.UpdateRecord(resign, out var error))
                        throw new DbException(error);
                    count++;
                }
                else
                {
                    resign.Status = RecordStatus.Erorr;
                    Viewer.AddToDisableGrid(resign, erorr, ActionFailColor);
                    resign.AppendErrorMessage(erorr);
                    if (!Adapter.UpdateRecord(resign, out var error))
                        throw new DbException(error);
                }
            }
            DisableList.Clear();
            return count;
        }

        public int DisplayTodayDeletes()
        {
            ClearDataGrid(Viewer.DataGridView_Delete);
            DeleteList = Logic.GetTodayDeletes().ToList();
            foreach (var delete in DeleteList)
            {
                Viewer.AddToDeleteGrid(delete);
            }
            return DeleteList.Count();
        }

        public int DisplayTodayDisables()
        {
            ClearDataGrid(Viewer.DataGridView_Disable);
            DisableList = Logic.GetTodayDisables().ToList();
            foreach (var disable in DisableList)
            {
                Viewer.AddToDisableGrid(disable);
            }
            return DisableList.Count();
        }

        public void DoUpdate()
        {
            ClearDataGrid(Viewer.DataGridView_Update);
            if (!TryGetOutlookWrapper(out var ol))
            {
                throw new NullReferenceException("Cant get outlook.");
            }
            var extractor = new ResignInfoExtractor();
            var emailList = ol.GetItemInCurrentSelectedFolder("Vo Ya Phuong Khanh", "Luu Nhat Hong"); //test
            //var resignList = new List<Resignation>();
            //int notResignCount = 0;
            //int resignCount = 0;
            foreach (var mail in emailList)
            {
                //null = cant parse info
                //false = no info table found -> not resign letter
                var result = extractor.ExtractResignForm(mail.HTMLBody, out var resign, out var errorMess);
                if (result == ParseResult.Parsed_Info_Error)
                {
                    //error
                    //notResignCount++;
                    Viewer.AddRecordToUpdateGrid(mail.Subject, mail.ReceivedTime, errorMess, Code.E, GetAttentionColor);
                    continue;
                }
                if (result == ParseResult.OK)
                {
                    //ok
                    //resignList.Add(resign);
                    resign.ReceiveDate = mail.ReceivedTime;
                    var dbResult = Adapter.UpsertRecordIfNewer(resign, out var dbError);

                    switch (dbResult)
                    {
                        case DbResult.Insert:
                            Viewer.AddRecordToUpdateGrid(mail.Subject, mail.ReceivedTime, AddToDbSuccessful, Code.I, InfoColor);
                            break;

                        case DbResult.Update:
                            Viewer.AddRecordToUpdateGrid(mail.Subject, mail.ReceivedTime, UpdateToDbSuccessful, Code.I, InfoColor);
                            break;

                        case DbResult.Older:
                            Viewer.AddRecordToUpdateGrid(mail.Subject, mail.ReceivedTime, AddToDbOlderResignInfo, Code.I, InfoColor);
                            break;

                        case DbResult.Erorr:
                            Viewer.AddRecordToUpdateGrid(mail.Subject, mail.ReceivedTime, AddToDbFail + dbError, Code.E, GetAttentionColor);
                            break;

                        default:
                            throw new InvalidProgramException();
                    }
                    //resignCount++;
                }
                if (result == ParseResult.Not_Resign_Email)
                {
                    //notResignCount++;
                    Viewer.AddRecordToUpdateGrid(mail.Subject, mail.ReceivedTime, errorMess, Code.I, InfoColor);
                    //not resign email
                }
            }
        }

        private bool TryGetOutlookWrapper(out OutlookWrapper wrapper)
        {
            try
            {
                wrapper = new OutlookWrapper();
                return true;
            }
            catch (NullReferenceException)
            {
                wrapper = null;
                return false;
                throw;
            }
        }

        public void Dispose()
        {
            Adapter.Dispose();
        }

        private IEnumerable<Resignation> GetFilterdRecords(RecordStatus status)
        {
            return Adapter.GetRecords(status);
        }
        private IEnumerable<Resignation> GetFilterdRecords(string ad)
        {
            if (ad == string.Empty)
            {
                return Adapter.GetAllRecords();
            }
            return Adapter.GetRecords("ADName", ad);
        }

        public int DisplayQuery(RecordStatus status)
        {
            ClearDataGrid(Viewer.DataGridView_Query);
            var query = GetFilterdRecords(status);
            foreach (var record in query)
            {
                Viewer.AddToQueryGrid(record);
            }
            return query.Count();
        }

        public int DisplayQuery(string ad)
        {
            ClearDataGrid(Viewer.DataGridView_Query);
            var query = GetFilterdRecords(ad);
            foreach (var record in query)
            {
                Viewer.AddToQueryGrid(record);
            }
            return query.Count();
        }

        public bool Find(string ad, out IEnumerable<Resignation> resigns)
        {
            return Adapter.FindByAdAndHr(ad, out resigns);
        }

        //public bool CheckDuplicate(string ad, out IEnumerable<Resignation> dups)
        //{
        //    return Adapter.FindDuplicateAdAndHr(ad, out dups);
        //}

        private const string CommitSuccess = "OK";

        public int CommitChanges()
        {
            if (CommandList.Count < 1) throw new InvalidOperationException("command list is empty");
            int count = 0;
            foreach (var cmd in CommandList)
            {
                switch (cmd.ActionType)
                {
                    case DbAction.Update:
                        count += CommitUpdate(cmd.Resign) ? 1 : 0;
                        break;

                    case DbAction.Delete:
                        count += CommitDelete(cmd.Resign) ? 1 : 0;
                        break;

                    case DbAction.Insert:
                        count += CommitInsert(cmd.Resign) ? 1 : 0;
                        break;

                    default:
                        throw new InvalidProgramException("Action type is not valid.");
                }
            }
            return count;
        }

        private bool CommitUpdate(Resignation resign)
        {
            if (Adapter.UpdateRecord(resign, out var error))
            {
                Viewer.AddToCommitLog(resign, CommitSuccess, ActionOkColor);
                return true;
            }
            else
            {
                Viewer.AddToCommitLog(resign, error, ActionFailColor);
                return false;
            }
        }

        private bool CommitDelete(Resignation resign)
        {
            if (Adapter.DeleteRecord(resign, out var error))
            {
                Viewer.AddToCommitLog(resign, CommitSuccess, ActionOkColor);
                return true;
            }
            else
            {
                Viewer.AddToCommitLog(resign, error, ActionFailColor);
                return false;
            }
        }

        private bool CommitInsert(Resignation resign)
        {
            if (Adapter.InsertRecord(resign, out var error))
            {
                Viewer.AddToCommitLog(resign, CommitSuccess, ActionOkColor);
                return true;
            }
            else
            {
                Viewer.AddToCommitLog(resign, error, ActionFailColor);
                return false;
            }
        }
    }
}