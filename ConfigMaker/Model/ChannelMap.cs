using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConfigMaker.Model
{
    public class ChannelMap
    {
        public ChannelMap()
        {
            Configurations = new BindingList<GrabConfiguration>();
        }

        public BindingList<GrabConfiguration> Configurations { get; set; }

        public void SaveToFile(string fileName)
        {
         
              var userChannels = Configurations
                .SelectMany(x => x.Config.channel)
                .Distinct()
                .ToList();

            var allSites = Configurations.SelectMany(x => x.Countries)
                .SelectMany(x => x.Sites)
                .ToList();

            var joined = userChannels.Join(
                allSites,
                arg => arg.site.ToLower(),
                arg => arg.Path.Name.ToLower().Replace(".channels.xml", string.Empty),
                (first, second) => new { channel = first, site = second })
                .ToList();

            foreach (var item in joined)
            {
                item.channel.ParentSite = item.site;
            }

            var mapItems = userChannels.Select(x => new ChannelMapItem() {
                Country = x.ParentSite.Country.Name,
                Site = x.ParentSite.site1,
                XmltvId = x.xmltv_id,
                Comment = x.Value
            })
            .Distinct()
            .OrderBy(x=> x.Country)
            .ThenBy(x=>x.Site)
            .ThenBy(x=>x.XmltvId)
            .ToList();


            string previousCountry = null;
            string previousSite = null;

            var sb = new StringBuilder();
            sb.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            sb.AppendLine("<channels>");

            foreach (var item in mapItems)
            {
                if (previousCountry != null)
                {
                    if (!string.Equals(previousSite, item.Site))
                    {
                        sb.Append('\t');
                        sb.AppendLine(string.Format("<!-- {0} End -->", previousSite));
                    }
                    if (!string.Equals(previousCountry, item.Country))
                    {
                        sb.AppendLine(string.Format("<!-- {0} End -->", previousCountry));
                    }
                }
                if (!string.Equals(previousCountry, item.Country))
                {
                    sb.AppendLine(string.Format("<!-- {0} Start -->", item.Country));
                }
                if (!string.Equals(previousSite, item.Site))
                {
                    sb.Append('\t');
                    sb.AppendLine(string.Format("<!-- {0} Start -->", item.Site));
                }
                sb.Append('\t');
                sb.AppendLine(
                    string.Format("<channel id=\"{0}\"></channel><!-- {1} -->", 
                    System.Web.HttpUtility.HtmlEncode(item.XmltvId), 
                    item.Comment));
                previousCountry = item.Country;
                previousSite = item.Site;
            }

            if (mapItems.Count > 0)
            {
                sb.Append('\t');
                sb.AppendLine(string.Format("<!-- {0} End -->", mapItems.Last().Site));
                sb.AppendLine(string.Format("<!-- {0} End -->", mapItems.Last().Country));
            }
            sb.AppendLine("</channels>");
            System.IO.File.WriteAllText(fileName, sb.ToString(), new System.Text.UTF8Encoding(false));
        }

        private class ChannelMapItem
        {
            public string Country { get; set; }
            public string Site { get; set; }
            public string XmltvId { get; set; }
            public string Comment { get; set; }

            // override object.Equals
            public override bool Equals(object obj)
            {
                
                if (obj == null || GetType() != obj.GetType())
                {
                    return false;
                }
                var item = (ChannelMapItem)obj;
                return Equals(Country, item.Country) && Equals(Site, item.Site) && Equals(XmltvId, item.XmltvId) && Equals(Comment, item.Comment);
            }

            // override object.GetHashCode
                public override int GetHashCode()
            {
                return Tuple.Create(Country, Site, XmltvId, Comment).GetHashCode();
            }

        }
    }

}

