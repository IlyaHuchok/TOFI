using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Request
    {
        [Key]
        public int RequestId { get; set; }
        public int ClientId { get; set; }
        public int OperatorId { get; set; }
        public int SecurityServiceEmployeeId { get; set; }
        public int RequestStatusId { get; set; }
        public string Status { get; set; } 
        public decimal AmountOfCredit { get; set; }
        public decimal Salary { get; set; }
        public string Pledge { get; set; } 
        public string Guarantee { get; set; }

        public virtual Client Client { get; set; }
        public virtual User Operator { get; set; }
        public virtual User SecurityServiceEmployee { get; set; }
        public virtual RequestStatus RequestStatus { get; set; }

    }
}
