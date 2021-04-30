using DevExpress.XtraEditors;
using ConfigMaker.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigMaker.Properties;

namespace ConfigMaker
{
    public static class Locations
    {
        private static DirectoryInfo _workingDirectory;
        private static object _workingDirectoryLock = new object();
        private static DirectoryInfo _userConfigDirectory;
        private static object _userConfigDirectoryLock = new object();
        private static DirectoryInfo _outputDirectory;
        private static object _outputDirectoryLock = new object();

        /// <summary>
        /// Path to the root directory where working files will be stored
        /// </summary>
        public static DirectoryInfo WorkingDirectory
        {
            get
            {
                if (_workingDirectory == null)
                {
                    lock (_workingDirectoryLock)
                    {
                        if (string.IsNullOrEmpty(Settings.Default.WorkingDirectory))
                        {
                            _workingDirectory = new DirectoryInfo(Directory.GetCurrentDirectory());
                        }
                        else
                        {
                            _workingDirectory = new DirectoryInfo(Settings.Default.WorkingDirectory);
                        }
                        if (!_workingDirectory.Exists)
                        {
                            try
                            {
                                _workingDirectory.Create();
                            }
                            catch (Exception ex)
                            {
                                XtraMessageBox.Show(string.Format("Working directory{0}" +
                                    "{1}{0}" +
                                    "doesn't exist and cannot be created.{0}" +
                                    "{2}",
                                    Environment.NewLine, _workingDirectory, ex.Message), "Error");
                                Environment.Exit(1);
                            }
                        }
                    }
                }
                return _workingDirectory;
            }
        }

        /// <summary>
        /// Path to user configurations directory inside working directory
        /// </summary>
        public static DirectoryInfo UserConfigDirectory
        {
            get
            {
                if (_userConfigDirectory == null)
                {
                    lock (_userConfigDirectoryLock)
                    {
                        if (string.IsNullOrEmpty(Settings.Default.UserConfigSubdirectory))
                        {
                            _userConfigDirectory = new DirectoryInfo(Path.Combine(WorkingDirectory.FullName, "configurations"));
                        }
                        else
                        {
                            _userConfigDirectory = new DirectoryInfo(Path.Combine(WorkingDirectory.FullName, Settings.Default.UserConfigSubdirectory));
                        }
                        if (!_userConfigDirectory.Exists)
                        {
                            try
                            {
                                _userConfigDirectory.Create();
                            }
                            catch (Exception ex)
                            {
                                XtraMessageBox.Show(string.Format("User configurations directory{0}" +
                                    "{1}{0}" +
                                    "doesn't exist and cannot be created.{0}" +
                                    "{2}",
                                    Environment.NewLine, _userConfigDirectory, ex.Message), "Error");
                                Environment.Exit(1);
                            }
                        }
                    }
                }
                return _userConfigDirectory;
            }
        }

        /// <summary>
        /// Path to XMLTV output directory inside working directory
        /// </summary>
        public static DirectoryInfo OutputDirectory
        {
            get
            {
                if (_outputDirectory == null)
                {
                    lock (_outputDirectoryLock)
                    {
                        if (string.IsNullOrEmpty(Settings.Default.OutputDirectory))
                        {
                            _outputDirectory = new DirectoryInfo(Path.Combine(WorkingDirectory.FullName, "output"));
                        }
                        else
                        {
                            _outputDirectory = new DirectoryInfo(Path.Combine(WorkingDirectory.FullName, Settings.Default.OutputDirectory));
                        }
                        if (!_outputDirectory.Exists)
                        {
                            try
                            {
                                _outputDirectory.Create();
                            }
                            catch (Exception ex)
                            {
                                XtraMessageBox.Show(string.Format("Output directory{0}" +
                                    "{1}{0}" +
                                    "doesn't exist and cannot be created.{0}" +
                                    "{2}",
                                    Environment.NewLine, _userConfigDirectory, ex.Message), "Error");
                                Environment.Exit(1);
                            }
                        }
                    }
                }
                return _outputDirectory;
            }
        }

    }
}
