﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace Entities
{
    public class PaymentDB
    {
        [Key]
        public int PaymentId { get; set; }
        public int OperatorId { get; set; }
        public int CreditId { get; set; }
        public int ContractNo { get; set; }
        public decimal Amount { get; set; }

        public virtual CreditDB Credits { get; set; }
        public virtual UserDB Users { get; set; }
    }
}
