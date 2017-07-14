using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ResignAccountHandlerUI.Adapter
{
    public enum DbResult
    {
        Insert,
        Update,
        Delete,
        Erorr,
        Older,
    }

    //Scheduled disable -> disabled -> Scheduled delete(10 days after disabled) -> deleted
    //Scheduled disable -> cancel
    public interface IDbAdapter : IDisposable
    {
        IEnumerable<Resignation> GetAllRecords();

        //void UpdateRecords(IEnumerable<Resignation> info, out List<Resignation> updated, out List<Resignation> addNew, out List<Resignation> fail);
        DbResult UpsertRecordIfNewer(Resignation resign, out string error);

        bool UpdateRecord(Resignation resign, out string error);

        bool InsertRecord(Resignation resign, out string error);

        bool DeleteRecord(Resignation resign, out string error);

        IEnumerable<Resignation> GetRecords(RecordStatus status);

        bool FindByAdAndHr(string ad, out IEnumerable<Resignation> resigns);

        //bool FindDuplicateAdAndHr(string ad, out IEnumerable<Resignation> resigns);

        //IEnumerable<Resignation> GetRecords(DateTime beforeThisDay);
    }

    public class DbAdapter : IDbAdapter
    {
        private const string ResignCollection = "Resign";
        private static LiteDatabase _db;
        public string DbPath { get; private set; }

        public DbAdapter(string dbPath)
        {
            DbPath = dbPath;
            _db = new LiteDatabase(GetConnectionString(dbPath));
        }

        private string GetConnectionString(string path)
        {
            return $"filename={path}; Timeout=10";
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public IEnumerable<Resignation> GetAllRecords()
        {
            return _db.GetCollection<Resignation>(ResignCollection).FindAll();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="status">null: take everything</param>
        /// <returns></returns>
        public IEnumerable<Resignation> GetRecords(RecordStatus status)
        {
            return _db.GetCollection<Resignation>(ResignCollection).Find(Query.EQ("Status", status.ToString()));
        }

        private void AddNew(Resignation resign)
        {
            var col = _db.GetCollection<Resignation>(ResignCollection);
            col.Insert(resign);
            col.EnsureIndex(x => x.ADName);
            col.EnsureIndex(x => x.HRCode);
        }

        private bool Update(Resignation resign)
        {
            var col = _db.GetCollection<Resignation>(ResignCollection);
            return col.Update(resign);
        }

       

        //}
        /// <summary>
        /// Return true = update OK, false = older than stored record, null = db problems
        /// </summary>
        /// <param name="resign"></param>
        /// <returns></returns>
        public DbResult UpsertRecordIfNewer(Resignation resign, out string errorMess)
        {
            errorMess = string.Empty;
            try
            {
                if (FindRecord(resign.ADName, out var found))
                {
                    var newList = found.Where(item => DateTime.Compare(resign.ReceiveDate, item.ReceiveDate) > 0);
                    if (newList.Count() < 1) return DbResult.Older;
                    //update record
                    var readyList = newList.Where(item => item.Status == RecordStatus.Ready);
                    if (readyList.Count() == 0)
                        throw new InvalidOperationException($"non-updatable status AD:{resign.ADName} HR: {resign.HRCode} ");
                    if (readyList.Count() > 1)
                        throw new InvalidOperationException($"Duplicate records with same AD:{resign.ADName} HR: {resign.HRCode}");

                    resign.Id = readyList.First().Id;
                    return Update(resign) ? DbResult.Update : throw new InvalidOperationException("Db error, cant update");
                }
                else //no record found
                {
                    //add new record
                    AddNew(resign);
                    return DbResult.Insert;
                }
            }
            catch (InvalidOperationException ex) //catch more errors
            {
                errorMess = ex.Message;
                return DbResult.Erorr;
            }
        }

        private bool FindRecord(string ad, string hrCode, out IEnumerable<Resignation> resigns)
        {
            resigns = _db.GetCollection<Resignation>(ResignCollection).
                Find(Query.And(Query.EQ("ADName", ad), Query.EQ("HRCode", hrCode)));
            if (resigns.Count() == 0) return false;
            return true;
        }
        private bool FindRecord(string ad, out IEnumerable<Resignation> resigns)
        {
            resigns = _db.GetCollection<Resignation>(ResignCollection).
                Find(Query.EQ("ADName", ad));
            if (resigns.Count() == 0) return false;
            return true;
        }

        /// <summary>
        /// Return true = update OK, false = db problems
        /// </summary>
        /// <param name="resign"></param>
        /// <returns></returns>
        public bool UpdateRecord(Resignation resign, out string error)
        {
            if (Update(resign))
            {
                error = string.Empty;
                return true;
            }
            error = $"Update fail: cant find indeX:{resign.Id}";
            return false;
        }

        public bool InsertRecord(Resignation resign, out string error)
        {
            error = string.Empty;
            try
            {
                AddNew(resign);
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        public bool DeleteRecord(Resignation resign, out string error)
        {
            if (_db.GetCollection<Resignation>(ResignCollection).Delete(resign.Id))
            {
                error = string.Empty;
                return true;
            }
            error = $"Delete fail: cant find indeX:{resign.Id}";
            return false;
        }

        public bool FindByAdAndHr(string ad, out IEnumerable<Resignation> resigns)
        {
            return FindRecord(ad, out resigns);
        }
    }
}