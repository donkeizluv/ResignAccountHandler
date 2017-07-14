using Microsoft.Office.Interop.Outlook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace ResignAccountHandlerCore
{
    public class OutlookWrapper
    {
        private readonly Application _app;

        public OutlookWrapper()
        {
            //_app = GetOutlook();
            //if (_app == null)
            //    throw new NullReferenceException("Cant get outlook instance, probly not running.");

            _app = GetOutlook() ?? throw new NullReferenceException("Cant get outlook instance, probly not running.");
        }

        public string GetHtmlBodySelectedEmail()
        {
            var email = GetSelectedMailItem();
            return email.HTMLBody;
        }

        public IEnumerable<MailItem> GetItemInCurrentSelectedFolder()
        {
            var list = new List<MailItem>();
            var oLFolder = _app.ActiveExplorer().CurrentFolder;
            foreach (var item in oLFolder.Items)
            {
                if (item is MailItem)
                {
                    list.Add(item as MailItem);
                }
            }
            return list;
        }

        public IEnumerable<MailItem> GetItemInCurrentSelectedFolder(params string[] senders)
        {
            var list = new List<MailItem>();
            var oLFolder = _app.ActiveExplorer().CurrentFolder;
            foreach (var item in oLFolder.Items)
            {
                var mailItem = item as MailItem;
                if (mailItem == null) continue;
                //Console.WriteLine(mailItem.SenderEmailAddress);
                if (senders.ToList().Contains(mailItem.SenderName))
                {
                    //if(mailItem.BodyFormat != OlBodyFormat.olFormatHTML)
                    //{
                    //    Debug.WriteLine("this fucking email right here");
                    //}
                    list.Add(mailItem);
                }
            }
            return list;
        }

        //public IEnumerable<MailItem> GetItemInCurrentSelectedFolder(string[] senders)
        //{
        //    var list = new List<MailItem>();
        //    var oLFolder = _app.ActiveExplorer().CurrentFolder;
        //    bool FoundStringInArray(string[] arr, string s)
        //    {
        //        foreach (var item in arr)
        //        {
        //            if (string.Compare(item, s, true) == 0) return true;
        //        }
        //        return false;
        //    }
        //    foreach (var item in oLFolder.Items)
        //    {
        //        var mailItem = item as MailItem;
        //        if (mailItem == null) continue;
        //        //Console.WriteLine(mailItem.SenderEmailAddress);
        //        if (FoundStringInArray(senders, mailItem.SenderName))
        //        {
        //            list.Add(mailItem);
        //        }
        //    }
        //    return list;
        //}

        private MailItem GetSelectedMailItem()
        {
            foreach (var item in _app.ActiveExplorer().Selection)
            {
                var email = (MailItem)item;
                return email;
            }
            throw new NullReferenceException("Cant get selected email.");
        }

        public static bool IsOutlookRunning()
        {
            var ol = GetOutlook();
            return ol != null;
        }

        private static Application GetOutlook()
        {
            try
            {
                //outlook needs to be clicked on (foreground) once before this script works....weird :/
                var ol = (Application)Marshal.GetActiveObject("Outlook.Application");
                return ol;
            }
            catch (COMException ex) when (ex.HResult == -2147221021) //operation invalid
            {
                return null;
            }
        }
    }
}