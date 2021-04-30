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
    public class RunAllConfigurations
    {
       public event StatusChangedEventHandler StatusChanged;

        private void ReportStatus(string status = null, string description = null)
        {
            StatusChanged?.Invoke(this, new StatusChangedEventArgs(status, description));
        }

        public void Execute(IList<GrabConfiguration> configurations, bool hideWindow)
        {
            var runConfCommand = new RunConfiguration();
            runConfCommand.StatusChanged += (sender,e)=> ReportStatus(e.Status, e.Description);
            foreach (var userConf in configurations)
            {
               var p =  runConfCommand.Execute(userConf,hideWindow);
                p.WaitForExit();
            }
        }
    }
}


