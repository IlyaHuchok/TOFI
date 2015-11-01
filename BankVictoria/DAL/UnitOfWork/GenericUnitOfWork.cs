using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DAL.UnitOfWork
{
    public class UnitOfWork<TContext> : IUnitOfWork
        where TContext : DbContext, new()
    {
        // WHEN CREATING CONCRETE IMPLEMENTATION
        // ADD NECESSARY REPOSITORIES LIKE:
        // private readonly IUserRepository UserRepository;
        // AND ADD THEIR INTIAZLIZATION IN CONSTRUCTORS
        protected readonly TContext _context;

        public UnitOfWork(TContext context)
        {
            _context = context;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

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
