using ResignAccountHandlerUI.Adapter;
using System;
using System.Collections.Generic;
using System.Linq;
using ResignAccountHandlerUI.Model;
using ResignAccountHandlerUI.ResignExtractor;

namespace ResignAccountHandlerUI.Logic
{
    /// <summary>
    /// contains logic for how to delete, disable
    /// </summary>
    public class BussiessLogic
    {
        public IDbAdapter Adapter { get; set; }
        public int DeleteAccountAfter { get; set; }

        //fail safe list for ignoring deletion of just added cases
        public List<Resignation> IgnoreList { get; set; }
        public bool DeleteFailSafe { get; set; }

        public BussiessLogic()
        {
            IgnoreList = new List<Resignation>();
            DeleteFailSafe = true;
        }

        public BussiessLogic(IDbAdapter adapter, int deleteAfter) : this()
        {
            DeleteAccountAfter = deleteAfter;
            Adapter = adapter;
        }
        /// <summary>
        /// returns list of met delete conditions
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Resignation> GetTodayDeletes()
        {
            return from rec in Adapter.GetRecords(RecordStatus.Disabled)
                   where (DateTime.Today - rec.ResignDay).TotalDays >= DeleteAccountAfter && 
                         (DeleteFailSafe && IgnoreList.FirstOrDefault(r => string.Compare(r.ADName, rec.ADName, true) == 0) == null)
                   select rec;
        }

        /// <summary>
        /// returns list of met disable conditions
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Resignation> GetTodayDisables()
        {
            var readySet = Adapter.GetRecords(RecordStatus.Ready).ToList();
            readySet.AddRange(Adapter.GetRecords(RecordStatus.OnlyDisable));
            return from rec in readySet
                   where (DateTime.Today - rec.ResignDay).TotalDays > -1
                   select rec;
        }
    }
}