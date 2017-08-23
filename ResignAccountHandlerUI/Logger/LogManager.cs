using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ResignAccountHandlerUI.Logger;

namespace ResignAccountHandlerUI.Log
{
    public static class LogManager
    {
        private const string LogFileName = "log.txt";
        private const string OtherLogFolder = "Reports";
        private static readonly List<ILogger> ListLogger = new List<ILogger>();
        public static bool WriteToFile { get; set; } = true;
        private static string LogPath => string.Format(@"{0}\{1}", Program.AssemblyDirectory, LogFileName);
        
        public static ILogger GetLogger(Type t)
        {
            var logger = new SimpleLogger(t);
            ListLogger.Add(logger);
            logger.OnNewLog += Logger_OnNewLog;
            return logger;
        }

        private static void Logger_OnNewLog(ILogger log, NewLogEventArgs e)
        {
            if(e.Ex != null)
            {
                WriteEx(e.Ex);
            }
            if (!string.IsNullOrEmpty(e.Log))
            {
                WriteLog(e.Log);
            }

        }

        public static void WriteOtherLog(string fileName, string content)
        {
            string fullFilename = string.Format(@"{0}\{1}\{2}", Program.AssemblyDirectory, OtherLogFolder, fileName);
            Directory.CreateDirectory(Path.GetDirectoryName(fullFilename));
            File.AppendAllText(Path.GetFullPath(fullFilename),
                content, Encoding.UTF8);
        }

        private static void WriteLog(string log)
        {
            Console.WriteLine(FormatLog(log));
            if (!WriteToFile) return;
            File.AppendAllLines(LogPath, new List<string> { FormatLog(log) }, Encoding.UTF8);
        }
        private static void WriteEx(Exception ex)
        {
            if (ex == null) return;
            Console.WriteLine("### Exception ### {0:G}", DateTime.Now);
            Console.WriteLine(ex.GetType().ToString());
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.StackTrace);
            if (ex.InnerException != null)
            {
                Console.WriteLine("Inner ex:");
                Console.WriteLine(ex.InnerException.GetType().ToString());
                Console.WriteLine(ex.InnerException.Message);
                Console.WriteLine(ex.InnerException.StackTrace);
            }
            if (!WriteToFile) return;
            AppendLine(string.Format("### Exception ### {0:G}", DateTime.Now));
            AppendLine(ex.GetType().ToString());
            AppendLine(ex.Message);
            AppendLine(ex.StackTrace);
            if (ex.InnerException != null)
            {
                AppendLine("Inner ex:");
                AppendLine(ex.InnerException.GetType().ToString());
                AppendLine(ex.InnerException.Message);
                AppendLine(ex.InnerException.StackTrace);
            }
        }
        private static void AppendLine(string s)
        {
            File.AppendAllText(LogPath, s + Environment.NewLine);
        }
        private static string FormatLog(string log)
        {
            return string.Format("{0:G} - {1}", DateTime.Now, log);
        }
    }
}