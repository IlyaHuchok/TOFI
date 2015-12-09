using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWork;
using BankDAL.Interfaces;

namespace BankUnitOfWork.Interfaces
{
    public interface IClientUnitOfWork : IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        IClientRepository ClientRepository { get; }
    }
}
