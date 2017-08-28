using ResignAccountHandlerUI.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ResignAccountHandlerUI.Automation
{
    public static class HtmlComposer
    {
        private static string HeadTag = Resources.HeadTag;

        private static string BuildHeadersHtml(string[] header)
        {
            var builder = new StringBuilder();
            builder.AppendLine("<tr>");
            for (int i = 0; i < header.Count(); i++)
            {
                string val = header[i] ?? throw new ArgumentException($"column header at {i} is empty");
                //if (val == string.Empty) continue; //should allow empty header
                builder.AppendLine(string.Format("<td><b>{0}</b></td>", val));
            }
            builder.AppendLine("</tr>");
            return builder.ToString();
        }

        public static string ComposeOpening()
        {
            var builder = new StringBuilder();
            builder.AppendLine("<html>").AppendLine(HeadTag).AppendLine("<body>");
            return builder.ToString();
        }

        public static string ComposeClosing()
        {
            var builder = new StringBuilder();
            builder.AppendLine("</body>").AppendLine("</html>");
            return builder.ToString();
        }

        public static string ComposeTable(IEnumerable<IEnumerable<string>> content, string[] header)
        {
            var builder = new StringBuilder();
            if (content.Count() < 1)
            {
                builder.AppendLine("<p>None.</p>");
                return builder.ToString();
            }
            //insert header
            builder.AppendLine("<table>");
            builder.AppendLine(BuildHeadersHtml(header));

            //row iteration
            foreach (var row in content)
            {
                builder.AppendLine("<tr>");

                foreach (var rowContent in row)
                {
                    builder.AppendLine(string.Format("<td>{0}</td>", rowContent));
                }
                builder.AppendLine("</tr>");
            }
            //close table
            builder.AppendLine("</table>");
            return builder.ToString();
        }
    }
}