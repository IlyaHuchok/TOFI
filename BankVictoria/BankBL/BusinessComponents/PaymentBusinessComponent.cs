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
    public class PaymentBusinessComponent : GenericBusinessComponent<IPaymentUnitOfWork>,IPaymentBusinessComponent
    {
        public PaymentBusinessComponent(IPaymentUnitOfWork unitOfWork) : base(unitOfWork) { }

        public int Add(int operatorId, int creditId/*, int contractNo*/, decimal amount, DateTime date)
        {
            var paymentToAdd = new Payment
            {
                OperatorId = operatorId,
                CreditId = creditId,
                //ContractNo = contractNo,
                Amount = amount,
                Date = date
            };
            _unitOfWork.PaymentRepository.Add(paymentToAdd);
            _unitOfWork.Save();
            return paymentToAdd.PaymentId;
        }
    }
}
