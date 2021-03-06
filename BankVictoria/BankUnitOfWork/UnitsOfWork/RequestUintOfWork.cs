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
    public class RequestUintOfWork : UnitOfWork, IRequestUintOfWork
    {
        private IRequestRepository _requestRepository;
        private IClientRepository _clientRepository;
        private ICreditRepository _creditRepository;
        private ICreditTypeRepository _creditTypeRepository;
        private IUserRepository _userRepository;
        public RequestUintOfWork(IKernel kernel) : base(kernel) { }

        public IClientRepository ClientRepository
        {
            get
            {
                if (_clientRepository == null)
                {
                    _clientRepository = _ninjectKernel.Get<IClientRepository>(new ConstructorArgument("context", GetContext()));
                }

                return _clientRepository;
            }
        }

        public ICreditRepository CreditRepository
        {
            get
            {
                if (_creditRepository == null)
                {
                    _creditRepository = _ninjectKernel.Get<ICreditRepository>(new ConstructorArgument("context", GetContext()));
                }

                return _creditRepository;
            }
        }

        public ICreditTypeRepository CreditTypeRepository
        {
            get
            {
                if (_creditTypeRepository == null)
                {
                    _creditTypeRepository = _ninjectKernel.Get<ICreditTypeRepository>(new ConstructorArgument("context", GetContext()));
                }

                return _creditTypeRepository;
            }
        }

        public IRequestRepository RequestRepository
        {
            get
            {
                if (_requestRepository == null)
                {
                    _requestRepository = _ninjectKernel.Get<IRequestRepository>(new ConstructorArgument("context", GetContext()));
                }

                return _requestRepository;
            }
        }

        public IUserRepository UserRepository
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = _ninjectKernel.Get<IUserRepository>(new ConstructorArgument("context", GetContext()));
                }

                return _userRepository;
            }
        }
    }
}
