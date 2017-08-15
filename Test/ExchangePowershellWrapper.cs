using System;
using System.Management.Automation.Runspaces;
using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Text;

namespace Test
{
    public class ExchangePowershellWrapper
    {
        //works!
        public Collection<PSObject> RemoveMailboxAndAd(string adName)
        {
            var runspace = GetExchangeRunspace();
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
        public Collection<PSObject> SetAutoReply(string alias, string content)
        {
            var runspace = GetExchangeRunspace();
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
            //var cmd = new Command("Set-MailboxAutoReplyConfiguration");
            //cmd.Parameters.Add("<alias>", alias); //how?
            //cmd.Parameters.Add("AutoReplyState", "enabled");
            //cmd.Parameters.Add("ExternalAudience", "all");
            //cmd.Parameters.Add("InternalMessage", content);
            //cmd.Parameters.Add("ExternalMessage", content);
            pipe.Commands.Add(cmd);
            return pipe.Invoke();
        }

        private static Runspace GetExchangeRunspace()
        {
            var info = new WSManConnectionInfo(new Uri("http://PRD-VN-MAIL05.sgvf.sgcf/PowerShell"),
                "http://schemas.microsoft.com/powershell/Microsoft.Exchange",
                (PSCredential)null)
            {
                OperationTimeout = 30 * 1000, // 30s
                OpenTimeout = 1 * 60 * 1000 // 30s
            };
            return RunspaceFactory.CreateRunspace(info);
        }
    }
}
