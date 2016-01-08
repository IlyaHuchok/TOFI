using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnitOfWork;
using BankDAL.Interfaces;

namespace BankUnitOfWork.Interfaces
{
    public interface ICreditTypeUnitOfWork : IUnitOfWork
    {
        ICreditTypeRepository CreditTypeRepository { get; }
    }
}
