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
    public class DeleteConfiguration
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
                ReportStatus("Deleting configuration", "");
                Log.Debug(string.Format("Deleting user configuration in {0}", configuration.Path.FullName));
                Directory.Delete(configuration.Path.FullName,true);
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("Failed to delete user configuration.{0}{1}", Environment.NewLine, ex.Message), ex);
                throw;
            }
        }

    }
}
