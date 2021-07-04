using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using CreateInvoicePolicy.Models;

namespace CreateInvoicePolicy
{
    public class FileContext
    {
        internal string GetDateForInvoiceFileName()
        {
            return @"E:\files\packets.csv";
        }

        internal string[] GetRowDateForInvoice(string dateForInvoiceFileName)
        {
            return File
                .ReadAllLines(dateForInvoiceFileName,Encoding.GetEncoding(866))
                .Skip(1)
                .ToArray();
        }

        internal int GetLastInvoiceNum()
        {
            return 1;
        }

        internal void SavePacket(Packet packet)
        {
            var workDir = Directory.GetCurrentDirectory();
            var fullFileName = Path.Combine(workDir,"OUT", packet.Supply.FolderName, packet.Supply.FileName);
            packet.Supply.FileName = fullFileName;
            CreateFolderIfNotExists(packet.Supply.FileName);

            XmlSerializer serializer = new XmlSerializer(typeof(Packet));
            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add("", "");
            TextWriter writer = new StreamWriter(packet.Supply.FileName, true,  Encoding.GetEncoding(1251));
            serializer.Serialize(writer, packet, namespaces);
            writer.Close();

            //XDocument doc = new XDocument(
            //    new XDeclaration("1.0", "windows-1251", "yes0"),
            //    new XElement("Root", "PACKET"));


        }

        private void CreateFolderIfNotExists(string fileName)
        {
            var firstLevDir = Path.GetDirectoryName(fileName);
            DirectoryInfo directoryInfo = new DirectoryInfo(firstLevDir);
            var secondLevDir = directoryInfo.Parent.ToString();
            if (!Directory.Exists(secondLevDir))
            {
                Directory.CreateDirectory(secondLevDir);
            }
            if (!Directory.Exists(firstLevDir))
            {
                Directory.CreateDirectory(firstLevDir);
            }
        }
    }
}
