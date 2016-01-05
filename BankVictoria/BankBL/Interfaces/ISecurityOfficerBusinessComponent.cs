using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BankBL.Entities;

using Entities;

namespace BankBL.Interfaces
{
    public interface ISecurityOfficerBusinessComponent
    {
        IEnumerable<Request> GetRequests(int numberOfRequests, int startingIndex = 0);
        IEnumerable<Request> GetAllRequests();
        IEnumerable<Request> GetRequestsByClientLastname(string lastnameSubstring, int numberOfRequests, int startingIndex = 0);
        IEnumerable<Request> GetAllRequestsByClientLastname(string lastnameSubstring);
        IEnumerable<Credit> GetClientCreditHistoryFull(int clientId);
        IEnumerable<Credit> GetClientCreditHistory(int clientId, int numberOfRecords, int startingIndex = 0);
        void AcceptRequest(Request request);
        void RejectRequest(Request request, string reason = null);
    }
}
