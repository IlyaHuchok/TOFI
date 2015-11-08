using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Account
    {
        public int AccountId { get; set; }
        public decimal Balance { get; set; }
        public int ClientId { get; set; }

        public virtual Client Client { get; set; }
    }
}
