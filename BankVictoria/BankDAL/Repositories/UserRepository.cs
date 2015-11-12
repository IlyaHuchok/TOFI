using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Repositories;
using Entities;
using BankDAL.Interfaces;
using BankDomainModel;

namespace BankDAL.Repositories
{
    public class UserRepository : GenericDataRepository<User, BankDbContext>, IUserRepository
    {
        public UserRepository(BankDbContext context)
            : base(context)
        {
        }
    }
}
