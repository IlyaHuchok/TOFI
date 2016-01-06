using System;
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
        IList<Request> GetAll(RequestStatus status);
        void Update(int ClientId,int? OperatorId,int? SecurityServiceEmployeeId, RequestStatus status);
    }
}
