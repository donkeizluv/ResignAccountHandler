using ResignAccountHandlerUI.AdHelper;
using ResignAccountHandlerUI.Model;
using System.Collections.Generic;
using System.DirectoryServices;

namespace ResignAccountHandlerUI.AdExecutioner
{
    public interface IExecutioner
    {
        bool DisableAccount(Resignation resign, out string errorMess);

        bool DeleteAccountAndMailbox(Resignation resign, out string errorMess);

        bool DeleteAccount(Resignation resign, out string errorMess);

        bool ContainsAutoToken(DirectoryEntry entry);

        string AutoReplyString { get; set; }

        /// <summary>
        /// token name: {Contact}
        /// </summary>
        string AutoReplyStringWithContact { get; set; }

        /// <summary>
        /// key is normalized to Lower
        /// </summary>
        /// should provide methods to intereact instead of exposing Dict
        Dictionary<string, string> ManagerDictionary { get; set; }

        bool SetMailBoxAutoReply { get; set; }

        AdController Ad { get; set; }
    }
}
