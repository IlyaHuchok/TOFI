using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BankBL.Entities;

using BL.BusinessComponents;
using BankBL.Interfaces;
using BankUnitOfWork.Interfaces;
using Entities;
using Entities.Enums;

namespace BankBL.BusinessComponents
{
    public class SecurityOfficerBusinessComponent : GenericBusinessComponent<ISecurityOfficerUnitOfWork>, ISecurityOfficerBusinessComponent
    {
        public SecurityOfficerBusinessComponent(ISecurityOfficerUnitOfWork unitOfWork) : base(unitOfWork) { }

        /// <summary>
        /// Is Used to share requests between all active workers
        /// e.g. if we have 4 active workers, each of them will have 1/4 of all requests
        /// </summary>
        private int GetLoadBalancerNumber(int securityOfficerId)
        {
            var activeWorkers = _unitOfWork.UserRepository.GetAll(x => x.Role == UserRole.SecurityServiceEmployee && x.IsActive == true);
            var indexOfSecurityOfficerAmongActiveWorkers = activeWorkers.Select(x => x.UserId).ToList().IndexOf(securityOfficerId);
            return indexOfSecurityOfficerAmongActiveWorkers - 1;
        }

        /// <summary>
        /// Gets N oldest request starting from startingIndex (older = higher id)
        /// </summary>
        /// <param name="numberOfRequests"></param>
        /// <returns></returns>
        public IEnumerable<Request> GetRequests(int numberOfRequests, int startingIndex = 0)
        {
            return _unitOfWork.RequestRepository.GetAll(x => x.Status == RequestStatus.ConfirmedByOperator).OrderByDescending(x => x.RequestId).Skip(startingIndex).Take(numberOfRequests);
        }

        public IEnumerable<Request> GetAllRequests()
        {
            return _unitOfWork.RequestRepository.GetAll(x => x.Status == RequestStatus.ConfirmedByOperator).OrderByDescending(x => x.RequestId);
        }

        /// <summary>
        /// Gets N oldest request starting from startingIndex (older = higher id)
        /// </summary>
        /// <param name="numberOfRequests"></param>
        /// <returns></returns>
        public IEnumerable<Request> GetRequestsByClientLastname(string lastnameSubstring, int numberOfRequests, int startingIndex = 0)
        {
            return _unitOfWork.RequestRepository.GetAll(x => x.Status == RequestStatus.ConfirmedByOperator && x.Client.LastName.Contains(lastnameSubstring)).OrderByDescending(x => x.RequestId).Skip(startingIndex).Take(numberOfRequests);
        }

        public IEnumerable<Request> GetAllRequestsByClientLastname(string lastnameSubstring)
        {
            return _unitOfWork.RequestRepository.GetAll(x => x.Status == RequestStatus.ConfirmedByOperator && x.Client.LastName.Contains(lastnameSubstring)).OrderByDescending(x => x.RequestId);
        }

        public IEnumerable<Credit> GetClientCreditHistoryFull(int clientId)
        {
            return _unitOfWork.CreditRepository.GetAll(x => x.Request.ClientId == clientId);
        }

        public IEnumerable<Credit> GetClientCreditHistory(int clientId, int numberOfRecords, int startingIndex = 0)
        {
            return _unitOfWork.CreditRepository.GetAll(x => x.Request.ClientId == clientId).Skip(startingIndex).Take(numberOfRecords);
        }

        public void AcceptRequest(Request request)
        {
            request.Status = RequestStatus.ConfirmedBySecurityOfficer;
            _unitOfWork.RequestRepository.Update(request);
        }

        public void RejectRequest(Request request, string reason = null)
        {
            request.Status = RequestStatus.Denied;
            request.Note = reason;
            _unitOfWork.RequestRepository.Update(request);
        }
    }
}
