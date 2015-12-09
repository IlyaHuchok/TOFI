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
    public class ClientUnitOfWork : UnitOfWork, IClientUnitOfWork
    {
        private IUserRepository _userRepository;
        private IClientRepository _clientRepository;

        public ClientUnitOfWork(IKernel kernel) : base(kernel) { }

        public IUserRepository UserRepository
        {
            get
            {
                return _userRepository ?? _ninjectKernel.Get<IUserRepository>(new ConstructorArgument("context", GetContext()));
            }
        }

        public IClientRepository ClientRepository
        {
            get
            {
                return _clientRepository ?? _ninjectKernel.Get<IClientRepository>(new ConstructorArgument("context", GetContext()));
            }
        }
    }
}
