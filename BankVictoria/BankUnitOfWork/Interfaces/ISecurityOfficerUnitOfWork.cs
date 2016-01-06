using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWork;
using BankDAL.Interfaces;

namespace BankUnitOfWork.Interfaces
{
    public interface ISecurityOfficerUnitOfWork : IUnitOfWork
    {
        IClientRepository ClientRepository { get; }
        IUserRepository UserRepository { get; }
        IRequestRepository RequestRepository { get; }
        ICreditRepository CreditRepository { get; }
        IAccountRepository AccountRepository { get; }
    }
}
