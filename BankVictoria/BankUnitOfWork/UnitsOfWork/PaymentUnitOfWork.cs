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
    }
}
