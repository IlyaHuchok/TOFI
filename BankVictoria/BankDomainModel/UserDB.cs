using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace BankDomainModel
{
    public class UserDB
    {
        [Key]
        public int UserId { get; set; }// или Id?
        public string Login { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        public virtual ICollection<RequestDB> Requests { get; set; }
    }
}
