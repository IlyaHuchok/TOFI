using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankBL.Interfaces
{
    public interface IPaymentBusinessComponent
    {
        int Add(int operatorId, int creditId, int contractNo, decimal amount, DateTime date);
    }
}
