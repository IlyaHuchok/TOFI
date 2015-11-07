using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Credit
    {
        public int CreditID { get; set; }
        public int ClientID { get; set; }
        public int CreditTypeID { get; set; }
        public int RequestID { get; set; }
        public int ContractNo { get; set; }
        public int Debt { get; set; }
        public int RemaingPayments { get; set; }
        public int AmountOfPayment { get; set; }
        public int Fine { get; set; }
    }
}
