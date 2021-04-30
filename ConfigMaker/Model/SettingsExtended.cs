using ConfigMaker.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ConfigMaker.Model
{
    partial class settings
    {
        [XmlIgnore()]
        public FileInfo Path { get; set; }

        const string appDataLocal = "%LOCALAPPDATA%";

        public static string AppDataConfigPath
        {
            get
            {
                return System.IO.Path.Combine(
                Environment.ExpandEnvironmentVariables(appDataLocal),
                "WebGrab+Plus",
                Settings.Default.WebGrabConfigFileName
                );
            }
        }

        public static string WorkingDirectoryConfigPath
        {
            get
            {
                return System.IO.Path.Combine(
               Locations.WorkingDirectory.FullName,
               Settings.Default.WebGrabConfigFileName
               );
            }
        }

        public static string DefaultDirectoryConfigPath
        {
            get
            {
               if (WorkingDirectoryConfigExists)
                {
                    return WorkingDirectoryConfigPath;
                }
                if (AppDataConfigExists)
                {
                    return AppDataConfigPath;
                }
                return WorkingDirectoryConfigPath;
            }
        }

        public static settings LoadTemplate()
        {
            return settings.Deserialize(Resources.WebGrab_config_template);
        }

        public static settings LoadAppDataConfig()
        {

            if (File.Exists(AppDataConfigPath))
            {
                var s= settings.LoadFromFile(AppDataConfigPath);
                s.Path = new FileInfo(AppDataConfigPath);
                return s;
            }
            return null;
        }

        public static settings LoadWorkingDirectory()
        {      
            if (File.Exists(WorkingDirectoryConfigPath))
            {
                var s=  settings.LoadFromFile(WorkingDirectoryConfigPath);
                s.Path = new FileInfo(WorkingDirectoryConfigPath);
                return s;
            }
            return null;
        }

        public static bool WorkingDirectoryConfigExists
        {
            get
            {
                return File.Exists(WorkingDirectoryConfigPath);
            }
        }

        public static bool AppDataConfigExists
        {
            get
            {
                return File.Exists(AppDataConfigPath);
            }
        }

        public static settings LoadDefault()
        {
            if (WorkingDirectoryConfigExists)
            {
                var s = LoadWorkingDirectory();
                s.Path = new FileInfo(WorkingDirectoryConfigPath);
                return s;
            }
            if (AppDataConfigExists)
            {
                var s = LoadAppDataConfig();
                s.Path = new FileInfo(AppDataConfigPath);
                return s;
            }
            var settings = LoadTemplate();
            settings.Path = new FileInfo(WorkingDirectoryConfigPath);
            return settings;
        }
    }
}
