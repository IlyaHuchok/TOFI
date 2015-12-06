using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Account
    {
        [Key]
        public int AccountId { get; set; }
        public decimal Balance { get; set; }
        public int? ClientId { get; set; } //nullable so to allow bank account record to have no client info

        public virtual Client Client { get; set; }
    }
}
