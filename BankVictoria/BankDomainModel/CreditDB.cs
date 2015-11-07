using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace BankDomainModel
{
    public class CreditDB
    {
        [Key]
        public int CreditID { get; set; }// или Id?
        public int ClientID { get; set; }
        public int CreditTypeID { get; set; }
        public int RequestID { get; set; }
        public int ContractNo { get; set; }
        public int Debt { get; set; }
        public int RemaingPayments { get; set; }
        public int AmountOfPayment { get; set; }
        public int Fine { get; set; } 

        public virtual ClientDB Client { get; set; }
        public virtual RequestDB Request { get; set; }
        public virtual CreditTypeDB CreditTypes { get; set; }

        public virtual ICollection<PaymentDB> Payments { get; set; }
    }
}
