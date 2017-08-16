using LiteDB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ResignAccountHandlerUI.Model;

namespace DbUpdater
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
            //ReadAllWithMap();
        }

        //private void ReadAllBson()
        //{
        //    using (var db = new LiteDatabase(GetConnectionString($@"{AssemblyDirectory}\db.dat")))
        //    {
        //        var collection = db.GetCollection("Resign");
        //        foreach (var rec in collection.FindAll())
        //        {
        //            //add new field
        //            //rec.Add("Contact", string.Empty);
        //            //collection.Update(rec);
        //            Console.WriteLine(rec.ToString());
        //        }
        //    }
        //    Console.WriteLine("done!");
        //    Console.ReadLine();
        //}
        //private static void ReadAllWithMap()
        //{
        //    bool test = true;
        //    using (var db = new LiteDatabase(GetConnectionString($@"{AssemblyDirectory}\db.dat")))
        //    {
        //        var collection = db.GetCollection<Resignation>("Resign");
        //        foreach (var rec in collection.FindAll())
        //        {
        //            if (test)
        //            {
        //                rec.Contact = string.Empty;
        //                collection.Update(rec);
        //            }
        //            Console.WriteLine($"{rec.ADName} - {rec.ResignDay} - {rec.Contact}");
        //        }
        //    }
        //    Console.WriteLine("done!");
        //    Console.ReadLine();
        //}

        private static string GetConnectionString(string path)
        {
            return $"filename={path}; Timeout=10";
        }
    }
}
