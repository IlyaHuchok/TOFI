﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWork;

namespace BL.BusinessComponents
{
    public abstract class GenericBusinessComponent<TUnitOfWork> : IDisposable
            where TUnitOfWork : IUnitOfWork
    {
        protected readonly TUnitOfWork _unitOfWork;


        //public DoMuchSHit()
        //{
        //    _unitOfWork.Repository1.Method45();
        //    _unitOfWork.Repository2.Method456();
        //    _unitOfWork.Repository5.Method7745();

        //    _unitOfWork.Save();
        //}
        public GenericBusinessComponent(TUnitOfWork unitOfWork)
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
