﻿using System;
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
       // public string Type { get; set; }
        public int TimeMonths { get; set; }
        public decimal PercentPerYear { get; set; }
        public string Currency { get; set; }
        public decimal FinePercent { get; set; }
        public decimal MinAmount { get; set; }
        public decimal MaxAmount { get; set; }
        /// <summary>
        /// If we don't want a credit type to be available for new credits
        /// but still want to support it for existing credits we set its IsAvailable = false
        /// </summary>
        public bool IsAvailable { get; set; }
        //public bool Pledge { get; set; }
        //public bool Guarantee { get; set; }

        //public virtual ICollection<Credit> Credits { get; set; }
    }
}
