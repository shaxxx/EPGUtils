using ConfigMaker.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigMaker.Model
{
    public class GrabConfiguration
    {
        public GrabConfiguration(DirectoryInfo path)
        {
            Path = path ?? throw new ArgumentNullException(nameof(path));
            Countries = new BindingList<Country>();
            Config = new settings();
        }

        public string Name { get => Path.Name; }

        public DirectoryInfo Path { get; private set; }

        public void SetPath(DirectoryInfo path)
        {
            Path = path;
        }

        public BindingList<Country> Countries { get; set; }

        public int TotalCountries
        {
            get
            {
                return Countries.Count;
            }
        }

        public int TotalSites
        {
            get
            {
                return Countries.SelectMany(x=> x.Sites).Count();
            }
        }

        public int TotalAvailableChannels
        {
            get
            {
                return Countries.SelectMany(x => x.Sites).SelectMany(x=>x.channels).Count();
            }
        }

        public settings Config { get; set; }

        public int TotalSelectedChannels
        {
            get
            {
                return Config?.channel.Count() ?? 0;
            }
        }

        public void LoadSettingsFromFile()
        {
          Config =  settings.LoadFromFile(System.IO.Path.Combine(Path.FullName, Settings.Default.WebGrabConfigFileName));
        }

        public void SaveToFile()
        {
            if (Config.license != null)
            {
                if (string.IsNullOrEmpty(Config.license.wgusername)
                    && string.IsNullOrEmpty(Config.license.password)
                    && string.IsNullOrEmpty(Config.license.registeredemail))
                {
                    Config.license = null;
                }
            }
            Config.SaveToFile(System.IO.Path.Combine(Path.FullName, Settings.Default.WebGrabConfigFileName));
        }

        public GrabConfiguration Clone()
        {
            return (GrabConfiguration)MemberwiseClone();
        }
    }
}
