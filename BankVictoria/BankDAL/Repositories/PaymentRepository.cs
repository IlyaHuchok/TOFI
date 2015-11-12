using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Repositories;
using Entities;
using BankDAL.Interfaces;
using BankDomainModel;

namespace BankDAL.Repositories
{
    public class PaymentRepository : GenericDataRepository<Payment, BankDbContext>, IPaymentRepository
    {
        public PaymentRepository(BankDbContext context)
            : base(context)
        {
        }
    }
}
