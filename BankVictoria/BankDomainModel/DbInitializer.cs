using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Entities;

namespace BankDomainModel
{
    public class DbInitializer : DropCreateDatabaseAlways<BankDbContext>
    {
        protected override void Seed(BankDbContext context)
        {
            AddBankAccount(context);

            base.Seed(context);
        }



        private void AddBankAccount(BankDbContext context)
        {
            var bankAccount = new BankAccount  //Bank Account
            {
                Balance = 40 * 1000 * 1000,
                Currency = "USD"
            };

            context.BankAccount = bankAccount;
            context.SaveChanges();
        }
    }
}
