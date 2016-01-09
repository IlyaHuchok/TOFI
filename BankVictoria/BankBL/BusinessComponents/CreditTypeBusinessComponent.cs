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
using BankBL.BusinessComponents;

namespace BankBL.BusinessComponents
{
    public class CreditTypeBusinessComponent : GenericBusinessComponent<ICreditTypeUnitOfWork>, ICreditTypeBusinessComponent
    {
        public CreditTypeBusinessComponent(ICreditTypeUnitOfWork unitOfWork) : base(unitOfWork) { }
        
        public IEnumerable<CreditType> GetAllCreditTypes()
        {
            return _unitOfWork.CreditTypeRepository.GetAll();
        }

        public IEnumerable<CreditType> GetAllActiveCreditTypes()
        {
            return _unitOfWork.CreditTypeRepository.GetAll().Where(x => x.IsAvailable = true);
        }
    }
}
