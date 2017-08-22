using ResignAccountHandlerUI.AdHelper;
using System;
using System.DirectoryServices;
using ResignAccountHandlerUI.Automation;
using ResignAccountHandlerUI.Log;
using ResignAccountHandlerUI.Logger;
using ResignAccountHandlerUI.Model;

namespace ResignAccountHandlerUI.AdExecutioner
{
    public interface IExecutioner
    {
        bool DisableAccount(Resignation resign, out string errorMess);

        bool DeleteAccountAndMailbox(Resignation resign, out string errorMess);

        bool DeleteAccount(Resignation resign, out string errorMess);

        bool ContainsAutoToken(DirectoryEntry entry);

        string AutoReplyString { get; set; }

        bool SetMailBoxAutoReply { get; set; }

        AdController Ad { get; set; }
    }

    public class Executioner : IExecutioner
    {
        private ExchangePowershellWrapper _psWrapper;
        private string _autoReplyString = string.Empty;
        public string AutoReplyString
        {
            get { return _autoReplyString; }
            set { _autoReplyString = value; }
        }

        public string Username { get; set; }
        public string Password { get; set; }
        private bool _setMailBoxAutoReply;
        public bool SetMailBoxAutoReply
        {
            get => _setMailBoxAutoReply;
            set
            {
                _setMailBoxAutoReply = value;
                if(_setMailBoxAutoReply)
                {
                    _psWrapper = new ExchangePowershellWrapper(Username, Password);
                }
            }
        }

        public const string AutoToken = "[auto_resign_token]"; //mark for delete, if account is not mark then -> token erorr
        public AdController Ad { get; set; }
        private ILogger _logger = LogManager.GetLogger(typeof(ResignAccountHandlerAutomation));

        public Executioner(string adm, string pwd)
        {
            try
            {
                //login
                Ad = new AdController(adm, pwd);
                Username = adm;
                Password = pwd;
            }
            catch (UnauthorizedAccessException)
            {
                throw;
            }
        }

        private DirectoryEntry GetEntry(string ad)
        {
            return Ad.GetUser(HandleAtSign(ad));
        }

        //check for disable & token b4 delete
        public bool DeleteAccount(Resignation resign, out string errorMess)
        {
            var entry = GetEntry(resign.ADName);
            if (entry == null)
            {
                errorMess = $"Cant find: {resign.ADName}";
                return false;
            }
            if (Ad.IsAccountActive(resign.ADName) || !ContainsAutoToken(entry))
            {
                errorMess = $"ad: {resign.ADName} is not De-activated or does NOT have AutoToken";
                return false;
            }
            return Ad.DeleteUser2(entry, out errorMess);
        }

        //disable & put token to description
        public bool DisableAccount(Resignation resign, out string errorMess)
        {
            var entry = GetEntry(resign.ADName);
            if (entry == null)
            {
                errorMess = $"Cant find: {resign.ADName}";
                return false;
            }
            //put info, token to description
            string description = Ad.GetProperty(entry, "description");
            Ad.SetProperty(entry, "description", string.Format("{0} {1} disable date: {2}", 
                description, AutoToken, 
                resign.ResignDay.ToShortDateString()),
                out string setDescriptionError);
            //set auto reply
            if(!SetAutoReply(resign.ADName, out var ex))
            {
                _logger.Log("Set mail box auto reply failed.");
                _logger.Log(ex);
            }
            return Ad.DisableUserAccount(entry, out errorMess);
        }
        private bool SetAutoReply(string alias, out Exception ex)
        {
            try
            {
                _psWrapper.GetAutoReplyPipe_V1(alias, AutoReplyString);
                ex = null;
                return true;
            }
            catch (Exception e)
            {
                ex = e;
                return false;
            }
        }
        public bool DeleteAccountAndMailbox(Resignation resign, out string errorMess)
        {
            throw new NotImplementedException();
        }
        public bool ContainsAutoToken(DirectoryEntry entry)
        {
            var description = Ad.GetProperty(entry, "description");
            return description.Contains(AutoToken);
        }
        private static string HandleAtSign(string email)
        {
            if (!email.Contains("@")) return email.Trim();
            var splited = email.Trim().Split('@');
            return splited[0];
        }
    }
}