using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.UnitOfWork;

namespace BL.BusinessComponents
{
    public abstract class GenericBusinessComponent : IDisposable
    {
        protected readonly IUnitOfWork _unitOfWork;

        public GenericBusinessComponent(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _unitOfWork.Dispose();
                }
            }

            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
