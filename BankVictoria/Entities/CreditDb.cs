using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace Entities
{
    public class CreditDB
    {
        [Key]
        public int CreditId { get; set; }
        public int ClientId { get; set; }
        public int CreditTypeId { get; set; }
        public int RequestId { get; set; }
        public int ContractNo { get; set; }
        public decimal Debt { get; set; }
        public int RemaingPayments { get; set; }
        public decimal AmountOfPayment { get; set; }
      //  public int Fine { get; set; } 

        public virtual ClientDB Client { get; set; }
        public virtual RequestDB Request { get; set; }
        public virtual CreditTypeDB CreditTypes { get; set; }

        public virtual ICollection<PaymentDB> Payments { get; set; }
    }
}
