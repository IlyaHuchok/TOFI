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

        public IList<Request> GetAll(RequestStatus status)
        {
            return _unitOfWork.RequestRepository.GetAll();
        }

        public IList<Request> GetByStatus(RequestStatus status)
        {
            return _unitOfWork.RequestRepository.GetAll(x=> x.Status == status);
        }

        public IList<Request> GetExceptStatus(RequestStatus status)
        {
            return _unitOfWork.RequestRepository.GetAll(x => x.Status != status);
        }

        public void Update(int ClientId, int? OperatorId, int? SecurityServiceEmployeeId, RequestStatus status)
        {
            var q = _unitOfWork.RequestRepository.GetSingle(x => x.ClientId == ClientId);
            q.OperatorId = OperatorId;
            q.SecurityServiceEmployeeId = SecurityServiceEmployeeId;
            q.Status = status;
            _unitOfWork.RequestRepository.Update(q);
            _unitOfWork.Save();
        }
    }
}
