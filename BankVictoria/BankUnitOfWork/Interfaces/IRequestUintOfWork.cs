﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWork;
using BankDAL.Interfaces;
namespace BankUnitOfWork.Interfaces
{
    public interface IRequestUintOfWork : IUnitOfWork
    {
        IRequestRepository RequestRepository { get; }
        IClientRepository ClientRepository { get; }
        ICreditRepository CreditRepository { get; }
        ICreditTypeRepository CreditTypeRepository { get; }
        IUserRepository UserRepository { get; }
    }
}
