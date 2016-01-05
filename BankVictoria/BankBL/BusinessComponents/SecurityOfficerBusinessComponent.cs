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
        /// Gets N oldest request starting from startingIndex (older = higher id)
        /// </summary>
        /// <param name="numberOfRequests"></param>
        /// <returns></returns>
        public IEnumerable<Request> GetRequests(int numberOfRequests, int startingIndex = 0)
        {
            return _unitOfWork.RequestRepository.GetAll().OrderByDescending(x => x.RequestId).Skip(startingIndex).Take(numberOfRequests);
        }

        public IEnumerable<Request> GetAllRequests()
        {
            return _unitOfWork.RequestRepository.GetAll().OrderByDescending(x => x.RequestId);
        }

        /// <summary>
        /// Gets N oldest request starting from startingIndex (older = higher id)
        /// </summary>
        /// <param name="numberOfRequests"></param>
        /// <returns></returns>
        public IEnumerable<Request> GetRequestsByClientLastname(string lastnameSubstring, int numberOfRequests, int startingIndex = 0)
        {
            return _unitOfWork.RequestRepository.GetAll(x => x.Client.LastName.Contains(lastnameSubstring)).OrderByDescending(x => x.RequestId).Skip(startingIndex).Take(numberOfRequests);
        }

        public IEnumerable<Request> GetAllRequestsByClientLastname(string lastnameSubstring)
        {
            return _unitOfWork.RequestRepository.GetAll(x => x.Client.LastName.Contains(lastnameSubstring)).OrderByDescending(x => x.RequestId);
        }

        public IEnumerable<CreditSummary> GetClientCreditHistoryFull(int clientId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CreditSummary> GetClientCreditHistory(int clientId, int numberOfRecords, int startingIndex = 0)
        {
            throw new NotImplementedException();
        }
    }
}
