using System;
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

        public IList<Request> GetAll(RequestStatus status)
        {
            return _unitOfWork.RequestRepository.GetAll();
        }

        public IList<Request> GetByStatus(RequestStatus status)
        {
            return _unitOfWork.RequestRepository.GetAll().Where(x => x.Status == status).ToList();
        }

        public IList<Request> GetExceptStatus(RequestStatus status)
        {
            return _unitOfWork.RequestRepository.GetAll().Where(x => x.Status != status).ToList();
        }

        public void Update(int ClientId, int? OperatorId, int? SecurityServiceEmployeeId, RequestStatus status)
        {
            var old = _unitOfWork.RequestRepository.GetSingle(x => x.ClientId == ClientId);
            old.OperatorId = OperatorId;
            old.SecurityServiceEmployeeId = SecurityServiceEmployeeId;
            old.Status = status;
            _unitOfWork.RequestRepository.Update(old);
            _unitOfWork.Save();
        }
    }
}
