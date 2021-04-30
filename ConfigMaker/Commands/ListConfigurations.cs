using ConfigMaker.Model;
using ConfigMaker.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigMaker.Commands
{
    public class ListConfigurations
    {
        public DirectoryInfo UserConfigDirectory { get; }

        public event StatusChangedEventHandler StatusChanged;

        private void ReportStatus(string status = null, string description = null)
        {
            StatusChanged?.Invoke(this, new StatusChangedEventArgs(status, description));
        }

        public ListConfigurations(DirectoryInfo userConfigDirectory)
        {
            UserConfigDirectory = userConfigDirectory;
        }

        public  BindingList<GrabConfiguration> Execute()
        {
            return ScanConfigurations(UserConfigDirectory);
        }

        private BindingList<GrabConfiguration> ScanConfigurations(DirectoryInfo userConfigDirectory)
        {
            try
            {
                ReportStatus("Searching configurations", "");
                Log.Debug(string.Format("Searching for configurations in {0}", userConfigDirectory));
                var configurations = new BindingList<GrabConfiguration>();
                var loadConfigCommand = new LoadConfiguration();
                foreach (var dir in userConfigDirectory.GetDirectories())
                {
                    var siteIniUserDir = new DirectoryInfo(Path.Combine(dir.FullName, Settings.Default.SiteIniUser));
                    // var listCountries = new ListCountries(siteIniUserDir);
                    //var countries = listCountries.Execute();
                    //if (countries.Count>  0)
                    //{
                        //var userConfig = new GrabConfiguration(dir) { Countries = countries};
                        //userConfig.Config.Path = new FileInfo(Path.Combine(dir.FullName, Settings.Default.WebGrabConfigFileName));
                        //if (userConfig.Path.Exists)
                        //{
                        //    userConfig.Config = settings.LoadFromFile(userConfig.Config.Path.FullName);
                        //}
                        var userConfig = loadConfigCommand.Execute(dir);
                    if (userConfig != null)
                    {
                        configurations.Add(userConfig);
                        Log.Debug(string.Format("Found user config {0}", userConfig.Name));
                    }
                }
                //}
                Log.Debug(string.Format("Found {0} user configurations in {1}", configurations.Count, userConfigDirectory.FullName));
                return configurations;
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("Search for configurations failed.{0}{1}", Environment.NewLine, ex.Message), ex);
                throw;
            }
        }

    }
}
