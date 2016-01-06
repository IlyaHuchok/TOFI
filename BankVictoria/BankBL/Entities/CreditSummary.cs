using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Entities;

namespace BankBL.Entities
{
    public class CreditSummary
    {
        public Credit Credit { get; set; }

        public bool WasRepaidOnTime { get; set; }
    }
}
