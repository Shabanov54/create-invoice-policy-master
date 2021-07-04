using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateInvoicePolicy
{
    class Program
    {
        static void Main(string[] args)
        {
            Builder builder = new Builder();
            builder.BuildInvoices();

            Console.ReadKey();
        }
    }
}
