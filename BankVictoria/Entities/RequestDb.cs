using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class RequestDB
    {
        [Key]
        public int RequestId { get; set; }
        public int ClientId { get; set; }
        public int OperatorId { get; set; }
        public int SecurityId { get; set; }
        public int RequsetStatusId { get; set; }
        public string Status { get; set; } 
        public decimal AmountOfCredit { get; set; }
        public decimal Salary { get; set; }
        public string Pledge { get; set; } 
        public string Guarantee { get; set; }

        public virtual Client client { get; set; }
        public virtual UserDB User { get; set; }
        public virtual RequestStatusDB RequestStatus { get; set; }

    }
}
