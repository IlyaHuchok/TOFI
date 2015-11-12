using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace Entities
{
    public class Credit
    {
        [Key]
        public int CreditId { get; set; }
        public int AccountId { get; set; }
        public int CreditTypeId { get; set; }
        public int RequestId { get; set; }
        public int ContractNo { get; set; }
        public decimal Debt { get; set; }
        public int RemaingPayments { get; set; }
        public decimal AmountOfPayment { get; set; }
        //  public int Fine { get; set; } 

        public virtual Account Account { get; set; }
        public virtual CreditType CreditType { get; set; }
        public virtual Request Request { get; set; }

        public virtual ICollection<Payment> Payments { get; set; }
    }
}
