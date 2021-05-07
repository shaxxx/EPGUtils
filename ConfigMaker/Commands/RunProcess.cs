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
    public class RunProcess
    {
       public event StatusChangedEventHandler StatusChanged;

        private void ReportStatus(string status = null, string description = null)
        {
            StatusChanged?.Invoke(this, new StatusChangedEventArgs(status, description));
        }

        public Process Execute(string fileName, string arguments, bool hideWindow)
        {
            try
            {
                ReportStatus("Running ", Path.GetFileName(fileName));
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    Arguments = arguments,
                    FileName = fileName,
                };
                if (hideWindow)
                {
                    startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                }
                return Process.Start(startInfo);
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("Failed to run {0}{1}{2}", Path.GetFileName(fileName), Environment.NewLine, ex.Message), ex);
                throw;
            }
        }
    }
}


