using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Entities.Enums;

namespace Entities
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }

        public virtual ICollection<Request> Requests { get; set; }
        // public virtual ICollection<Client> Clients { get; set; } ??
    }
}
