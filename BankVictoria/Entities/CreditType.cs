using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace Entities
{
    public class CreditType
    {
        [Key]
        public int CreditTypeId { get; set; } // или Id?
        public string Name { get; set; }
        public string Type { get; set; }
        public int Time { get; set; }
        public int PercentPerYear { get; set; }
        public string Currency { get; set; }
        public decimal Fine { get; set; }
        public bool Pledge { get; set; }
        public bool Guarantee { get; set; }

        public virtual ICollection<Credit> Credits { get; set; }
    }
}
