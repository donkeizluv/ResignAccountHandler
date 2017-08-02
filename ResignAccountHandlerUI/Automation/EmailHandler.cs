using MailKit;
using MailKit.Net.Imap;
using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading;

namespace ResignAccountHandlerUI.Automation
{
    public interface IEmailHandler
    {
        string SmtpMailServer { get; set; }
        string ImapMailServer { get; set; }
        int ImapPort { get; set; }
        int SmtpPort { get; set; }

        string FormReaderPassword { get; set; }
        string FormReaderUsername { get; set; }
        string ReportSenderPassword { get; set; }
        string ReportSenderUsername { get; set; }

        string SenderEmailSuffix { get; set; }
        string ResignFolderName { get; set; }
        string ProcessedFolderName { get; set; }
        bool MoveToProcessedFolder { get; set; }

        IEnumerable<MimeMessage> GetImapEmail(string folderName, IEnumerable<MailboxAddress> acceptSenders);

        void SendEmail(IEnumerable<MailboxAddress> recipients, IEnumerable<MailboxAddress> cc, string htmlBody, string subject);
    }

    public class EmailHandler : IEmailHandler
    {
        public EmailHandler(string userName, string pwd)
        {
            FormReaderUsername = userName;
            FormReaderPassword = pwd;
        }
        public EmailHandler(string userName, string pwd, bool moveProcessed, string processedFolder)
        {
            FormReaderUsername = userName;
            FormReaderPassword = pwd;
            MoveToProcessedFolder = moveProcessed;
            ProcessedFolderName = processedFolder;
        }

        //public virtual string Pop3MailServer { get; set; }
        //public virtual int Pop3Port { get; set; } = 995;
        public virtual string SmtpMailServer { get; set; } = "mail.hdsaison.com.vn";
        //public virtual string ImapMailServer { get; set; } = "prd-vn-mail05.sgvf.sgcf"; //faster than mail.hdsaison.com.vn
        public virtual string ImapMailServer { get; set; } = "mail.hdsaison.com.vn";
        public virtual int ImapTimeout { get; set; } = 1000 * 90;
        public virtual int SmptpTimeout { get; set; } = 1000 * 90;
        public virtual int ImapPort { get; set; } = 143; //993 for internet
        public virtual int SmtpPort { get; set; } = 25; //faster than 587
        public string FormReaderPassword { get; set; }
        public string FormReaderUsername { get; set; }
        public string SenderEmailSuffix { get; set; } = "@hdsaison.com.vn";
        public string ResignFolderName { get; set; }
        public string ProcessedFolderName { get; set; }
        public bool MoveToProcessedFolder { get; set; } = false;
        private string _reportPwd;
        private string _reportUsername;
        //use read account to send if report sender account is not set
        public string ReportSenderPassword
        {
            get
            {
                if (string.IsNullOrEmpty(_reportPwd))
                    return FormReaderPassword;
                return _reportPwd;
            }
            set
            {
                _reportPwd = value;
            }
        }
        public string ReportSenderUsername
        {
            get
            {
                if (string.IsNullOrEmpty(_reportUsername))
                    return FormReaderUsername;
                return _reportUsername;
            }
            set
            {
                _reportUsername = value;
            }
        }

        public IEnumerable<MimeMessage> GetImapEmail(string folderName, IEnumerable<MailboxAddress> acceptSenders)
        {
            var messList = new List<MimeMessage>();
            ResignFolderName = folderName;
            using (var client = new ImapClient())
            {
                //accept all cert
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                client.Timeout = ImapTimeout;
                //internet
                //client.SslProtocols = System.Security.Authentication.SslProtocols.Tls;
                //client.Connect("mail.hdsaison.com.vn", 993, true); //slower

                //local network
                client.Connect(ImapMailServer, ImapPort, false);

                //try time out socket
                //var socket = GetSocket("mail.hdsaison.com.vn", 993);
                //client.Connect(socket, "mail.hdsaison.com.vn", 993);

                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate(FormReaderUsername, FormReaderPassword);


                // The Inbox folder is always available on all IMAP servers...
                //var folder = client.Inbox();
                var resignFolder = GetFolder(client, ResignFolderName, CancellationToken.None) ??
                    throw new NullReferenceException($"Folder {ResignFolderName} not found");
                resignFolder.Open(FolderAccess.ReadWrite);
                IMailFolder processedFolder = null;
                if(MoveToProcessedFolder)
                {
                    processedFolder = GetFolder(client, ProcessedFolderName, CancellationToken.None) ??
                        throw new NullReferenceException($"Folder {ProcessedFolderName} not found");
                    //processedFolder.Open(FolderAccess.ReadWrite);
                }

                for (int i = 0; i < resignFolder.Count; i++)
                {
                    var message = resignFolder.GetMessage(i);
                    if (!IsSenderInList(message.From, acceptSenders)) continue;
                    messList.Add(message);
                    //seems to work now
                    //needs more testing
                    if(MoveToProcessedFolder)
                    {
                        resignFolder.MoveTo(i, processedFolder);
                        resignFolder.AddFlags(new int[] { i }, MessageFlags.Deleted, true);
                    }
                }
                resignFolder.Expunge();
                client.Disconnect(true);
            }
            return messList;
        }

        private bool IsSenderInList(InternetAddressList from, IEnumerable<MailboxAddress> senderList)
        {
            foreach (var fromAdress in from)
            {
                foreach (var address in from)
                {
                    if (string.Compare(fromAdress.Name, address.Name, true) == 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void SendEmail(IEnumerable<MailboxAddress> recipients, IEnumerable<MailboxAddress> cc, string htmlBody, string subject)
        {
            var message = new MimeMessage();
            message.Subject = subject;
            //set From
            message.From.Add(new MailboxAddress(ReportSenderUsername + SenderEmailSuffix));
            //add recipients
            message.To.AddRange(recipients?? new List<MailboxAddress>());
            //add cc
            if(cc != null || cc.Count() > 0)
                message.Cc.AddRange(cc);
            //build body
            var bodyBuilder = new BodyBuilder()
            {
                HtmlBody = htmlBody
            };
            message.Body = bodyBuilder.ToMessageBody();
            SendEmail(message);
        }

        private void SendEmail(MimeMessage email)
        {
            using (var client = new SmtpClient())
            {
                // For demo-purposes, accept all SSL certificates
                client.Timeout = SmptpTimeout;
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                client.Connect(SmtpMailServer, SmtpPort, false);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                // Note: only needed if the SMTP server requires authentication
                client.Authenticate(ReportSenderUsername, ReportSenderPassword);
                client.Send(email);
                client.Disconnect(true);
            }
        }

        private static IMailFolder GetFolder(ImapClient client, string folderName, CancellationToken cancellationToken)
        {
            var personal = client.GetFolder(client.PersonalNamespaces[0]);

            foreach (var folder in personal.GetSubfolders(false, cancellationToken))
            {
                if (string.Compare(folder.Name, folderName, true) == 0)
                    return folder;
            }

            return null;
        }
    }
}