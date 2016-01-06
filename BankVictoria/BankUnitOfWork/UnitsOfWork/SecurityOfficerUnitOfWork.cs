using System;
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
    public class SecurityOfficerUnitOfWork : UnitOfWork, ISecurityOfficerUnitOfWork
    {
        private IUserRepository _userRepository;
        private IClientRepository _clientRepository;
        private IRequestRepository _requestRepository;
        private ICreditRepository _creditRepository;
        private IAccountRepository _accountRepository;

        public SecurityOfficerUnitOfWork(IKernel kernel) : base(kernel) { }

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

        public IAccountRepository AccountRepository
        {
            get
            {
                if (_accountRepository == null)
                {
                    _accountRepository = _ninjectKernel.Get<IAccountRepository>(new ConstructorArgument("context", GetContext()));
                }

                return _accountRepository;
            }
        }
    }
}
