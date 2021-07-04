using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CreateInvoicePolicy.Models
{
    [XmlRoot("PACKET")]
    public class Packet
    {
        [XmlAttribute("TYPE")]
        public string Type;
        [XmlAttribute("ID")]
        public int InvoiceNumAtr;
        [XmlAttribute("NAME")]
        public string Name;
        [XmlAttribute("FROM")]
        public string From;
        [XmlElement("SUPPLY")]
        public Supply Supply = new Supply();
    }
}
