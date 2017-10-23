using ResignAccountHandlerUI.AdHelper;
using System;
using System.DirectoryServices;
using ResignAccountHandlerUI.Model;
using System.Collections.Generic;

namespace ResignAccountHandlerUI.AdExecutioner
{
    /// <summary>
    /// mock executioner
    /// </summary>
    public class MockExecutioner : IExecutioner
    {
        public MockExecutioner()
        {
        }

        public bool SetMailBoxAutoReply { get; set; }
        public AdController Ad { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool DeleteAccount(Resignation resign, out string errorMess)
        {
            errorMess = string.Empty;
            if (!ContainsAutoToken(null))
            {
                errorMess = $"ad: {resign.ADName} is not De-activated or does NOT have AutoToken";
                return false;
            }
            return true;
        }

        public bool DisableAccount(Resignation resign, out string errorMess)
        {
            //test manager dict
            string reply = ComposeAutoReplyString(resign);
            errorMess = string.Empty;
            return true;
        }
        private const string ContactToken = "{Contact}";
        private string ComposeAutoReplyString(Resignation resign)
        {
            if (ManagerDictionary.ContainsKey(resign.Manager.ToLower()))
            {
                return AutoReplyStringWithContact.Replace(ContactToken, ManagerDictionary[resign.Manager.ToLower()]);
            }
            //_logger.Log($"Manager: {resign.Manager} doesnt have contact info -> Use default reply");
            return AutoReplyString;
        }
        public bool ContainsAutoToken(DirectoryEntry entry)
        {
            return true;
        }

        public string AutoReplyString { get; set; }
        public Dictionary<string, string> ManagerDictionary { get; set; }
        public string AutoReplyStringWithContact { get; set; }

        public bool DeleteAccountAndMailbox(Resignation resign, out string errorMess)
        {
            throw new NotImplementedException();
        }
    }
}