using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class BankAccount
    {
        [Key]
        public int BankAccountId { get; set; }
        public string Currency { get; set; }
        public decimal Balance { get; set; }
    }
}
