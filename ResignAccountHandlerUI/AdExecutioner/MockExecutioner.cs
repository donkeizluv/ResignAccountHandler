using ResignAccountHandlerUI.AdHelper;
using System;
using System.DirectoryServices;
using ResignAccountHandlerUI.Model;

namespace ResignAccountHandlerUI.AdExecutioner
{
    /// <summary>
    /// mock class
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
            errorMess = string.Empty;
            return true;
        }

        public bool ContainsAutoToken(DirectoryEntry entry)
        {
            return true;
        }

        public string AutoReplyString { get; set; }

        public bool DeleteAccountAndMailbox(Resignation resign, out string errorMess)
        {
            throw new NotImplementedException();
        }
    }
}