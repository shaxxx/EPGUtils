using ConfigMaker.Model;
using ConfigMaker.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigMaker.Commands
{
    public class SaveConfiguration
    {
        public event StatusChangedEventHandler StatusChanged;

        private void ReportStatus(string status = null, string description = null)
        {
            StatusChanged?.Invoke(this, new StatusChangedEventArgs(status, description));
        }

        public void Execute(GrabConfiguration configuration)
        {
            try
            {
                ReportStatus("Saving configuration", "");
                Log.Debug(string.Format("Saving user configuration in {0}", configuration.Path.FullName));
                var siteIniUserDir = new DirectoryInfo(Path.Combine(configuration.Path.FullName, Settings.Default.SiteIniUser));
                Directory.CreateDirectory(siteIniUserDir.FullName);
                DeleteOldCountries(siteIniUserDir, configuration);
                DeleteOldSites(siteIniUserDir, configuration);
                AddMissingSites(siteIniUserDir, configuration);
                Directory.CreateDirectory(configuration.Path.FullName);
                configuration.SaveToFile();
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("Failed to save user configuration.{0}{1}", Environment.NewLine, ex.Message), ex);
                throw;
            }
        }

        private void DeleteOldCountries(DirectoryInfo siteIniUserDir, GrabConfiguration configuration)
        {
            var directories = siteIniUserDir.GetDirectories();
            foreach (var dir in directories)
            {
                if (!configuration.Countries.Any(x => x.Path.Name.ToLower() == dir.Name.ToLower()))
                {
                    dir.Delete(true);
                }
            }
        }

        private void DeleteOldSites(DirectoryInfo siteIniUserDir, GrabConfiguration configuration)
        {
            var countries = siteIniUserDir.GetDirectories();
            var selectedSites = configuration.Countries.SelectMany(x => x.Sites).ToList();
            foreach (var country in countries)
            {
                var files = country.GetFiles();
                var countrySites = selectedSites.Where(x => x.Country.Name.ToLower() == country.Name.ToLower()).ToList();
                if (countrySites.Count > 0)
                {
                    foreach (var file in files)
                    {
                        if (Path.GetExtension(file.Name).ToLower() == ".xml")
                        {
                            if (!countrySites.Any(x => x.Path.Name == file.Name))
                            {
                                file.Delete();
                            }

                        }
                        else if (Path.GetExtension(file.Name).ToLower() == ".ini")
                        {
                            if (!countrySites.Any(x => x.IniPath.Name == file.Name))
                            {
                                file.Delete();
                            }
                        }
                    }
                }
            }
        }

        private void AddMissingSites(DirectoryInfo siteIniUserDir, GrabConfiguration configuration)
        {
            var selectedSites = configuration.Countries.SelectMany(x => x.Sites).ToList();
            var countries = siteIniUserDir.GetDirectories();
            foreach (var site in selectedSites)
            {
                if (site.Path.Exists)
                {
                    var targetXmlFile = Path.Combine(siteIniUserDir.FullName,  site.Country.Name, site.Path.Name);
                    if (!File.Exists(targetXmlFile))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(targetXmlFile));
                        File.Copy(site.Path.FullName,  targetXmlFile);
                    }
                }
                if (site.IniPath.Exists)
                {
                    var targetIniFile = Path.Combine(siteIniUserDir.FullName, site.Country.Name, site.IniPath.Name);
                    if (!File.Exists(targetIniFile))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(targetIniFile));
                        File.Copy(site.IniPath.FullName, targetIniFile);
                    }            
                }
            }
        }

        private void CheckBackupDirExists()
        {

        }
    }
}
