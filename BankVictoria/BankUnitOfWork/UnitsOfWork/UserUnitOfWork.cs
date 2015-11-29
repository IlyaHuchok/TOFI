﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankUnitOfWork.Interfaces;
using BankDAL.Interfaces;
using BankDomainModel;
using Ninject;
using Ninject.Parameters;

namespace BankUnitOfWork.UnitsOfWork
{
    public class UserUnitOfWork : UnitOfWork, IUserUnitOfWork
    {
        private IUserRepository _userRepository;

        public UserUnitOfWork(IKernel kernel) : base(kernel) { }

        public IUserRepository UserRepository
        {
            get
            {
                return _userRepository ?? _ninjectKernel.Get<IUserRepository>(new ConstructorArgument("context", GetContext()));
            }
        }
    }
}