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
    public class LoadConfiguration
    {
       public event StatusChangedEventHandler StatusChanged;

        private void ReportStatus(string status = null, string description = null)
        {
            StatusChanged?.Invoke(this, new StatusChangedEventArgs(status, description));
        }

        public GrabConfiguration Execute(DirectoryInfo configurationDirectory)
        {
            try
            {
                ReportStatus("Loading configuration", "");
                Log.Debug(string.Format("Loading user configuration from {0}", configurationDirectory.FullName));

                var webGrabConfFile = Path.Combine(configurationDirectory.FullName, Settings.Default.WebGrabConfigFileName);
                if (File.Exists(webGrabConfFile))
                {
                    var userConfig = new GrabConfiguration(configurationDirectory);
                    userConfig.Config = settings.LoadFromFile(webGrabConfFile);
                    userConfig.Config.Path = new FileInfo(webGrabConfFile);
                    var siteIniUserDir = new DirectoryInfo(Path.Combine(configurationDirectory.FullName, Settings.Default.SiteIniUser));
                    if (siteIniUserDir.Exists)
                    {
                        var listCountries = new ListCountries(siteIniUserDir);
                        var countries = listCountries.Execute();
                        userConfig.Countries = countries;
                    }
                    Log.Debug(string.Format("Loaded user config {0}", userConfig.Name));
                    return userConfig;
                }
                Log.WarnFormat("{0} not found in {1}", Settings.Default.WebGrabConfigFileName, configurationDirectory.FullName);
                return null;
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("Failed to load user configuration.{0}{1}", Environment.NewLine, ex.Message), ex);
                throw;
            }
        }
    }
}


