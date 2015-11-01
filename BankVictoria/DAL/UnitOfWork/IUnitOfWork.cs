using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DAL.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        // WHEN CREATING CONCRETE INTERFACES FOR UNITS OF WORK
        // ADD NECESSARY REPOSITORIES WITH THEIR GETTERS LIKE:
        // SomeRepository SomeRepository { get; }
        void Save();
    }
}
