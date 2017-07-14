using System;
using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Management.Automation.Runspaces;

namespace ResignAccountHandlerUI.AdExecutioner
{
    public static class ExchangePowershellWrapper
    {
        public static Collection<PSObject> RemoveMailboxAndAd(string adName)
        {
            //to use default cre
            var psCredential = (PSCredential)null;
            var connectionInfo = new WSManConnectionInfo(new Uri("http://PRD-VN-MAIL05.sgvf.sgcf/PowerShell"), "http://schemas.microsoft.com/powershell/Microsoft.Exchange", psCredential)
            {
                OperationTimeout = 30 * 1000, // 30s
                OpenTimeout = 1 * 60 * 1000 // 30s
            };
            var runspace = RunspaceFactory.CreateRunspace(connectionInfo);
            runspace.Open();

            var pipe = runspace.CreatePipeline();
            var cmd = new Command("Remove-Mailbox");
            cmd.Parameters.Add("Identity", adName);
            //cmd.Parameters.Add("Permanent", true);
            cmd.Parameters.Add("Confirm", false);
            pipe.Commands.Add(cmd);
            return pipe.Invoke(); //this fucking command actually doesnt return shit
        }
    }
}
