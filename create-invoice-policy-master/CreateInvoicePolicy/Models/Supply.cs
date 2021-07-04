using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CreateInvoicePolicy.Models
{
    public class Supply
    {
        [XmlIgnore]
        public string FolderName;
        [XmlIgnore]
        public string FileName;
        [XmlElement("INVOICE_NUM")]
        public int InvoiceNum;
        [XmlElement("INVOICE_DATE")]
        public string InvoiceDate;
        [XmlElement("DEP_ID")]
        public int DepId;
        [XmlArray("ITEMS")]
        [XmlArrayItem("ITEM")]
        public List<Item> Items = new List<Item>();
    }
}
