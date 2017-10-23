using ResignAccountHandlerUI.Automation;
using ResignAccountHandlerUI.Log;
using System;
using System.IO;
using System.Reflection;
using ResignAccountHandlerUI.Logger;
using SharpConfig;

namespace ResignAccountHandlerConsole
{
    internal class Program
    {
        private const string ConfigFilemame = "config.ini";
        private const string ManDictFilename = "man_dict.txt";

        private static ILogger _logger = LogManager.GetLogger(typeof(Program));
        public static void Main(string[] args)
        {
            bool unhandleEx = false;
            try
            {
                var ini = Configuration.LoadFromFile($"{ExeDir}\\{ConfigFilemame}");
                var config = new AutomatorConfig(ini, ManDictFilename);
                var auto = AutomatorFactory.GetAutomator(config);
                //var auto = AutomatorFactory.GetDebugAutomator();
                auto.Run();
                //_logger.Log("Done!");
                //Console.ReadLine();
                //var automation = AutomatorFactory.GetDebugAutomator();
                //automation.Run()
                //Console.ReadLine(); //cmt this when run
            }
            catch (Exception ex) //be more specific
            {
                _logger.Log(ex); //logger handles inner ex
                unhandleEx = true;
                //Console.ReadLine(); //cmt this when run
            }
            finally
            {
                Environment.Exit(unhandleEx ? 1 : 0);
            }
        }

        public static string ExeDir
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                var uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }
    }
}