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
    public  class UpdateSiteKeys
    {
        public  event StatusChangedEventHandler StatusChanged;

        public void Execute(DirectoryInfo configurationDir, settings source)
        {
            try
            {
                ReportStatus("Updating decryption keys", "");
                var dirs = configurationDir.GetDirectories();
                //go trough all user configurations
                foreach (var dir in dirs)
                {
                    Log.Debug(string.Format("Updating decryption keys for {0}", dir.FullName));
                    //check if WebGrab++.config.xml exists
                    var targetConfigFile = Path.Combine(dir.FullName, Settings.Default.WebGrabConfigFileName);
                    if (File.Exists(targetConfigFile))
                    {
                        //load target config
                        var targetConfig = settings.LoadFromFile(targetConfigFile);
                        //list all sites used in target channels
                        var selectedSites = targetConfig.channel.Select(x => x.site).Distinct().ToList();
                        foreach (var selectedSite in selectedSites)
                        {
                            //check if selected site has value in source config
                            var newKey = source.decryptkey.FirstOrDefault(x => x.site == selectedSite);
                            if (newKey != null)
                            {
                                //get existing site keys in target config for selected site
                                var oldKeys = targetConfig.decryptkey.Where(x => x.site == selectedSite).ToList();
                                foreach (var oldKey in oldKeys)
                                {
                                    //remove old key from target
                                    targetConfig.decryptkey.Remove(oldKey);
                                    Log.Debug(string.Format("Removed existing decryption key for {0}", selectedSite));
                                }
                                targetConfig.decryptkey.Add(newKey);
                                Log.Debug(string.Format("Updated decryption key for {0}", selectedSite));
                            }
                        }
                        targetConfig.SaveToFile(targetConfigFile);
                    }
                }
                Log.Debug("Finished updating decryption keys");
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
