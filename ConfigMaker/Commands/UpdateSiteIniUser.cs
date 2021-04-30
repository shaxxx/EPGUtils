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
    public  class UpdateSiteIniUser
    {
        public  event StatusChangedEventHandler StatusChanged;

        public void Execute(GrabConfiguration grabConfiguration)
        {
            try
            {
                ReportStatus("Updating siteini.user", "");
                Log.Debug(string.Format("Updating siteini.user for {0}", grabConfiguration.Name));
                var siteIniUserDir = new DirectoryInfo(Path.Combine(grabConfiguration.Path.FullName, Settings.Default.SiteIniUser));
                var listUserCountries = new ListCountries(siteIniUserDir);
                var userCountries = listUserCountries.Execute();
                var sitePackDir = new DirectoryInfo(Path.Combine(Locations.WorkingDirectory.FullName, Settings.Default.SiteIniPack));
                var listAllCountries = new ListCountries(sitePackDir);
                var allCountries = listAllCountries.Execute();
                var selectedSites = userCountries.SelectMany(x => x.Sites).ToList();
                var allSites = allCountries.SelectMany(x => x.Sites).ToList();
                for (int i = 0; i < selectedSites.Count; i++)
                {
                    ReportStatus("Updating siteini.user", string.Format("{0} / {1}",i + 1, selectedSites.Count));
                    site item = selectedSites[i];
                    var siteXmlPack = allSites.SingleOrDefault(x => x.Path.Name == item.Path.Name);
                    if (siteXmlPack != null)
                    {
                        File.Copy(siteXmlPack.Path.FullName, item.Path.FullName,true);
                        if (siteXmlPack.IniPath.Exists)
                        {
                            File.Copy(siteXmlPack.IniPath.FullName, item.IniPath.FullName, true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("Search for configurations failed.{0}{1}", Environment.NewLine, ex.Message), ex);
                throw;
            }
        }

        private void ReportStatus(string status = null, string description = null)
        {
            StatusChanged?.Invoke(this, new StatusChangedEventArgs(status, description));
        }
    }
}
