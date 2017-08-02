using ResignAccountHandlerUI.Adapter;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ResignAccountHandlerUI.Logic
{
    /// <summary>
    /// contains logic for how to delete, disable
    /// </summary>
    public class BussiessLogic
    {
        public IDbAdapter Adapter { get; set; }
        public int DeleteAccountAfter { get; set; }

        public BussiessLogic(IDbAdapter adapter, int deleteAfter)
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
                   where (DateTime.Today - rec.ResignDay).TotalDays >= DeleteAccountAfter
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