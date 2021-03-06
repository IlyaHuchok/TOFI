﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Entities.Enums;
namespace BankBL.Interfaces
{
    public interface IRequestBusinessComponent
    {
        IList<Request> GetByStatus(RequestStatus status);
        IList<Request> GetExceptStatus(RequestStatus status);
        IList<Request> GetAll();
        void Update(Request request/*int ClientId,int? OperatorId,int? SecurityServiceEmployeeId, RequestStatus status*/);
        void Add(int ClientId, int? OperatorId, int? SecurityServiceEmployeeId, int CreditTypeId, RequestStatus Status, decimal AmountOfCredit, decimal Salary, string Note);
    }
}
