using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class ClientInfoDB
    {
        [Key] 
        public int ClientId { get; set; }
        public int UserId { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public DateTime Birthday { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string PassportNo { get; set; } 
        public string PassportIdentificationNo { get; set; }
        public string PassportAuthority { get; set; }
        public DateTime PassportExpirationDate { get; set; }
        public string PlaceOfResidence { get; set; }
        public string RegistrationAddress { get; set; }
        public string MotherSurname { get; set; }

        public virtual UserDB User { get; set; }

        public virtual ICollection<RequestDB> Requests { get; set; }
        public virtual ICollection<CreditDB> Credits { get; set; }
    }
}
