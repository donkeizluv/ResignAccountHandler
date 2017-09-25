using System;
using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text;

namespace ResignAccountHandlerUI.AdExecutioner
{
    public class ExchangePowershellWrapper
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public ExchangePowershellWrapper(string username, string pwd)
        {
            Username = username;
            Password = pwd;
        }
        //works!
        //public Collection<PSObject> RemoveMailboxAndAd(string adName)
        //{
        //    var runspace = GetExchangeRunspace(Username, Password);
        //    runspace.Open();
        //    var pipe = runspace.CreatePipeline();
        //    var cmd = new Command("Remove-Mailbox");
        //    cmd.Parameters.Add("Identity", adName);
        //    cmd.Parameters.Add("Permanent", true);
        //    cmd.Parameters.Add("Confirm", false);
        //    pipe.Commands.Add(cmd);
        //    return pipe.Invoke();
        //}
        //works!
        public Pipeline GetAutoReplyPipe_V1(string alias, string content)
        {
            var runspace = GetExchangeRunspace(Username, Password);
            runspace.Open();
            var pipe = runspace.CreatePipeline();
            var builder = new StringBuilder();
            builder.Append("Set-MailboxAutoReplyConfiguration ");
            builder.Append(alias);
            builder.Append(" -AutoReplyState enabled");
            builder.Append(" -ExternalAudience all");
            builder.Append($" -InternalMessage \"{content}\"");
            builder.Append($" -ExternalMessage \"{content}\"");
            var cmd = new Command(builder.ToString(), true);
            pipe.Commands.Add(cmd);
            return pipe;
        }

        public Pipeline SetRecipientLimits(string identity, int limit)
        {
            var runspace = GetExchangeRunspace(Username, Password);
            runspace.Open();
            var pipe = runspace.CreatePipeline();
            var cmd = new Command("Set-Mailbox");
            cmd.Parameters.Add("Identity", identity);
            cmd.Parameters.Add("RecipientLimits", limit);
            pipe.Commands.Add(cmd);
            return pipe;
        }

        public Pipeline SetMailProtocols(string identity, bool enable)
        {
            var runspace = GetExchangeRunspace(Username, Password);
            runspace.Open();
            var pipe = runspace.CreatePipeline();
            var cmd = new Command("Set-CASMailbox");
            cmd.Parameters.Add("Identity", identity);
            cmd.Parameters.Add("OWAEnabled", enable);
            cmd.Parameters.Add("ActiveSyncEnabled", enable);
            cmd.Parameters.Add("PopEnabled", enable);
            cmd.Parameters.Add("ImapEnabled", enable);
            cmd.Parameters.Add("MapiEnabled", enable);
            pipe.Commands.Add(cmd);
            return pipe;
        }
        //doesnt work
        public PowerShell GetAutoReplyPipe_V2(string alias, string content)
        {
            var psExec = PowerShell.Create();
            psExec.Runspace = GetExchangeRunspace(Username, Password);
            var builder = new StringBuilder();
            builder.Append("Set-MailboxAutoReplyConfiguration ");
            builder.Append(alias);
            builder.Append(" -AutoReplyState enabled");
            builder.Append(" -ExternalAudience all");
            builder.Append($" -InternalMessage \"{content}\"");
            builder.Append($" -ExternalMessage \"{content}\"");
            psExec.AddScript(builder.ToString()); //reads all the lines in the powershell script
            return psExec;
        }

        private static Runspace GetExchangeRunspace(string username, string pwd)
        {
            var info = new WSManConnectionInfo(new Uri("http://PRD-VN-MAIL05.sgvf.sgcf/PowerShell"),
                "http://schemas.microsoft.com/powershell/Microsoft.Exchange",
                new PSCredential(username, pwd.ToSecureString()))
            {
                OperationTimeout = 30 * 1000,
                OpenTimeout = 1 * 60 * 1000,
                SkipCACheck = true,
                SkipCNCheck = true
            };

            return RunspaceFactory.CreateRunspace(info);
        }

    }
}
