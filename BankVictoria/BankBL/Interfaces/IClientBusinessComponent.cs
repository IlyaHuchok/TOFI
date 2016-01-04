using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Entities.Enums;
using Entities;
namespace BankBL.Interfaces
{
    public interface IClientBusinessComponent 
    {
        int Add(string username, string pass, UserRole userRole,
            string lastName, string name, string patronymic, DateTime birthday,
            string mobile, string email, string passportNo,
            DateTime passwordExpiration, string passportIdentityNo, string passportAuthority,
            string placeOfResidence, string registrationAddress);
        Client GetByID(int id);
        IList<Client> GetAll();
        int Count();
    }
}
