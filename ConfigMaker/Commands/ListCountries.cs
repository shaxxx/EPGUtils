using ConfigMaker.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigMaker.Commands
{
    public class ListCountries
    {
        public DirectoryInfo TargetDirectory { get; }

        public event StatusChangedEventHandler StatusChanged;

        private void ReportStatus(string status = null, string description = null)
        {
            StatusChanged?.Invoke(this, new StatusChangedEventArgs(status, description));
        }

        public ListCountries(DirectoryInfo targetDirectory)
        {
            TargetDirectory = targetDirectory;
        }

        public  BindingList<Country> Execute()
        {
            return LoadCountries(TargetDirectory);
        }

        private BindingList<Country> LoadCountries(DirectoryInfo configDir)
        {
            try
            {
                ReportStatus("Searching configurations", "");
                Log.Debug(string.Format("Searching for countries in {0}", configDir.FullName));
                var countries = new BindingList<Country>();
                foreach (var dir in configDir.GetDirectories())
                {
                    var country = new Country(dir);
                    var sites = LoadSitesForCountry(country);
                    if (sites.Count > 0)
                    {
                        country.Sites = sites;
                        countries.Add(country);
                        Log.Debug(string.Format("Found country {0} in {1}", country.Name, configDir.FullName));
                    }
                }
                Log.Debug(string.Format("Found {0} countries in {1}", countries.Count, configDir.FullName));
                return countries;
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("Search for countries failed.{0}{1}", Environment.NewLine, ex.Message), ex);
                throw;
            }
        }

        private BindingList<site> LoadSitesForCountry(Country country)
        {
            var sites = new BindingList<site>();
            foreach (var config in country.Path.GetFiles("*.channels.xml"))
            {
                var site = LoadSiteConfig(config);
                if (site != null)
                {
                    //CheckSiteIniExists(country.Path, site);
                    site.Country = country;
                    sites.Add(site);
                    Log.Debug(string.Format("Found site {0} in {1}", site.site1, country.Path.FullName));
                }
            }
            Log.Debug(string.Format("Found {0} sites in {1}", sites.Count, country.Path.FullName));
            return sites;
        }

        private site LoadSiteConfig(FileInfo siteConfigFile)
        {
            Log.Debug(string.Format("Loading site {0}", siteConfigFile.FullName));
            try
            {
                var siteConfig = site.LoadFromFile(siteConfigFile.FullName);
                siteConfig.Path = siteConfigFile;
                foreach (var channel in siteConfig.channels)
                {
                    channel.ParentSite = siteConfig;
                }
                Log.Debug(string.Format("Site {0} loaded", siteConfig.site1));
                return siteConfig;
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("Failed to load site config{2}{0}{1}", Environment.NewLine, ex.Message, siteConfigFile), ex);
                return null;
            }

        }

        private bool CheckSiteIniExists(DirectoryInfo countryDirectory, site site)
        {
            if (!site.IniPath.Exists)
            {
                Log.Error(string.Format("Missing ini file for site {0}", site.site1));
                throw new FileNotFoundException(string.Format("Site ini {0} is missing.", site.IniPath.FullName));
                return false;
            }
            return true;
        }

    }
}
