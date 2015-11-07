using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class CreditType
    {
        public int CreditTypeId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int Time { get; set; }
        public int PercentPerYear { get; set; }
        public string Currency { get; set; }
        public int Fine { get; set; }
        public string Pledge { get; set; } 
        public string Guarantee { get; set; }

    }
}
