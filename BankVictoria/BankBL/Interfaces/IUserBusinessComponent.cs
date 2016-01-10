using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Entities;
using Entities.Enums;

namespace BankBL.Interfaces
{
    public interface IUserBusinessComponent
    {
        /// TODO: Add remaining methods here
        UserRole? Login(string name, string pass);
        int Add(string name, string pass, UserRole userRole);
        bool Exists(string name, string pass);
        int GetIdByLogin(string name);

        IEnumerable<User> GetAll();
        void Update(User user);

        void Delete(User user);
    }
}
