using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace UnitOfWork
{
    public abstract class UnitOfWork<TContext> : IUnitOfWork
        where TContext : IDisposable, new()
    {
        // WHEN CREATING CONCRETE IMPLEMENTATION
        // ADD NECESSARY REPOSITORIES LIKE:
        // private readonly IUserRepository UserRepository;
        // AND ADD THEIR INTIAZLIZATION IN CONSTRUCTORS
        protected TContext _context;

        public abstract void Save();
            //_context.SaveChanges();

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
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
