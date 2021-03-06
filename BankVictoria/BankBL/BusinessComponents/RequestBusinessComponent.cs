﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.BusinessComponents;
using BankBL.Interfaces;
using Entities;
using Entities.Enums;
using BankBL.BusinessComponents;
using BankUnitOfWork.Interfaces;

namespace BankBL.BusinessComponents
{
    public class RequestBusinessComponent : GenericBusinessComponent<IRequestUintOfWork>, IRequestBusinessComponent
    {
        public RequestBusinessComponent(IRequestUintOfWork unitOfWork) : base(unitOfWork) { }

        public void Add(int ClientId, int? OperatorId, int? SecurityServiceEmployeeId, int CreditTypeId, RequestStatus Status, decimal AmountOfCredit, decimal Salary, string Note)
        {
            Request request = new Request() { ClientId = ClientId, OperatorId = OperatorId, SecurityServiceEmployeeId = SecurityServiceEmployeeId, CreditTypeId = CreditTypeId,
                                              Status = Status, AmountOfCredit = AmountOfCredit, Salary = Salary, Note = Note};
            _unitOfWork.RequestRepository.Add(request);
            _unitOfWork.Save();
        }

        public IList<Request> GetAll()
        {
            return _unitOfWork.RequestRepository.GetAll().OrderBy(x => x.RequestId).ToList();
        }

        public IList<Request> GetByStatus(RequestStatus status)
        {
            return _unitOfWork.RequestRepository.GetAll().Where(x => x.Status == status).ToList();
        }

        public IList<Request> GetExceptStatus(RequestStatus status)
        {
            return _unitOfWork.RequestRepository.GetAll().Where(x => x.Status != status).ToList();
        }

        public void Update(Request request/*int ClientId, int? OperatorId, int? SecurityServiceEmployeeId, RequestStatus status*/)
        {
            var old = _unitOfWork.RequestRepository.GetSingle(x => x.RequestId == request.RequestId);
            old.OperatorId = request.OperatorId;
            old.SecurityServiceEmployeeId = request.SecurityServiceEmployeeId;
            old.Status = request.Status;
            old.Note = request.Note;
            old.RequestId = request.RequestId;
            _unitOfWork.RequestRepository.Update(old);
            _unitOfWork.Save();
        }
    }
}
