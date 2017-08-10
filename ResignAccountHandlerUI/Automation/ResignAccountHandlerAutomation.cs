using MimeKit;
using ResignAccountHandlerUI.Adapter;
using ResignAccountHandlerUI.AdExecutioner;
using ResignAccountHandlerUI.Exceptions;
using ResignAccountHandlerUI.Log;
using ResignAccountHandlerUI.UIController;
using ResignAccountHandlerUI.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Polly;
using System.Net.Sockets;
using MailKit.Security;

namespace ResignAccountHandlerUI.Automation
{
    public interface IResignAccountHanlderAutomation : IDisposable
    {
        //require to run
        bool SendReport { get; set; }

        bool IsCompleted { get; }

        //string Username { get; set; } //authenticate to receive/send email
        //string Password { get; set; }
        string SenderEmailSuffix { get; set; }

        string ResignFolderName { get; set; }

        //string ProcessedFolderName { get; set; }
        //bool MoveToProcessedFolder { get; set; }

        int ReadEmailRetry { get; set; }
        int SendReportRetry { get; set; }

        IEnumerable<MailboxAddress> AcceptedSenders { get; set; }
        IEnumerable<string> ReportReceiver { get; set; }
        IEnumerable<string> ReportCC { get; set; }
        IExecutioner Executioner { get; set; }
        IDbAdapter Adapter { get; set; }
        IEmailHandler EmailHandler { get; set; }
        BussiessLogic Logic { get; set; }

        //other prop
        List<List<string>> UpdateResults { get; }

        List<List<string>> DisablesResults { get; }
        List<List<string>> DeleteResults { get; }

        int DisableAccounts();

        //void EmailDisableAccountsReport(); //should not expose this
        int DeleteAccounts();

        void DoUpdate(IEnumerable<MimeMessage> mailList);

        void Run();

        void EmailReport();

        //void ClearActionResults();
    }

    /// <summary>
    /// Automation is meant to create and run once!
    /// </summary>
    public class ResignAccountHandlerAutomation : IResignAccountHanlderAutomation
    {
        private const string DateStringFormat = "dd/MM/yyyy hh:mm:ss tt";
        //interface implementation
        //require to run
        private ILogger _logger = LogManager.GetLogger(typeof(ResignAccountHandlerAutomation));
        public bool SendReport { get; set; }

        public bool IsCompleted { get; private set; } //if true can no longer run -> create new automator

        //public string Username { get; set; } //authenticate to receive/send email
        //public string Password { get; set; }
        public string SenderEmailSuffix { get; set; }

        public string ResignFolderName { get; set; }
        //public string ProcessedFolderName { get; set; }
        //public bool MoveToProcessedFolder { get; set; }
        public IEnumerable<MailboxAddress> AcceptedSenders { get; set; }
        public IEnumerable<string> ReportReceiver { get; set; }
        public IEnumerable<string> ReportCC { get; set; }
        public IExecutioner Executioner { get; set; }
        public IDbAdapter Adapter { get; set; }
        public IEmailHandler EmailHandler { get; set; }
        public BussiessLogic Logic { get; set; }

        //self prop
        //for report purposes
        //last column must be error Code
        public List<List<string>> UpdateResults { get; private set; }

        public List<List<string>> DisablesResults { get; private set; }
        public List<List<string>> DeleteResults { get; private set; }
        public int ReadEmailRetry { get; set; }
        public int SendReportRetry { get; set; }
        private int _readEmailTries = 1;
        private int _sendReportTries = 1;
        private const int Wait = 3;
        //better use factory to create this
        public ResignAccountHandlerAutomation()
        {
            UpdateResults = new List<List<string>>();
            DisablesResults = new List<List<string>>();
            DeleteResults = new List<List<string>>();
        }

        public ResignAccountHandlerAutomation(IExecutioner exe, IDbAdapter adapter, IEmailHandler mailHanler) : this()
        {
            Executioner = exe;
            Adapter = adapter;
            EmailHandler = mailHanler;
        }

        public int DisableAccounts()
        {
            var disableList = Logic.GetTodayDisables().ToList();

            _logger.Log($"Disable - total: {disableList.Count()}");
            int count = 1;
            foreach (var resign in disableList)
            {
                if (Executioner.DisableAccount(resign, out string erorr))
                {
                    resign.Status = RecordStatus.Disabled;
                    DisablesResults.Add(MakeRow(resign.Id.ToString(),
                        resign.ADName, resign.HRCode, 
                        resign.ReceiveDate.ToString(DateStringFormat), 
                        resign.ResignDay.ToString(DateStringFormat),
                        erorr, 
                        Code.I.ToString()));
                    if (!Adapter.UpdateRecord(resign, out var dbError))
                        throw new DbException(dbError);
                    _logger.Log($"Disable - OK: {resign.ADName}");
                }
                else
                {
                    resign.Status = RecordStatus.Erorr;
                    DisablesResults.Add(MakeRow(resign.Id.ToString(),
                        resign.ADName, 
                        resign.HRCode, 
                        resign.ReceiveDate.ToString(DateStringFormat), 
                        resign.ResignDay.ToString(DateStringFormat), 
                        erorr, Code.E.ToString()));
                    resign.AppendErrorMessage(erorr);
                    if (!Adapter.UpdateRecord(resign, out var dbError))
                        throw new DbException(dbError); //unlikely to happen
                    _logger.Log($"Disable - Fail: {resign.ADName} reason: {erorr}");
                }
                count++;
            }
            DisablesResults.Sort((item1, item2) => (item1.Last().CompareTo(item2.Last())));
            return count;
        }

        public int DeleteAccounts()
        {
            var deleteList = Logic.GetTodayDeletes().ToList();
            _logger.Log($"Delete - total: {deleteList.Count()}");
            int count = 1;
            foreach (var resign in deleteList)
            {
                if (Executioner.DeleteAccount(resign, out string erorr))
                {
                    resign.Status = RecordStatus.Deleted;
                    DeleteResults.Add(MakeRow(resign.Id.ToString(),
                        resign.ADName, resign.HRCode, resign.ReceiveDate.ToString(DateStringFormat), 
                        resign.ResignDay.ToString(DateStringFormat), erorr, Code.I.ToString()));
                    if (!Adapter.UpdateRecord(resign, out var dbError)) //should not happen at all
                        throw new DbException(dbError);
                    _logger.Log($"Delete - OK: {resign.ADName}");
                }
                else
                {
                    resign.Status = RecordStatus.Erorr;
                    DeleteResults.Add(MakeRow(resign.Id.ToString(),
                        resign.ADName, 
                        resign.HRCode,
                        resign.ReceiveDate.ToString(DateStringFormat),
                        resign.ResignDay.ToString(DateStringFormat),
                        erorr, 
                        Code.E.ToString()));
                    resign.AppendErrorMessage(erorr);
                    if (!Adapter.UpdateRecord(resign, out var dbError))
                        throw new DbException(dbError); //unlikely to happen
                    _logger.Log($"Delete - Fail: {resign.ADName} reason: {erorr}");
                }
                count++;
            }
            DeleteResults.Sort((item1, item2) => (item1.Last().CompareTo(item2.Last())));
            return count;
        }

        private List<string> MakeRow(params string[] rowContent)
        {
            return new List<string>(rowContent);
        }

        public void DoUpdate(IEnumerable<MimeMessage> emailList)
        {
            var extractor = new ResignInfoExtractor();
            _logger.Log($"Parsing - total emails: {emailList.Count()}");
            foreach (var email in emailList)
            {
                var extractResult = extractor.ExtractResignForm(email.HtmlBody, email.Date.DateTime, out var resign, out var errorMess);
                if (extractResult == ParseResult.Parsed_Info_Error)
                {
                    //error
                    _logger.Log($"Parsing - {email.Subject}: form error -> {errorMess}");
                    UpdateResults.Add(MakeRow(email.Subject, email.Date.DateTime.ToString(DateStringFormat), errorMess, Code.E.ToString()));
                    continue;
                }
                if (extractResult == ParseResult.OK)
                {
                    var dbResult = Adapter.UpsertRecordIfNewer(resign, out var dbError);

                    switch (dbResult)
                    {
                        case DbResult.Insert:
                            UpdateResults.Add(MakeRow(email.Subject, 
                                email.Date.DateTime.ToString(DateStringFormat),
                                resign.ResignDay.ToString(DateStringFormat),
                                dbResult.ToString(), 
                                Code.I.ToString()));
                            break;

                        case DbResult.Update: //update what?
                            UpdateResults.Add(MakeRow(email.Subject, 
                                email.Date.DateTime.ToString(DateStringFormat),
                                resign.ResignDay.ToString(DateStringFormat),
                                dbResult.ToString(), 
                                Code.I.ToString()));
                            break;

                        case DbResult.Older:
                            UpdateResults.Add(MakeRow(email.Subject, 
                                email.Date.DateTime.ToString(DateStringFormat), 
                                string.Empty,
                                dbResult.ToString(), 
                                Code.I.ToString()));
                            break;

                        case DbResult.Erorr:
                            UpdateResults.Add(MakeRow(email.Subject, 
                                email.Date.DateTime.ToString(DateStringFormat), 
                                string.Empty,
                                dbError, 
                                Code.E.ToString()));
                            break;

                        default:
                            throw new InvalidProgramException();
                    }
                    _logger.Log($"Parsing - {email.Subject}: OK -> DB: {dbError}");
                }
                if (extractResult == ParseResult.Not_Resign_Email)
                {
                    _logger.Log($"Parsing - {email.Subject}: probly not resign email");
                    UpdateResults.Add(MakeRow(email.Subject,
                        email.Date.DateTime.ToString(DateStringFormat), 
                        string.Empty,
                        errorMess,
                        Code.I.ToString()));
                }
            }
            //sort base on error mess
            UpdateResults.Sort((item1, item2) => (item1.Last().CompareTo(item2.Last())));
        }

        //NYI: retry on reading, send fail
        public void Run()
        {
            if (IsCompleted) throw new InvalidOperationException("Automator already run, create new Automator");
            _logger.Log($"Begin reading folder: {ResignFolderName}");
            var readEmailPol = Policy.
                Handle<SocketException>().
                WaitAndRetry(ReadEmailRetry, count =>
                {
                    _logger.Log($"Read form failed -> wait {Wait}s then retry, retry left: {SendReportRetry - _readEmailTries}");
                    return TimeSpan.FromSeconds(Wait);
                }, (ex, span) =>
                {
                    if (_readEmailTries > ReadEmailRetry)
                    {
                        throw ex;
                    }
                    _logger.Log($"Retry...");
                    _readEmailTries++;
                });
            var emailList = readEmailPol.Execute(() => EmailHandler.GetImapEmail(ResignFolderName, AcceptedSenders));
            DoUpdate(emailList);
            DisableAccounts();
            DeleteAccounts();
            if (SendReport)
            {
                _logger.Log("Sending report...");
                //so fucling socket ex is mutual
                //but then there is Auth ex when cred is perfecly fine, WTF?
                var sendReportPol = Policy.Handle<SocketException>().
                    Or<AuthenticationException>().
                    WaitAndRetry(SendReportRetry, count =>
                    {
                        _logger.Log($"Send report failed -> wait {Wait}s then retry, retry left: {SendReportRetry - _sendReportTries}");
                        return TimeSpan.FromSeconds(Wait);
                    }, (ex, span) =>
                    {
                        if (_sendReportTries > SendReportRetry)
                        {
                            throw ex;
                        }
                        _logger.Log($"Retry...");
                        _sendReportTries++;
                    });
                sendReportPol.Execute(EmailReport);
            }
            IsCompleted = true;
        }

        /// <summary>
        /// no real need to call this cuz program will auto terminate
        /// </summary>
        //public void ClearActionResults()
        //{
        //    UpdateResults.Clear();
        //    DisablesResults.Clear();
        //    DeleteResults.Clear();
        //}

        private static readonly string ReportSubject = "Resign Handler Report {0}";
        public void EmailReport()
        {
            EmailHandler.SendEmail(ToAddress(ReportReceiver),
                ToAddress(ReportCC), ReportComposer.MakeReportBody(UpdateResults, DisablesResults, DeleteResults),
                string.Format(ReportSubject, DateTime.Today.ToString("dd/MM/yyyy")));
        }
        private IEnumerable<MailboxAddress> ToAddress(IEnumerable<string> addresses)
        {
            var enumerable = addresses as string[] ?? addresses.ToArray();
            if (addresses == null || !enumerable.Any()) return null;
            return enumerable.Select(a => new MailboxAddress(a.Trim()));
        }
        public void Dispose()
        {
            Adapter.Dispose();
        }
    }
}