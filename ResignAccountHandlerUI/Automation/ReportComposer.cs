using System;
using System.Collections.Generic;
using System.Text;

namespace ResignAccountHandlerUI.Automation
{
    public static class ReportComposer
    {
        private static readonly string GreetingLine = "Dear all,</p><p>Report from resign handler as follows:";
        private static readonly string SaluteLine = "Regards,";

        private static readonly string ReportUpdateGreeting = "<b>Reading forms result:</b>";
        private static readonly string[] UpdateResultHeader = new string[] { "Subject", "ReceiveDate", "ResignDay", "Message", "Code" };

        private static readonly string DisableGreeting = "<b>Account deactivation:</b>";
        private static readonly string[] DisableResultHeader = new string[] { "Index", "AD", "HR", "ReceiveDate", "ResignDay", "Message", "Code" };

        private static readonly string DeleteGreeting = "<b>Account deletion:</b>";

        //DeleteResults.Add(MakeRow(resign.ADName, resign.HRCode, resign.ReceiveDate.ToString(), erorr, Code.I.ToString()));
        public static string MakeReportBody(List<List<string>> updateResult, List<List<string>> disableResult, List<List<string>> deleteResult)
        {
            var htmlBodyBuilder = new StringBuilder();
            htmlBodyBuilder.AppendLine(HtmlComposer.ComposeOpening());

            htmlBodyBuilder.AppendLine(InsertPTag(GreetingLine));
            //update report
            htmlBodyBuilder.AppendLine(InsertPTag(ReportUpdateGreeting));
            htmlBodyBuilder.AppendLine(HtmlComposer.ComposeTable(updateResult, UpdateResultHeader));
            //disable report
            htmlBodyBuilder.AppendLine(InsertPTag(DisableGreeting));
            htmlBodyBuilder.AppendLine(HtmlComposer.ComposeTable(disableResult, DisableResultHeader));
            //delete report
            htmlBodyBuilder.AppendLine(InsertPTag(DeleteGreeting));
            htmlBodyBuilder.AppendLine(HtmlComposer.ComposeTable(deleteResult, DisableResultHeader));
            //version
            htmlBodyBuilder.AppendLine(InsertPTag($"v{Program.Version}"));
            htmlBodyBuilder.AppendLine(HtmlComposer.ComposeClosing());
            return htmlBodyBuilder.ToString();
        }
        private static string InsertPTag(string line)
        {
            return $"<p>{line}</p>";
        }

    }
}
