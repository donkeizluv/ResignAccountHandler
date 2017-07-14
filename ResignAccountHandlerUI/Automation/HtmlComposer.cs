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
            builder.Append("<tr>");
            for (int i = 0; i < header.Count(); i++)
            {
                string val = header[i] ?? throw new ArgumentException($"column header at {i} is empty");
                //if (val == string.Empty) continue; //should allow empty header
                builder.Append(string.Format("<td><b>{0}</b></td>", val));
            }
            builder.Append("</tr>");
            return builder.ToString();
        }

        public static string ComposeOpening()
        {
            var builder = new StringBuilder();
            builder.Append("<html>").Append(HeadTag).Append("<body>");
            return builder.ToString();
        }

        public static string ComposeClosing()
        {
            var builder = new StringBuilder();
            builder.Append("</body>").Append("</html>");
            return builder.ToString();
        }

        public static string ComposeTable(IEnumerable<IEnumerable<string>> content, string[] header)
        {
            var builder = new StringBuilder();
            if (content.Count() < 1)
            {
                builder.Append("<p>None.</p>");
                return builder.ToString();
            }
            //insert header
            builder.Append("<table>");
            builder.Append(BuildHeadersHtml(header));

            //row iteration
            foreach (var row in content)
            {
                builder.Append("<tr>");

                foreach (var rowContent in row)
                {
                    builder.Append(string.Format("<td>{0}</td>", rowContent));
                }
                builder.Append("</tr>");
            }
            //close table
            builder.Append("</table>");
            return builder.ToString();
        }
    }
}