using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BankDomainModel
{
    public class RequestDB
    {
        [Key]
        public int RequestId { get; set; }// или Id?
        public int ClientId { get; set; }
        public int OperatorId { get; set; }
        public int SecurityId { get; set; }
        public string Status { get; set; } //string?
        public int AmountOfCredit { get; set; } //int ?
        public int Salary { get; set; }
        public string Pledge { get; set; } // залог string?
        public string Guarantee { get; set; }

        public virtual CreditDB Client { get; set; }
        public virtual UserDB User { get; set; }
        

    }
}
