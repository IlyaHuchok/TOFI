using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using Entities;

namespace BankDAL.Interfaces
{
    interface IUserRepository : IGenericDataRepository<User>
    {
    }
}
