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
    public class CreditTypeUnitOfWork : UnitOfWork, ICreditTypeUnitOfWork
    {
        private ICreditTypeRepository _creditTypeRepository;
        public CreditTypeUnitOfWork(IKernel kernel) : base(kernel) { }

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
    }
}
