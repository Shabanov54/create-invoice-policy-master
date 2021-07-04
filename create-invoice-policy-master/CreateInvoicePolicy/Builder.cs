using CreateInvoicePolicy.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateInvoicePolicy
{
    public class Builder
    {
        private int _invoiceNum;
        private readonly FileContext _fileContext; 
        public Builder()
        {
            _fileContext = new FileContext();
            _invoiceNum = _fileContext.GetLastInvoiceNum();
        }
        public void BuildInvoices()
        {
            string dateForInvoiceFileName = _fileContext.GetDateForInvoiceFileName();
            string[] rowDateForInvoice = _fileContext.GetRowDateForInvoice(dateForInvoiceFileName);
            List<Packet> packets = GetPackets(rowDateForInvoice);
            foreach (var packet in packets)
            {

                _fileContext.SavePacket(packet);
                Console.WriteLine($"Пакет {packet.Supply.FileName} сохранен в папке {packet.Supply.FolderName}");
            }
        }

        private List<Packet> GetPackets(string[] rowDateForInvoice)
        {
            var packets = new List<Packet>();
            foreach (var row in rowDateForInvoice)
            {
                Packet newPacket = GetPacketInRow(row);
                var packet = packets.FirstOrDefault(p => p.Supply.DepId == newPacket.Supply.DepId);
                if (packet != null)
                {
                    
                    foreach (var item in newPacket.Supply.Items)
                    {
                        packet.Supply.Items.Add(item);
                    }
                }
                else
                {
                    newPacket.Supply.InvoiceNum = _invoiceNum;
                    newPacket.InvoiceNumAtr = _invoiceNum;
                    newPacket.Type = "12";
                    newPacket.Name = "Электронная накладная";
                    newPacket.From = "Росгосстрах";
                    _invoiceNum++;
                    packets.Add(newPacket);
                }
            }
            return packets;
        }

        private Packet GetPacketInRow(string row)
        {
            Packet packet = new Packet();
            string[] fields = row.Split(';');

            packet.Supply.DepId = int.Parse(fields[1]);
            packet.Supply.FolderName = fields[8];
            packet.Supply.FileName = $"{fields[1]}_Росгосстрах_{_invoiceNum}.xml";
            packet.Supply.InvoiceDate = fields[7];

            var item = new Item();
            item.Code = int.Parse(fields[2]);
            item.Name = fields[10];
            item.Qtty = int.Parse(fields[3]);
            item.SPrice = fields[4].Replace('.',',');
            item.RPrice = float.Parse(fields[5]);
            item.Nds = float.Parse(fields[6]);
            packet.Supply.Items.Add(item);

            return packet;
        }
    }
}
