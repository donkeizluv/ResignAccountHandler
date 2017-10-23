//using MimeKit;
//using ResignAccountHandlerUI.AdExecutioner;
//using ResignAccountHandlerUI.Logic;
//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace ResignAccountHandlerUI.Automation
//{
//    public class AutomatorConfig
//    {
//        public bool SendReport;
//        //public string SenderEmailSuffix;
//        public string ResignFolderName;
//        public string ProcessedFolderName;
//        public bool MoveToProcessedFolder;
//        public IEnumerable<Tuple<string, string>> AcceptedResignSenders;
//        //set mailbox auto reply
//        public string AutoReplyString { get; set; }
//        public bool SetMailBoxAutoReply { get; set; }
//        //public IDbAdapter Adapter;
//        public IEmailHandler EmailHandler;

//        public IExecutioner Executioner;
//        public string[] ReportCC;
//        public string[] ReportReceiver;
//        public BussiessLogic Logic;
//        public int DeleteAfter;
//        public int ReadEmailRetry;
//        public int SendReportRetry;


//        public AutomatorConfig(Configuration config)
//        {

//        }
//    }
//}
