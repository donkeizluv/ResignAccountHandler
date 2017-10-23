using MimeKit;
using ResignAccountHandlerUI.Adapter;
using ResignAccountHandlerUI.AdExecutioner;
using ResignAccountHandlerUI.Logic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ResignAccountHandlerUI.Automation
{
    public static class AutomatorFactory
    {
        public static string GetDbPath()
        {
            return $@"{Program.AssemblyDirectory}\db.dat";
        }

        public static ResignAccountHandlerAutomation GetAutomator(dynamic config)
        {
            var automator = new ResignAccountHandlerAutomation()
            {
                SendReport = config.SendReport,
                //SenderEmailSuffix = config.SenderEmailSuffix,
                ResignFolderName = config.ResignFolderName,
                AcceptedSenders = TupleToAddressList(config.AcceptedResignSenders),
                Adapter = new DbAdapter(GetDbPath()), //not configuarable
                EmailHandler = config.EmailHandler,
                Executioner = config.Executioner,
                ReportCC = config.ReportCC,
                ReportReceiver = config.ReportReceiver,
                SendReportRetry = config.SendReportRetry,
                ReadEmailRetry = config.ReadEmailRetry,
            };
            automator.EmailHandler.MoveToProcessedFolder = config.MoveToProcessedFolder;
            automator.EmailHandler.ProcessedFolderName = config.ProcessedFolderName;

            automator.Logic = new BussiessLogic(automator.Adapter, config.DeleteAfter);
            return automator;
        }
        private static List<MailboxAddress> TupleToAddressList(IEnumerable<Tuple<string, string>> acceptedSenders)
        {
            return acceptedSenders.Select(item => new MailboxAddress(item.Item1, item.Item2)).ToList();
        }
        public static ResignAccountHandlerAutomation GetDebugAutomator()
        {
            var acceptedSenders = new List<MailboxAddress>
            {
                new MailboxAddress("luu nhat hong", "luu.nhat-hong@hdsaison.com.vn"),
                new MailboxAddress("vo ya phuong khanh", "vo.phuong-khanh@hdsaison.com.vn"),
            };

            var automator = new ResignAccountHandlerAutomation()
            {
                SendReport = true,
                //SenderEmailSuffix = "@hdsaison.com.vn",
                ResignFolderName = "Test",
                AcceptedSenders = acceptedSenders,
                Adapter = new DbAdapter(GetDbPath()),
                EmailHandler = new EmailHandler("luu.nhat-hong", "Dant@760119", true, "DoneResign"), //internet
                //EmailHandler = new EmailHandler("luu.nhat-hong", "Dant@760119"), //intranet
                Executioner = new MockExecutioner(), //mock
                ReportCC = new string[] { "luu.nhat-hong@hdsaison.com.vn" },
                ReportReceiver = new string[] { "luu.nhat-hong@hdsaison.com.vn" },
                ReadEmailRetry = 5,
                SendReportRetry = 5,
                
            };
            automator.EmailHandler.MoveToProcessedFolder = false;
            automator.Logic = new BussiessLogic(automator.Adapter, 10);
            return automator;
        }
    }
}