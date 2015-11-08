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
        public DbSet<Client> Clients { get; set; }
        public DbSet<Credit> Credits { get; set;}
        public DbSet<CreditType> CreditTypes { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<RequestStatus> RequestStatuses { get; set; }
    }
}
