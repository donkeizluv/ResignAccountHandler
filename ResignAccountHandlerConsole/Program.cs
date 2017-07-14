﻿using Nini.Config;
using ResignAccountHandlerUI.AdExecutioner;
using ResignAccountHandlerUI.Automation;
using ResignAccountHandlerUI.Log;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ResignAccountHandlerConsole
{
    internal class Program
    {
        private const string ConfigFileName = "config.ini";
        private static ILogger _logger = LogManager.GetLogger(typeof(Program));
        public static void Main(string[] args)
        {
            bool unhandleEx = false;
            try
            {
                var config = GetConfig();
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
                _logger.Log(ex);
                unhandleEx = true;
                //Console.ReadLine(); //cmt this when run
            }
            finally
            {
                Environment.Exit(unhandleEx ? 1 : 0);
            }
        }

        private static AutomatorConfig GetConfig()
        {
            try
            {
                var nini = new IniConfigSource($@"{AssemblyDirectory}\{ConfigFileName}");
                var emailAuth = nini.Configs["EmailAccount"].GetString("Authentication").Split(':');
                var executioner = nini.Configs["Executioner"].GetString("Authentication");
                var config = new AutomatorConfig()
                {
                    //report
                    SendReport = nini.Configs["Report"].GetBoolean("SendReport"),
                    //email account
                    SenderEmailSuffix = nini.Configs["EmailAccount"].GetString("SenderEmailSuffix"),
                    EmailHandler = new EmailHandler(emailAuth.First(), emailAuth.Last()),
                    ResignFolderName = nini.Configs["EmailAccount"].GetString("ResignFolderName"),
                    ProcessedFolderName = nini.Configs["EmailAccount"].GetString("ProcessedFolderName"),
                    MoveToProcessedFolder = nini.Configs["EmailAccount"].GetBoolean("MoveToProcessedFolder"),
                    ReportCC = nini.Configs["EmailAccount"].GetString("ReportCC").Split(','),
                    ReportReceiver = nini.Configs["EmailAccount"].GetString("ReportReceiver").Split(','),

                    //exp: luu nhat hong:luu.nhat-hong@hdsaison.com.vn,vo ya phuong khanh:vo.phuong-khanh@hd... *case insenstive
                    AcceptedResignSenders = SplitToTuple(nini.Configs["EmailAccount"].GetString("AcceptedResignSenders")),
                    //db adapter
                    //Adapter = new DbAdapter($@"{Program.AssemblyDirectory}\db.dat"), //not configureable
                    //Executioner
                    Executioner = executioner == string.Empty ?
                    (IExecutioner)new MockExecutioner() : new Executioner(executioner.Split(':').First(), executioner.Split(':').Last()),
                    //Policy
                    DeleteAfter = nini.Configs["Policy"].GetInt("DeleteAccountAfter")
                };
                return config;
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("invalid config!");
                Console.ReadLine();
                throw new ArgumentException("invalid config");
            }
           
        }

        private static IEnumerable<Tuple<string, string>> SplitToTuple(string s)
        {
            return from item in s.Split(',')
                   select new Tuple<string, string>(item.Split(':').First().Trim(), item.Split(':').Last().Trim());

            //return s.Split(',').Select(pair => new Tuple<string, string>(pair.Split(':').First().Trim(),
            //    pair.Split(':').Last().Trim()));
        }

        

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
    }
}