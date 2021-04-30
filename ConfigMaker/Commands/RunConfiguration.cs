using ConfigMaker.Model;
using ConfigMaker.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigMaker.Commands
{
    public class RunConfiguration
    {
       public event StatusChangedEventHandler StatusChanged;

        private void ReportStatus(string status = null, string description = null)
        {
            StatusChanged?.Invoke(this, new StatusChangedEventArgs(status, description));
        }

        public Process Execute(GrabConfiguration userConfig, bool hideWindow)
        {
            try
            {
                ReportStatus("Running configuration", userConfig.Name);
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    Arguments = "\"" + userConfig.Path.FullName + "\"",
                    FileName = Settings.Default.WebGrabExePath,
                };
                if (hideWindow)
                {
                    startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                }
                return Process.Start(startInfo);
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("Failed to run configuration.{0}{1}", Environment.NewLine, ex.Message), ex);
                throw;
            }
        }
    }
}


