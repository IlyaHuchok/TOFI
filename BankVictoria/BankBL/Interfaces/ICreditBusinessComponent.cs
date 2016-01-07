using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Entities.Enums;
namespace BankBL.Interfaces
{
    public interface ICreditBusinessComponent
    {
        void Update(int CreditId, decimal AllreadyPaid, bool IsRepaid, bool HasDelays, decimal PaidForFine, DateTime CountFineFromThisDate);
        void Update(int CreditId, decimal AllreadyPaid, decimal PaidForFine);
        void Update(int CreditId, bool IsRepaid, bool HasDelays);
        void Update(int CreditId, DateTime CountFineFromThisDate);
        IList<Credit> GetAll();
    }
}
