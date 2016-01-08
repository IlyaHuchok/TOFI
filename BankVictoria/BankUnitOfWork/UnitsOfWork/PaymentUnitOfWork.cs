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
    public class PaymentUnitOfWork : UnitOfWork, IPaymentUnitOfWork
    {
        private IPaymentRepository _paymentRepository;
        private ICreditRepository _creditRepository;
        private IUserRepository _userRepository;
        public PaymentUnitOfWork(IKernel kernel) : base(kernel) { }

        public IPaymentRepository PaymentRepository
        {
            get
            {
                if (_paymentRepository == null)
                {
                    _paymentRepository = _ninjectKernel.Get<IPaymentRepository>(new ConstructorArgument("context", GetContext()));
                }

                return _paymentRepository;
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
