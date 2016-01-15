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
    public class CreditBusinessComponent : GenericBusinessComponent<ICreditUnitOfWork>, ICreditBusinessComponent
    {
        public CreditBusinessComponent(ICreditUnitOfWork unitOfWork) : base(unitOfWork) { }

        public IList<Credit> GetAll()
        {
            return _unitOfWork.CreditRepository.GetAll();
        }

        public IEnumerable<Credit> GetClientCredits(int clientId)
        {
            return _unitOfWork.CreditRepository.GetAll().Where(x => x.Request.ClientId == clientId);
        }

        public void Update(int CreditId, DateTime CountFineFromThisDate)
        {
            var old = _unitOfWork.CreditRepository.GetSingle(x => x.CreditId == CreditId);
            old.CountFineFromThisDate = CountFineFromThisDate;
            _unitOfWork.CreditRepository.Update(old);
            _unitOfWork.Save();
        }

        public void Update(int CreditId, bool IsRepaid, bool HasDelays)
        {
            var old = _unitOfWork.CreditRepository.GetSingle(x => x.CreditId == CreditId);
            old.IsRepaid = IsRepaid;
            old.HasDelays = HasDelays;
            _unitOfWork.CreditRepository.Update(old);
            _unitOfWork.Save();
        }

        public void Update(int CreditId, decimal AllreadyPaid, decimal PaidForFine)
        {
            var old = _unitOfWork.CreditRepository.GetSingle(x => x.CreditId == CreditId);
            old.AllreadyPaid = AllreadyPaid;
            old.PaidForFine = PaidForFine;
            _unitOfWork.CreditRepository.Update(old);
            _unitOfWork.Save();
        }

        public void Update(int CreditId, decimal AllreadyPaid, bool IsRepaid, bool HasDelays, decimal PaidForFine, DateTime CountFineFromThisDate)
        {
            var old = _unitOfWork.CreditRepository.GetSingle(x => x.CreditId == CreditId);
            old.AllreadyPaid = AllreadyPaid;
            old.IsRepaid = IsRepaid;
            old.HasDelays = HasDelays;
            old.PaidForFine = PaidForFine;
            old.CountFineFromThisDate = CountFineFromThisDate;
            _unitOfWork.CreditRepository.Update(old);
            _unitOfWork.Save();
        }


        public void AllowCredit(int securityOfficerId, Request request)
        {
            request.Status = RequestStatus.CreditProvided;
            request.SecurityServiceEmployeeId = securityOfficerId;
            _unitOfWork.RequestRepository.Update(request);

            var account = new Account { Balance = request.AmountOfCredit, Client = request.Client };
            _unitOfWork.BankAccount.Balance -= request.AmountOfCredit;
           
            var credit = new Credit
            {
                Account = account,
                CreditType = request.CreditType,
                Request = request,
                StartDate = DateTime.UtcNow,
                //AmountOfPaymentsPerMonth wtf?
                IsRepaid = false,
                HasDelays = false,
                CountFineFromThisDate = DateTime.UtcNow.AddDays(30), //!!! hard-coded!!!
                PaidForFine = 0
            };

            _unitOfWork.AccountRepository.Add(account);
            _unitOfWork.CreditRepository.Add(credit);
            _unitOfWork.RequestRepository.Update(request);
        }

    }
}
