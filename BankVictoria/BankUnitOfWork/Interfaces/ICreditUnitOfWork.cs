using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWork;
using BankDAL.Interfaces;
namespace BankUnitOfWork.Interfaces
{
    public interface ICreditUnitOfWork : IUnitOfWork
    {
        ICreditRepository CreditRepository { get; }
        IAccountRepository AccountRepository { get; }
        ICreditTypeRepository CreditTypeRepository { get; }
        IRequestRepository RequestRepository { get; }
    }
}
