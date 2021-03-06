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
    public class ClientRepository : GenericDataRepository<Client, BankDbContext>, IClientRepository
    {
        public ClientRepository(BankDbContext context)
            : base(context)
        {
        }
    }
}
