using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigMaker.Model
{
    public class Country
    {
        public Country(DirectoryInfo path)
        {
            Path = path ?? throw new ArgumentNullException(nameof(path));
            Sites = new BindingList<site>();
        }

        public string Name { get => Path.Name; }

        public DirectoryInfo Path { get; }

        public BindingList<site> Sites { get; set; }

        public bool Selected { get; set; }
    }
}
