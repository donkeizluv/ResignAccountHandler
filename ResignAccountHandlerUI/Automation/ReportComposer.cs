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
            htmlBodyBuilder.Append(HtmlComposer.ComposeOpening());

            htmlBodyBuilder.Append(InsertPTag(GreetingLine));
            //update report
            htmlBodyBuilder.Append(InsertPTag(ReportUpdateGreeting));
            htmlBodyBuilder.Append(HtmlComposer.ComposeTable(updateResult, UpdateResultHeader));
            //disable report
            htmlBodyBuilder.Append(InsertPTag(DisableGreeting));
            htmlBodyBuilder.Append(HtmlComposer.ComposeTable(disableResult, DisableResultHeader));
            //delete report
            htmlBodyBuilder.Append(InsertPTag(DeleteGreeting));
            htmlBodyBuilder.Append(HtmlComposer.ComposeTable(deleteResult, DisableResultHeader));
            //version
            htmlBodyBuilder.Append(InsertPTag($"v{Program.Version}"));
            htmlBodyBuilder.Append(HtmlComposer.ComposeClosing());
            return htmlBodyBuilder.ToString();
        }
        private static string InsertPTag(string line)
        {
            return $"<p>{line}</p>";
        }

    }
}
