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
    public class RequestUintOfWork : UnitOfWork, IRequestUintOfWork
    {
        private IRequestRepository _requestRepository;
        public RequestUintOfWork(IKernel kernel) : base(kernel) { }
         
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
