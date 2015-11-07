using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Request
    {
        public int RequestId { get; set; }
        public int ClientId { get; set; }
        public int OperatorId { get; set; }
        public int SecurityId { get; set; }
        public string Status { get; set; } //string?
        public int AmountOfCredit { get; set; } //int ?
        public int Salary { get; set; }
        public string Pledge { get; set; } // залог string?
        public string Guarantee { get; set; }
    }
}
