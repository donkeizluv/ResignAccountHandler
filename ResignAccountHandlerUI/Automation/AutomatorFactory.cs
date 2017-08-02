using MimeKit;
using ResignAccountHandlerUI.Adapter;
using ResignAccountHandlerUI.AdExecutioner;
using ResignAccountHandlerUI.Logic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ResignAccountHandlerUI.Automation
{
    public class AutomatorConfig
    {
        public bool SendReport;
        public string SenderEmailSuffix;
        public string ResignFolderName;
        public string ProcessedFolderName;
        public bool MoveToProcessedFolder;
        public IEnumerable<Tuple<string, string>> AcceptedResignSenders;

        //public IDbAdapter Adapter;
        public IEmailHandler EmailHandler;

        public IExecutioner Executioner;
        public string[] ReportCC;
        public string[] ReportReceiver;
        public BussiessLogic Logic;
        public int DeleteAfter;

        public IEnumerable<MailboxAddress> GetAcceptedResignSenders()
        {
            return AcceptedResignSenders.Select(item => new MailboxAddress(item.Item1, item.Item2));
        }
    }

    public static class AutomatorFactory
    {
        public static string GetDbPath()
        {
            return $@"{Program.AssemblyDirectory}\db.dat";
        }

        public static ResignAccountHandlerAutomation GetAutomator(AutomatorConfig config)
        {
            var automator = new ResignAccountHandlerAutomation()
            {
                SendReport = config.SendReport,
                SenderEmailSuffix = config.SenderEmailSuffix,
                ResignFolderName = config.ResignFolderName,
                AcceptedSenders = config.GetAcceptedResignSenders(),
                Adapter = new DbAdapter(GetDbPath()), //not configuarable
                EmailHandler = config.EmailHandler,
                Executioner = config.Executioner,
                ReportCC = config.ReportCC,
                ReportReceiver = config.ReportReceiver
            };
            automator.EmailHandler.MoveToProcessedFolder = config.MoveToProcessedFolder;
            automator.EmailHandler.ProcessedFolderName = config.ProcessedFolderName;

            automator.Logic = new BussiessLogic(automator.Adapter, config.DeleteAfter);
            return automator;
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
                SenderEmailSuffix = "@hdsaison.com.vn",
                ResignFolderName = "Test",
                AcceptedSenders = acceptedSenders,
                Adapter = new DbAdapter(GetDbPath()),
                EmailHandler = new EmailHandler("luu.nhat-hong", "Dant@760119", true, "DoneResign"), //internet
                //EmailHandler = new EmailHandler("luu.nhat-hong", "Dant@760119"), //intranet
                Executioner = new MockExecutioner(), //mock
                ReportCC = new string[] { "luu.nhat-hong@hdsaison.com.vn" },
                ReportReceiver = new string[] { "luu.nhat-hong@hdsaison.com.vn" },
            };
            automator.EmailHandler.MoveToProcessedFolder = false;
            automator.Logic = new BussiessLogic(automator.Adapter, 10);
            return automator;
        }
    }
}