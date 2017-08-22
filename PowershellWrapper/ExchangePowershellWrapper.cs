using System;
using System.Management.Automation.Runspaces;
using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Text;

namespace Test
{
    public class ExchangePowerShellWrapper
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public ExchangePowerShellWrapper(string username, string pwd)
        {
            Username = username;
            Password = pwd;
        }
        //works!
        public Collection<PSObject> RemoveMailboxAndAd(string adName)
        {
            var runspace = GetExchangeRunspace(Username, Password);
            runspace.Open();
            var pipe = runspace.CreatePipeline();
            var cmd = new Command("Remove-Mailbox");
            cmd.Parameters.Add("Identity", adName);
            cmd.Parameters.Add("Permanent", true);
            cmd.Parameters.Add("Confirm", false);
            pipe.Commands.Add(cmd);
            return pipe.Invoke();
        }
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
