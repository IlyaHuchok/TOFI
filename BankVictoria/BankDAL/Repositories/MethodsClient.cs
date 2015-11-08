using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankDomainModel
{
    public class MethodsClient
    {
        public void Add()
        {
            using (var db = new BankDbContext())
            {
                var client = new ClientDB
                {

                };
            }
        }
        public void RemoveClient(string PassportNo)
        {
            using (var db = new BankDbContext())
            {
                List<ClientDB> query = (from b in db.Client where b.PassportNo == PassportNo select b).ToList();
                db.Client.Remove(query[0]);
            }
        }


    }
}
