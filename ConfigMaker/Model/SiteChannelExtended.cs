using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ConfigMaker.Model
{
    public partial class siteChannel
    {
        [XmlIgnore()]
        public site ParentSite { get; set; }

        [XmlIgnore()]
        public bool Selected { get; set; }

        [XmlIgnore()]
        public string UserXmltvId { get; set; }

        [XmlIgnore()]
        public bool Duplicate { get; set; }
    }
}
