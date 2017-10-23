using CsvHelper;
using ResignAccountHandlerUI.AdExecutioner;
using ResignAccountHandlerUI.Automation;
using ResignAccountHandlerUI.Logic;
using SharpConfig;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ResignAccountHandlerConsole
{
    public class AutomatorConfig
    {
        public bool SendReport;
        //public string SenderEmailSuffix;
        public string ResignFolderName;
        public string ProcessedFolderName;
        public bool MoveToProcessedFolder;
        public IEnumerable<Tuple<string, string>> AcceptedResignSenders;
        //set mailbox auto reply
        public bool SetMailBoxAutoReply { get; set; }
        public string AutoReplyString { get; set; }
        public string AutoReplyStringWithContactToken { get; set; }
        //public IDbAdapter Adapter;
        public IEmailHandler EmailHandler;

        public IExecutioner Executioner;
        public string[] ReportCC;
        public string[] ReportReceiver;
        public BussiessLogic Logic;
        public int DeleteAfter;
        public int ReadEmailRetry;
        public int SendReportRetry;

        public AutomatorConfig(Configuration ini, string manDictFilename)
        {
            try
            {
                //declares section
                var sectionEmailAccount = ini["EmailAccount"];
                var sectionExecutioner = ini["Executioner"];
                var sectionMailBoxAutoReply = ini["MailBoxAutoReply"];
                var sectionReport = ini["Report"];
                var sectionRetry = ini["Retry"];
                var sectionPolicy = ini["Policy"];


                var emailAuth = sectionEmailAccount["Authentication"].StringValueTrimmed.Split(':');
                var executioner = sectionExecutioner["Authentication"].StringValueTrimmed;
                //set mailbox auto reply
                SetMailBoxAutoReply = sectionMailBoxAutoReply["SetMailBoxAutoReply"].BoolValue;
                AutoReplyString = sectionMailBoxAutoReply["AutoReplyString"].StringValueTrimmed;
                AutoReplyStringWithContactToken = sectionMailBoxAutoReply["AutoReplyStringWithContactToken"].StringValueTrimmed;
                //report
                SendReport = sectionReport["SendReport"].BoolValue;
                //email account
                //SenderEmailSuffix = nini.Configs["EmailAccount"].GetString("SenderEmailSuffix"),
                EmailHandler = new EmailHandler(emailAuth.First(), emailAuth.Last());
                ResignFolderName = sectionEmailAccount["ResignFolderName"].StringValueTrimmed;
                ProcessedFolderName = sectionEmailAccount["ProcessedFolderName"].StringValueTrimmed;
                MoveToProcessedFolder = sectionEmailAccount["MoveToProcessedFolder"].BoolValue;
                ReadEmailRetry = sectionRetry["ReadEmailRetry"].IntValue;
                ReportCC = sectionReport["ReportCC"].StringValueTrimmed.Split(',');
                ReportReceiver = sectionReport["ReportReceiver"].StringValueTrimmed.Split(',');
                SendReportRetry = sectionRetry["SendReportRetry"].IntValue;

                //exp: luu nhat hong:luu.nhat-hong@hdsaison.com.vn,vo ya phuong khanh:vo.phuong-khanh@hd... *case insenstive
                AcceptedResignSenders = SplitToTuple(sectionEmailAccount["AcceptedResignSenders"].StringValueTrimmed);
                //db adapter
                //Adapter = new DbAdapter($@"{Program.AssemblyDirectory}\db.dat"), //not configureable
                //Executioner
                var manDict = GetManDict(manDictFilename);
                Executioner = executioner == string.Empty ?
                (IExecutioner)new MockExecutioner() { ManagerDictionary = manDict } : 
                new Executioner(executioner.Split(':').First(), executioner.Split(':').Last()) { ManagerDictionary = manDict };
                //Policy
                DeleteAfter = sectionPolicy["DeleteAccountAfter"].IntValue;

                //set auto reply flag & content to Executioner
                if (SetMailBoxAutoReply)
                {
                    if (string.IsNullOrEmpty(AutoReplyString) || string.IsNullOrEmpty(AutoReplyStringWithContactToken))
                        throw new ArgumentException("auto reply strings must be set. when AutoReply is true");

                    Executioner.SetMailBoxAutoReply = true;
                    Executioner.AutoReplyString = AutoReplyString;
                    Executioner.AutoReplyStringWithContact = AutoReplyStringWithContactToken;
                }
                //set report sender account
                var reportAuth = sectionReport["Authentication"].StringValueTrimmed;
                if (!string.IsNullOrEmpty(reportAuth))
                {
                    EmailHandler.ReportSenderUsername = reportAuth.Split(':').First();
                    EmailHandler.ReportSenderPassword = reportAuth.Split(':').Last();
                }
            }
            catch (NullReferenceException)
            {
                //wrap exception
                Console.WriteLine("invalid config!");
                Console.ReadLine();
                throw new ArgumentException("invalid config");
            }
        }
        private static Dictionary<string, string> GetManDict(string manDictFilename)
        {
            var dict = new Dictionary<string, string>();
            var fileInfo = new FileInfo(manDictFilename);
            if (!fileInfo.Exists) return dict;

            using (var textStream = new StreamReader(fileInfo.FullName, Encoding.UTF8, false))
            {
                var reader = new CsvReader(textStream, new CsvHelper.Configuration.Configuration()
                {
                    Delimiter = ";",
                    TrimOptions = CsvHelper.Configuration.TrimOptions.Trim,
                    IgnoreReferences = true,
                    MissingFieldFound = null
                });

                var dictPairType = new
                {
                    Name = string.Empty,
                    Email = string.Empty
                };

                var records = reader.GetRecords(dictPairType);
                foreach (var pair in records)
                {
                    if (dict.ContainsKey(pair.Name.ToLower())) throw new ArgumentException($"Duplicate value: {pair.Name} in manager dict");
                    dict.Add(pair.Name.ToLower(), pair.Email.ToLower());
                }

            }
            return dict;
        }
        private static IEnumerable<Tuple<string, string>> SplitToTuple(string s)
        {
            return from item in s.Split(',')
                   select new Tuple<string, string>(item.Split(':').First().Trim(), item.Split(':').Last().Trim());

            //return s.Split(',').Select(pair => new Tuple<string, string>(pair.Split(':').First().Trim(),
            //    pair.Split(':').Last().Trim()));
        }
    }
}
