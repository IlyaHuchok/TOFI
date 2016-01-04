using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        /// Gets N oldest request (older = higher id)
        /// </summary>
        /// <param name="numberOfRequests"></param>
        /// <returns></returns>
        IEnumerable<Request> GetRequests(int numberOfRequests)
        {
            return _unitOfWork.RequestRepository.GetAll().OrderByDescending(x => x.RequestId).Take(numberOfRequests);
        }

        IEnumerable<Request> GetAllRequests()
        {
            return _unitOfWork.RequestRepository.GetAll().OrderByDescending(x => x.RequestId);
        }

        /// <summary>
        /// Gets N oldest request (older = higher id)
        /// </summary>
        /// <param name="numberOfRequests"></param>
        /// <returns></returns>
        IEnumerable<Request> GetRequestsByClientLastname(string lastnameSubstring, int numberOfRequests)
        {
            return _unitOfWork.RequestRepository.GetAll(x => x.Client.LastName.Contains(lastnameSubstring)).OrderByDescending(x => x.RequestId).Take(numberOfRequests);
        }

        IEnumerable<Request> GetAllRequestsByClientLastname(string lastnameSubstring)
        {
            return _unitOfWork.RequestRepository.GetAll(x => x.Client.LastName.Contains(lastnameSubstring)).OrderByDescending(x => x.RequestId);
        }
    }
}
