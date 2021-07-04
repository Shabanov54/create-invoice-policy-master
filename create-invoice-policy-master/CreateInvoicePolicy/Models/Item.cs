using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CreateInvoicePolicy.Models
{
    public class Item
    {
        [XmlElement("CODE")]
        public int Code;
        [XmlElement("NAME")]
        public string Name;
        [XmlElement("VENDOR")]
        public string Vendor;
        [XmlElement("QTTY")]
        public int Qtty;
        [XmlElement("SPRICE")]
        public string SPrice;
        [XmlElement("NDS")]
        public float Nds;
        [XmlElement("RPRICE")]
        public float RPrice;
    }
}
