using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ResignAccountHandlerUI.ResignExtractor;

namespace TestZone
{
    class Program
    {
        public static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                var uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }
        static void Main(string[] args)
        {
            var extractor = new ResignInfoExtractor();
            var doc = new HtmlAgilityPack.HtmlDocument();
            string content = File.ReadAllText($"{AssemblyDirectory}\\test2.txt");
            doc.LoadHtml(content);
            var test1 = extractor.Test(doc, "tên nhân viên:");
            var test2 = extractor.Test(doc, "e14778");
        }
    }
}
