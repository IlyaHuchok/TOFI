﻿using System;
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
        public BankDbContext()
        {
            // remove this in don't want the data to be deleted
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<BankDbContext>());
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Credit> Credits { get; set;}
        public DbSet<CreditType> CreditTypes { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<RequestStatus> RequestStatuses { get; set; }

      protected override void OnModelCreating(DbModelBuilder modelBuilder)
      {
        modelBuilder.Entity<Credit>().HasRequired(x => x.Request).WithOptional(x => x.Credit);

        base.OnModelCreating(modelBuilder);
      }
    }
}
