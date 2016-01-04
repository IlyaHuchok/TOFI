using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Enums
{
    public enum RequestStatus
    {
        Created = 1,
        ConfirmedByOperator = 2,
        ConfirmedBySecurityOfficer = 3,
        CreditProvided = 4,
        Denied = 5
    }
}
