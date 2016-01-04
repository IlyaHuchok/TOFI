using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Entities;

namespace BankBL.Interfaces
{
    public interface ISecurityOfficerBusinessComponent
    {
        IEnumerable<Request> GetRequests(int numberOfRequests);
        IEnumerable<Request> GetAllRequests();
        IEnumerable<Request> GetRequestsByClientLastname(string lastnameSubstring, int numberOfRequests);
        IEnumerable<Request> GetAllRequestsByClientLastname(string lastnameSubstring);
    }
}
