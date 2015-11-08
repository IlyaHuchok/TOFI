using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Entities;
namespace BankDomainModel
{
    public class BankDbContext : DbContext 
    { 
        public DbSet<ClientDB> Client { get; set; }
        public DbSet<CreditDB> Credit { get; set;}
        public DbSet<CreditTypeDB> CreditType { get; set; }
        public DbSet<RequestDB> Request { get; set; }
        public DbSet<PaymentDB> Payment { get; set; }
        public DbSet<UserDB> User { get; set; }
    }
}
