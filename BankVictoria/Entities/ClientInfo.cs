using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Client
    {
        public int ClientId { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public DateTime Birthday { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public int PassportNo { get; set; } //int?
        public int PassportIdentificationNo { get; set; }//int?
        public string PassportAuthority { get; set; }
        public DateTime PassportExpirationDate { get; set; }
        public string PlaceOfResidence { get; set; }
        public string RegistrationAddress { get; set; }
        public string MotherSurname { get; set; }

        //password ?


    }
}
