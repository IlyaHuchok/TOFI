using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.BusinessComponents;
using BankBL.Interfaces;
using BankUnitOfWork.Interfaces;
using Entities;
using Entities.Enums;
//using BankDAL.Repositories;

namespace BankBL.BusinessComponents
{
    public class UserBusinessComponent : GenericBusinessComponent<IUserUnitOfWork>, IUserBusinessComponent
    {
        public UserBusinessComponent(IUserUnitOfWork unitOfWork) : base(unitOfWork) { }

        public UserRole? Login(string name, string pass)
        {
            var user = _unitOfWork.UserRepository.GetSingle(x => x.Login == name && x.Password == pass);
            if (user == null)
            {
                return null;
            }
            else
            {
                return user.Role;
            }
        }

        /// TODO: Add remaining methods here
    }
}
