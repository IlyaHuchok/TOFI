using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace BankDomainModel
{
    public class CreditTypeDB
    {
        [Key]
        public int CreditTypeId { get; set; } // или Id?
        public string Name { get; set; }
        public string Type { get; set; }
        public int Time { get; set; }
        public int PercentPerYear { get; set; }
        public string Currency { get; set; }
        public int Fine { get; set; }
        public string Pledge { get; set; }
        public string Guarantee { get; set; }

        public virtual ICollection<CreditDB> Credits { get; set; }
    }
}
