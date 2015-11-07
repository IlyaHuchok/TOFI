using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public int OperatorId { get; set; }
        public int CreditId { get; set; }
        public int ContractNo { get; set; }
        public int Amount { get; set; }
    }
}
