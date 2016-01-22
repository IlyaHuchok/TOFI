using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BankDomainModel;

using BankDAL.Repositories;

using Entities;
using Entities.Enums;
using System.Runtime.InteropServices;

namespace ConsoleTestDbFiller
{
    class Program
    {
        static void Main(string[] args)
        {
            // NOT A PART OF PROJECT
            // ADD ANY CODE FOR TESTING HERE
            try
            {
                //Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
                FillDb(); //comment if don't want to refill database
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Console.WriteLine("Database operations finished");
            Console.ReadLine();
            decimal a;
        }

        public static void FillDb()
        {
            using (var context = new BankDbContext())
            {
                Console.WriteLine("ConnectionString\n" + context.Database.Connection.ConnectionString);
                Console.WriteLine("DataSource\n" + context.Database.Connection.DataSource);
                Console.WriteLine("ConnectionString\n" + context.Database.Connection.Database);
                // CLEARS ALL DATA !!!
                Console.WriteLine("ALL DATA WILL BE DELETED FROM DB NOW!!! ([ENTER] TO PROCEED)");
                Console.ReadLine();
                //if (!context.Database.Exists())
                //{
                    context.Database.Delete();
                    context.Database.Create();
                //}
                context.Database.Initialize(true);//
                Console.WriteLine("Db initialized");
                Console.ReadLine();
                context.Accounts.RemoveRange(context.Accounts);
                ///Console.WriteLine("I've successfully completed first db action!");
                ///Console.ReadLine();
                context.Clients.RemoveRange(context.Clients);
                context.Credits.RemoveRange(context.Credits);
                context.CreditTypes.RemoveRange(context.CreditTypes);
                context.Payments.RemoveRange(context.Payments);
                context.Requests.RemoveRange(context.Requests);
                //context.RequestStatuses.RemoveRange(context.RequestStatuses);
                context.Users.RemoveRange(context.Users);
                context.SaveChanges();
                // CLEARS ALL DATA !!!

                //var statusRepo = new RequestStatusRepository(context);
                //var statusCreated = new RequestStatus { Status = "Created" };
                //var statusConfirmedByOperator = new RequestStatus { Status = "statusConfirmedByOperator" };
                //var statusConfirmedBySse = new RequestStatus { Status = "statusConfirmedBySecurityServiceEmployee" };
                ////var statusConfirmed = new RequestStatus { Status = "ConfirmedBy" };
                //var statusCreditProvided = new RequestStatus { Status = "statusCreditProvided" };
                //var statusDenied = new RequestStatus { Status = "Denied" };
                //statusRepo.Add(statusCreated, statusConfirmedByOperator, statusConfirmedByOperator, statusCreditProvided,statusDenied);

                //   context.SaveChanges();
                //            var confirmedByOperatorStatusId = statusCreditProvided.RequestStatusId;
                //             var createdStatusId = statusCreated.RequestStatusId;
                //             var deinedStatusId = statusDenied.RequestStatusId;

                var creditShort = new CreditType
                {
                    Name = "Easy Money",
                    //Type = "" WTF IS TYPE?????
                    TimeMonths = 12,
                    PercentPerYear = 20.0m,
                    Currency = "USD",
                    FinePercent = 40.0m,
                    MinAmount = 200,
                    MaxAmount = 2000,
                    IsAvailable = true
                };

                var creditMedium = new CreditType
                {
                    Name = "Not So Easy Money",
                    //Type = "" WTF IS TYPE?????
                    TimeMonths = 12 * 2,
                    PercentPerYear = 25.0m,
                    Currency = "USD",
                    FinePercent = 50.0m,
                    MinAmount = 200,
                    MaxAmount = 5000,
                    IsAvailable = true
                };

                var creditLong = new CreditType
                {
                    Name = "Still Money",
                    //Type = "" WTF IS TYPE?????
                    TimeMonths = 12 * 4,
                    PercentPerYear = 30.0m,
                    Currency = "USD",
                    FinePercent = 60.0m,
                    MinAmount = 200,
                    MaxAmount = 5000,
                    IsAvailable = true
                };

                var creditTypeRepo = new CreditTypeRepository(context);
                creditTypeRepo.Add(creditShort, creditLong, creditMedium);
                context.SaveChanges();
                var creditEasyId = creditShort.CreditTypeId;
                var creditMediumId = creditMedium.CreditTypeId;
                var creditLongId = creditLong.CreditTypeId;

                var admin = new User { Login = "admin", Password = "admin", Role = UserRole.Admin, IsActive = true };

                var ss = new User // security service employee
                { Login = "security", Password = "security", Role = UserRole.SecurityServiceEmployee, IsActive = true };

                var operator1 = new User // 
                { Login = "operator1", Password = "operator1", Role = UserRole.Operator, IsActive = true };

                var operator2 = new User // 
                { Login = "operator2", Password = "operator2", Role = UserRole.Operator, IsActive = true };

                var client1 = new User { Login = "client1", Password = "client1", Role = UserRole.Client };

                var client2 = new User { Login = "client2", Password = "client2", Role = UserRole.Client };

                var client3 = new User { Login = "client3", Password = "client3", Role = UserRole.Client };

                var userRepo = new UserRepository(context);
                userRepo.Add(admin, ss, operator1, operator2, client1, client2, client3);
                context.SaveChanges();
                var client1Id = client1.UserId;
                var client2Id = client2.UserId;
                var client3Id = client3.UserId;

                var client1Info = new Client
                {
                    UserId = client1.UserId,
                    Name = "Clientone",
                    LastName = "Clientov",
                    Patronymic = "Clientovich",
                    Birthday = new DateTime(1990, 1, 1),
                    Mobile = "+375441234567",
                    Email = "client1@nosite.com",
                    PassportNo = "AB1234567",
                    PassportIdentificationNo = "4123456B124PB7",
                    PassportAuthority = "Ministry of internal affairs",
                    PassportExpirationDate = DateTime.Now.AddYears(6),
                    PlaceOfResidence = "Pushkina st.1 app.18",
                    RegistrationAddress = "Pushkina st.1 app.18"
                };

                var client2Info = new Client
                {
                    UserId = client2.UserId,
                    Name = "Clienttwo",
                    LastName = "Clientov",
                    Patronymic = "Clientovich",
                    Birthday = new DateTime(1982, 2, 2),
                    Mobile = "+375251234567",
                    Email = "client2@nosite.com",
                    PassportNo = "AB1234123",
                    PassportIdentificationNo = "4125552B124PB7",
                    PassportAuthority = "Ministry of internal affairs",
                    PassportExpirationDate = DateTime.Now.AddYears(1),
                    PlaceOfResidence = "Pushkina st.2 app.7",
                    RegistrationAddress = "Pushkina st.2 app.7"
                };

                var client3Info = new Client
                {
                    UserId = client3.UserId,
                    Name = "Clientthree",
                    LastName = "Clientov",
                    Patronymic = "Clientovich",
                    Birthday = new DateTime(1973, 3, 3),
                    Mobile = "+375291234567",
                    Email = "client3@nosite.com",
                    PassportNo = "AB1223331",
                    PassportIdentificationNo = "4129332B124PB3",
                    PassportAuthority = "Ministry of internal affairs",
                    PassportExpirationDate = DateTime.Now.AddYears(6),
                    PlaceOfResidence = "Pushkina st.3 app.24",
                    RegistrationAddress = "Pushkina st.3 app.24"
                };

                var clientRepo = new ClientRepository(context);
                clientRepo.Add(client1Info, client2Info, client3Info);
                context.SaveChanges();

                var request1client1 = new Request
                {
                    ClientId = client1Info.ClientId,
                    //RequestStatusId = createdStatusId,
                    Status = RequestStatus.Created,
                    CreditTypeId = creditEasyId,
                    AmountOfCredit = 1000,
                    Salary = 500
                };

                var request2client1 = new Request
                {
                    ClientId = client1Info.ClientId,
                    //RequestStatusId = createdStatusId,
                    Status = RequestStatus.Created,
                    OperatorId = operator1.UserId,
                    CreditTypeId = creditMediumId,
                    AmountOfCredit = 1200,
                    Salary = 500
                };

                var request3client1 = new Request
                {
                    ClientId = client1Info.ClientId,
                    //RequestStatusId = confirmedByOperatorStatusId,
                    Status = RequestStatus.ConfirmedByOperator,
                    OperatorId = operator1.UserId,
                    CreditTypeId = creditLongId,
                    AmountOfCredit = 1000,
                    Salary = 500
                };

                var request4client1 = new Request
                {
                    ClientId = client1Info.ClientId,
                    Status = RequestStatus.ConfirmedByOperator, // createdStatusId,
                    OperatorId = operator1.UserId,
                    CreditTypeId = creditMediumId,
                    AmountOfCredit = 1100,
                    Salary = 500
                };
                var request5client1 = new Request
                {
                    ClientId = client1Info.ClientId,
                    //RequestStatusId = createdStatusId,
                    Status = RequestStatus.CreditProvided,
                    OperatorId = operator1.UserId,
                    CreditTypeId = creditLongId,
                    AmountOfCredit = 1300,
                    Salary = 500
                };

                var request6client1 = new Request
                {
                    ClientId = client1Info.ClientId,
                    //RequestStatusId = confirmedByOperatorStatusId,
                    Status = RequestStatus.CreditProvided,
                    OperatorId = operator1.UserId,
                    CreditTypeId = creditLongId,
                    AmountOfCredit = 900,
                    Salary = 500
                };
                var request7client1 = new Request
                {
                    ClientId = client1Info.ClientId,
                    Status = RequestStatus.ConfirmedBySecurityOfficer, // createdStatusId,
                    OperatorId = operator1.UserId,
                    CreditTypeId = creditLongId,
                    AmountOfCredit = 800,
                    Salary = 500
                };
                var request8client1 = new Request
                {
                    ClientId = client1Info.ClientId,
                    //RequestStatusId = confirmedByOperatorStatusId,
                    Status = RequestStatus.CreditProvided,
                    OperatorId = operator1.UserId,
                    CreditTypeId = creditMediumId,
                    AmountOfCredit = 900,
                    Salary = 500 
                };
                 
                var requestRepo = new RequestRepository(context);
                requestRepo.Add(
                    request1client1,
                    request2client1,
                    request3client1,
                    request4client1,
                    request5client1,
                    request6client1,
                    request7client1,
                    request8client1);
                context.SaveChanges();


                //var acc1 = new Account  //Bank Account
                //{
                //    ClientId = null,
                //    Balance = 40*1000*1000
                //};

                var bankAccount = new BankAccount //Bank Account
                { Balance = 40 * 1000 * 1000, Currency = "USD" };
                context.BankAccount = bankAccount;
                context.SaveChanges();


                var acc2 = new Account { ClientId = client1Info.ClientId, Balance = request2client1.AmountOfCredit };

                var accountRepo = new AccountRepository(context);
                accountRepo.Add( /*acc1,*/ acc2);
                context.SaveChanges();


                DateTime dt1 = DateTime.Now.AddDays(-(30 * 4 + 5));
                var credit1Client1 = new Credit
                {
                    AccountId = acc2.AccountId,
                    CreditTypeId = creditLong.CreditTypeId,
                    //ContractNo = 123123, //random
                    RequestId = request5client1.RequestId,
                    //AllreadyPaid = 0,
                    AmountOfPaymentPerMonth = (request5client1.AmountOfCredit / creditLong.TimeMonths) * (1 + creditLong.PercentPerYear / 100 * creditLong.TimeMonths / 12),
                    StartDate = dt1,
                    IsRepaid = false,
                    HasDelays = false,
                    CountFineFromThisDate = dt1.AddDays(30),//DateTime.UtcNow.AddDays(30), //!!! hard-coded!!!
                    AmountToCountFineFromForFirstDelayedMonth = (request5client1.AmountOfCredit / creditLong.TimeMonths) * (1 + creditLong.PercentPerYear / 100 * creditLong.TimeMonths / 12),
                    PaidForFine = 0
                };
                DateTime dt2 = DateTime.UtcNow.AddDays(-(30 * 50 + 7));
                var credit2Client1 = new Credit
                {
                    AccountId = acc2.AccountId,
                    CreditTypeId = creditLong.CreditTypeId,
                    //ContractNo = 123123, //random
                    RequestId = request6client1.RequestId,
                    //AllreadyPaid = 0,
                    AmountOfPaymentPerMonth = (request6client1.AmountOfCredit / creditLong.TimeMonths) * (1 + creditLong.PercentPerYear / 100 * creditLong.TimeMonths/12),
                    StartDate = dt2,
                    IsRepaid = true,
                    HasDelays = true,
                    CountFineFromThisDate = dt2.AddDays(30), //!!! hard-coded!!!
                    AmountToCountFineFromForFirstDelayedMonth = (request6client1.AmountOfCredit / creditLong.TimeMonths) * (1 + creditLong.PercentPerYear / 100 * creditLong.TimeMonths / 12),
                    PaidForFine = 0
                };

                var credit3Client1 = new Credit
                {
                    AccountId = acc2.AccountId,
                    CreditTypeId = creditMedium.CreditTypeId,
                    //ContractNo = 123123, //random
                    RequestId = request8client1.RequestId,
                    //AllreadyPaid = 0,
                    AmountOfPaymentPerMonth = (request8client1.AmountOfCredit / creditMedium.TimeMonths) * (1 + creditMedium.PercentPerYear / 100 * creditMedium.TimeMonths / 12),
                    StartDate = DateTime.Now,
                    IsRepaid = false,
                    HasDelays = true,
                    CountFineFromThisDate = DateTime.Now.AddDays(30), //!!! hard-coded!!!
                    AmountToCountFineFromForFirstDelayedMonth = (request8client1.AmountOfCredit / creditMedium.TimeMonths) * (1 + creditMedium.PercentPerYear / 100 * creditMedium.TimeMonths / 12),
                    PaidForFine = 0
                };
                request5client1.Credit = credit1Client1; // IMPORTANT do this for 1-1 relationship (exception otherwise)
                request6client1.Credit = credit2Client1; // IMPORTANT do this for 1-1 relationship (exception otherwise)
                request8client1.Credit = credit3Client1; // IMPORTANT do this for 1-1 relationship (exception otherwise)

                var creditRepo = new CreditRepository(context);
                creditRepo.Add(credit1Client1, credit2Client1, credit3Client1);
                context.SaveChanges();

           /*     var payment = new Payment
                {
                    OperatorId = operator1.UserId,
                    CreditId = credit1Client1.CreditId,
                    //ContractNo = credit1Client1.ContractNo,
                    Amount = 75,
                    Date = credit1Client1.StartDate.AddDays(15)
                };
                //var credit

                var payRepo = new PaymentRepository(context);
                payRepo.Add(payment);                                */
                var test = context.BankAccount;
                    //context.RequestStatuses.Where(x => x.Status.Contains("Created")).FirstOrDefault();
                //context.RequestStatuses.Add(new RequestStatus { Status = "Created" });
                context.SaveChanges();
            }
        }
    }
}
