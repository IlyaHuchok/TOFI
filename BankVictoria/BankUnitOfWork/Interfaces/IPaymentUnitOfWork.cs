using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnitOfWork;
using BankDAL.Interfaces;
namespace BankUnitOfWork.Interfaces
{
    public interface IPaymentUnitOfWork : IUnitOfWork
    {
        IPaymentRepository PaymentRepository { get; }
        ICreditRepository CreditRepository { get; }
        IUserRepository UserRepository { get; }
    }
}
