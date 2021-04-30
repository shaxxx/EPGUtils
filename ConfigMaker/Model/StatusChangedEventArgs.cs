using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigMaker.Model
{
    public delegate void StatusChangedEventHandler(Object sender, StatusChangedEventArgs e);

    public class StatusChangedEventArgs
    {
        public StatusChangedEventArgs(string status = null, string description = null)
        {
            Status = status;
            Description = description;
        }

        public string Status { get; }
        public string Description { get; }
    }
}
