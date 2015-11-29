using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWork;
using BankDomainModel;
using Ninject;

namespace BankUnitOfWork
{
    public class UnitOfWork : UnitOfWork<BankDbContext>
    {
        protected readonly IKernel _ninjectKernel;

        protected BankDbContext GetContext()
        {
            return _context ?? _ninjectKernel.Get<BankDbContext>();
        }

        public UnitOfWork(IKernel ninjectKernel)
        {
            _ninjectKernel = ninjectKernel;
        }

        public override void Save()
        {
            _context.SaveChanges();
        }
    }
}
