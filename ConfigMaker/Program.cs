using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ConfigMaker.Commands;
using ConfigMaker.Properties;
using DevExpress.LookAndFeel;
using DevExpress.XtraEditors;
using Ionic.Zip;
using log4net;
using log4net.Config;

namespace ConfigMaker
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            DevExpress.Skins.SkinManager.EnableFormSkins();
            DevExpress.UserSkins.BonusSkins.Register();
            UserLookAndFeel.Default.SetSkinStyle("DevExpress Style");
            Log.Configure();

            Log.Info("Entering application.");
            bool runnAll = false;
            bool compress = false;

            if (args.ToList().Any(x => x.ToLower() == "/runall"))
            {
                runnAll = true;
                if (args.ToList().Any(x => x.ToLower() == "/compress"))
                {
                    compress = true;
                }
            }

            var sitePackDirLocation = Path.Combine(Locations.WorkingDirectory.FullName, Settings.Default.SiteIniPack);
            if (!Directory.Exists(sitePackDirLocation))
            {
                if (runnAll)
                {
                    Log.Error(string.Format("Can't continue without siteini.pack.{0}Application will exit.", Environment.NewLine));
                    Application.Exit();
                }
                if (mainForm.AskQuestion("siteini.pack not found in working folder. Download new?"))
                {
                    var siteIniUpdater = new SiteIniPackUpdater(Locations.WorkingDirectory);
                    siteIniUpdater.StatusChanged += (sender, e) => SplashManager.ShowSplashScreen(e.Status, e.Description);
                    siteIniUpdater.Execute().GetAwaiter().GetResult();
                }
                else
                {
                    Log.Error(string.Format("Can't continue without siteini.pack.{0}Application will exit.", Environment.NewLine));
                    XtraMessageBox.Show(string.Format("Can't continue without siteini.pack.{0}Application will exit.", Environment.NewLine));
                    Application.Exit();
                }
            }
            if (runnAll)
            {
                RunAll(compress);
                return;
            }

            Application.Run(new mainForm());
            Log.Info("Exiting application.");
        }

        private static void RunAll(bool compress)
        {
            PreRunTask();
            var listConfigurations = new ListConfigurations(Locations.UserConfigDirectory);
            var configList = listConfigurations.Execute();
            var runConfigsCommand = new RunAllConfigurations();
            runConfigsCommand.Execute(configList, true);
            if (compress)
            {
                foreach (var file in Locations.OutputDirectory.GetFiles("*.xml"))
                {
                    using (ZipFile zip = new ZipFile())
                    {
                        var zipFileName = Path.Combine(Locations.OutputDirectory.FullName, Path.GetFileNameWithoutExtension(file.Name) + ".zip");
                        zip.AddFile(file.FullName);
                        zip.Save(zipFileName);
                    }
                }
            }
            PostRunTask();
        }

        public static void PreRunTask()
        {
            if (string.IsNullOrEmpty(Settings.Default.PreRunTask))
            {
                Log.Info("Pre Run Task not defined, skipping...");
                return;
            }
            if (!File.Exists(Settings.Default.PreRunTask))
            {
                Log.Error(string.Format("Pre Run Task {0} doesn't exist, skipping...", Settings.Default.PreRunTask));
                return;
            }
            var runTask = new RunProcess();
            runTask.Execute(Settings.Default.PreRunTask, null, true);
        }

        public static void PostRunTask()
        {
            if (string.IsNullOrEmpty(Settings.Default.PostRunTask))
            {
                Log.Info("Post Run Task not defined, skipping...");
                return;
            }
            if (!File.Exists(Settings.Default.PostRunTask))
            {
                Log.Error(string.Format("Post Run Task {0} doesn't exist, skipping...", Settings.Default.PostRunTask));
                return;
            }
            var runTask = new RunProcess();
            runTask.Execute(Settings.Default.PostRunTask, null, true);
        }
    }
}