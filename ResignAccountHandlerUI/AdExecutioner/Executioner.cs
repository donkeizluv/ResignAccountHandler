using ResignAccountHandlerUI.AdHelper;
using System;
using System.Collections.ObjectModel;
using System.DirectoryServices;
using System.Management.Automation;
using ResignAccountHandlerUI.Automation;
using ResignAccountHandlerUI.Log;
using ResignAccountHandlerUI.Logger;
using ResignAccountHandlerUI.Model;
using System.Management.Automation.Runspaces;
using System.Collections.Generic;

namespace ResignAccountHandlerUI.AdExecutioner
{
   

    public class Executioner : IExecutioner
    {
        private ExchangePowershellWrapper _psWrapper;
        private string _autoReplyString = string.Empty;
        public string AutoReplyString
        {
            get { return _autoReplyString; }
            set { _autoReplyString = value; }
        }
        public Dictionary<string, string> ManagerDictionary { get; set; }
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

        public const string AutoToken = "[auto_resign_token]"; //auto token
        private const string ContactToken = "{Contact}";

        public AdController Ad { get; set; }
        public string AutoReplyStringWithContact { get; set; }

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

        public Executioner()
        {
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

            //Exchange auto reply, protocols
            if (!ExecuteWrapper(_psWrapper.SetMailProtocols(resign.ADName, false), out var autoRepEx))
            {
                _logger.Log("DisableMailProtocols failed.");
                _logger.Log(autoRepEx);
            }
            //auto reply
            string autoReply = ComposeAutoReplyString(resign);
            if(!string.IsNullOrEmpty(autoReply))
            {
                if (!ExecuteWrapper(_psWrapper.GetAutoReplyPipe_V1(resign.ADName, autoReply), out var limitEx))
                {
                    _logger.Log("SetAutoReply failed.");
                    _logger.Log(limitEx);
                }
            }
            return Ad.DisableUserAccount(entry, out errorMess);
        }
        //merge this into 1 method
        private bool ExecuteWrapper(Pipeline pipe, out Exception ex)
        {
            ex = null;
            try
            {
                var results = pipe.Invoke();
                if (pipe.Error.Count > 0) //check if executed OK
                {
                    if (pipe.Error.Read() is Collection<ErrorRecord> error)
                    {
                        foreach (var er in error)
                        {
                            _logger.Log($"[PowerShell Error]: {er.Exception.Message}");
                        }
                        return false;
                    }
                }
                //OK
                return true;
            }
            catch (Exception e)
            {
                ex = e;
                return false;
            }
            finally
            {
                CleanUpPipe(pipe);
            }
        }
        private string ComposeAutoReplyString(Resignation resign)
        {
            if (ManagerDictionary == null || string.IsNullOrEmpty(resign.Manager))
            {
                _logger.Log($"Resign index: {resign.Id} doesnt have Contact info -> Use default reply");
                return AutoReplyString;
            }
            if(ManagerDictionary.ContainsKey(resign.Manager.ToLower()))
            {
                return AutoReplyStringWithContact.Replace(ContactToken, ManagerDictionary[resign.Manager.ToLower()]);
            }
            _logger.Log($"Manager: {resign.Manager} doesnt have contact info -> Use default reply");
            return AutoReplyString;
        }
        private void CleanUpPipe(Pipeline pipe)
        {
            try
            {
                pipe?.Runspace.Close();
            }
            catch (Exception) { }
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