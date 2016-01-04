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

        public Request GetByStatus(string status)
        {
            return _unitOfWork.RequestRepository.GetSingle(x=> x.Status.Equals(status));
        }
    }
}
