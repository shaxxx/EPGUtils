using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ConfigMaker.Model
{
    public partial class site
    {
        [XmlIgnore()]
        public FileInfo Path { get; set; }

        [XmlIgnore()]
        public FileInfo IniPath
        {
            get
            {
                var siteFile = System.IO.Path.GetFileNameWithoutExtension(Path.FullName);
                var fileName = System.IO.Path.GetFileNameWithoutExtension(siteFile) + ".ini";
                var iniFile = System.IO.Path.Combine(Path.DirectoryName, fileName);
                return new FileInfo(iniFile);
            }
        }

        [XmlIgnore()]
        public Country Country { get; set; }
    }
}
