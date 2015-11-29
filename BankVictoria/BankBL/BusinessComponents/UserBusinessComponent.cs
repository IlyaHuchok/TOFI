using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.BusinessComponents;
using BankBL.Interfaces;
using BankUnitOfWork.Interfaces;
using Entities;

namespace BankBL.BusinessComponents
{
    public class UserBusinessComponent : GenericBusinessComponent<IUserUnitOfWork>, IUserBusinessComponent
    {
        public UserBusinessComponent(IUserUnitOfWork unitOfWork) : base(unitOfWork) { }

        public bool Login(string name, string pass)
        {
            return true;//_unitOfWork.GetUserRepository
        }

        /// TODO: Add remaining methods here
    }
}
