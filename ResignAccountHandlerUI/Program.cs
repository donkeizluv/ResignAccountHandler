using ResignAccountHandlerUI.Adapter;
using ResignAccountHandlerUI.AdExecutioner;
using ResignAccountHandlerUI.Forms;
using ResignAccountHandlerUI.UIController;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace ResignAccountHandlerUI
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>

        [STAThread]
        private static void Main(string[] agrs)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var viewer = new FormResignHandler();
            //var controller = new ResignAccountHanlderController(viewer, new Executioner(userName, pwd),
            //    new DbAdapter($@"{AssemblyDirectory}\db.dat"));
            var controller = new ResignAccountHanlderController(viewer, new MockExecutioner(),
                new DbAdapter($@"{AssemblyDirectory}\db.dat"));
            viewer.Controller = controller;
            Application.Run(viewer);
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
        public static string Version
        {
            get
            {
                System.Reflection.Assembly assembly = Assembly.GetExecutingAssembly();
                FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
                return fvi.FileVersion;
            }
        }
    }
}