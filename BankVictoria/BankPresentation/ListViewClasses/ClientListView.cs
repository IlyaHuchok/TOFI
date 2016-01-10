using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankPresentation.ListViewClasses
{
    public class ClientListView
    {
        public int RequestId { get; set; }
        public string CreditType { get; set; }
        public string Amount { get; set; }
        public string Status { get; set; }
    }
}
