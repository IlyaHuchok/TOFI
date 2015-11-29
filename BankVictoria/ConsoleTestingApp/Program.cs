using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BankDomainModel;

using BankDAL.Repositories;

using Entities;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            // NOT A PART OF PROJECT
            // ADD ANY CODE FOR TESTING HERE
            
            FillDb();

            Console.ReadLine();
        }

        public static void FillDb()
        {
            using (var context = new BankDbContext())
            {

                //// CLEARS ALL DATA !!!
                //Console.WriteLine("ALL DATA WILL BE DELETED FROM DB NOW!!! ([ENTER] TO PROCEED)");
                //Console.ReadLine();
                //context.Accounts.RemoveRange(context.Accounts);
                //context.Clients.RemoveRange(context.Clients);
                //context.Credits.RemoveRange(context.Credits);
                //context.CreditTypes.RemoveRange(context.CreditTypes);
                //context.Payments.RemoveRange(context.Payments);
                //context.Requests.RemoveRange(context.Requests);
                //context.RequestStatuses.RemoveRange(context.RequestStatuses);
                //context.Users.RemoveRange(context.Users);
                //context.SaveChanges();
                //// CLEARS ALL DATA !!!

                //var statusRepo = new RequestStatusRepository(context);
                //var statusCreated = new RequestStatus { Status = "Created" };
                //var statusConfirmed = new RequestStatus { Status = "Confirmed" };
                //var statusDenied = new RequestStatus { Status = "Denied" };
                //statusRepo.Add(statusCreated, statusConfirmed, statusDenied);

                //context.SaveChanges();
                //var confirmedId = statusConfirmed.RequestStatusId;

                //var creditEasy = new CreditType
                //{
                //    Name = "Easy Money",
                //    //Type = "" WTF IS TYPE?????
                //    Time = TimeSpan.F
                //}

                //var test = context.RequestStatuses.Where(x => x.Status.Contains("Created")).FirstOrDefault();
                //context.RequestStatuses.Add(new RequestStatus { Status = "Created" });
                context.SaveChanges();
            }
        }
    }
}
