﻿using System;
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
    public class CreditRepository : GenericDataRepository<Credit, BankDbContext>, ICreditRepository
    {
        public CreditRepository(BankDbContext context)
            : base(context)
        {
        }
    }
}
