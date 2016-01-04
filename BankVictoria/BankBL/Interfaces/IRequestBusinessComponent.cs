using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
namespace BankBL.Interfaces
{
    public interface IRequestBusinessComponent
    {
        Request GetByStatus(string status);
    }
}
