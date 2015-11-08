using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Entities.Enums;

namespace Entities
{
    public class UserDB
    {
        [Key]
        public int UserId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }

        public virtual ICollection<RequestDB> Requests { get; set; }
        public virtual ICollection<ClientDB> Clients { get; set; }
    }
}
