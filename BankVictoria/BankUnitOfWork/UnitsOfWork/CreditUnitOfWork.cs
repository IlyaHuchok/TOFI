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
    public class CreditUnitOfWork : UnitOfWork, ICreditUnitOfWork
    {
        private ICreditRepository _creditRepository;
        private IAccountRepository _accountRepository;
        private ICreditTypeRepository _creditTypeRepository;
        private IRequestRepository _requestRepository;

        public CreditUnitOfWork(IKernel kernel) : base(kernel){}


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
    }
}
