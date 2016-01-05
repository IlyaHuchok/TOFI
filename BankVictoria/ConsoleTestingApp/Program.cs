﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BankDomainModel;

using BankDAL.Repositories;

using Entities;
using Entities.Enums;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            // NOT A PART OF PROJECT
            // ADD ANY CODE FOR TESTING HERE
            
            FillDb(); //comment if don't want to refill database

            Console.ReadLine();
            decimal a;
        }

        public static void FillDb()
        {
            using (var context = new BankDbContext())
            {

                // CLEARS ALL DATA !!!
                Console.WriteLine("ALL DATA WILL BE DELETED FROM DB NOW!!! ([ENTER] TO PROCEED)");
                Console.ReadLine();
                context.Accounts.RemoveRange(context.Accounts);
                context.Clients.RemoveRange(context.Clients);
                context.Credits.RemoveRange(context.Credits);
                context.CreditTypes.RemoveRange(context.CreditTypes);
                context.Payments.RemoveRange(context.Payments);
                context.Requests.RemoveRange(context.Requests);
             //   context.RequestStatuses.RemoveRange(context.RequestStatuses);
                context.Users.RemoveRange(context.Users);
                context.SaveChanges();
                // CLEARS ALL DATA !!!

//                var statusRepo = new RequestStatusRepository(context);
 //               var statusCreated = new RequestStatus { Status = "Created" };
   //             var statusConfirmedByOperator = new RequestStatus { Status = "statusConfirmedByOperator" };
     //           var statusConfirmedBySse = new RequestStatus { Status = "statusConfirmedBySecurityServiceEmployee" };
                //var statusConfirmed = new RequestStatus { Status = "ConfirmedBy" };
       //         var statusCreditProvided = new RequestStatus { Status = "statusCreditProvided" };
         //       var statusDenied = new RequestStatus { Status = "Denied" };
           //     statusRepo.Add(statusCreated, statusConfirmedByOperator, statusConfirmedByOperator, statusCreditProvided,statusDenied);

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
                    MaxAmount = 2000
                };

                var creditMedium= new CreditType
                {
                    Name = "Not So Easy Money",
                    //Type = "" WTF IS TYPE?????
                    TimeMonths = 12 * 2,
                    PercentPerYear = 25.0m,
                    Currency = "USD",
                    FinePercent = 50.0m,
                    MinAmount = 200,
                    MaxAmount = 5000
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
                    MaxAmount = 5000
                };

                var creditTypeRepo = new CreditTypeRepository(context);
                creditTypeRepo.Add(creditShort, creditLong, creditMedium);
                context.SaveChanges();
                var creditEasyId = creditShort.CreditTypeId;
                var creditMediumId = creditMedium.CreditTypeId;
                var creditLongId = creditLong.CreditTypeId;

                var admin = new User
                {
                    Login = "admin",
                    Password = "admin",
                    Role = UserRole.Admin
                };

                var ss = new User // security service employee
                {
                    Login = "security",
                    Password = "security",
                    Role = UserRole.SecurityServiceEmployee
                };

                var operator1 = new User // 
                {
                    Login = "operator1",
                    Password = "operator1",
                    Role = UserRole.Operator
                }; 

                var operator2 = new User // 
                {
                    Login = "operator2",
                    Password = "operator2",
                    Role = UserRole.Operator
                };

                var client1 = new User
                {
                    Login = "client1",
                    Password = "client1",
                    Role = UserRole.Client
                };

                var client2 = new User
                {
                    Login = "client2",
                    Password = "client2",
                    Role = UserRole.Client
                };

                var client3 = new User
                {
                    Login = "client3",
                    Password = "client3",
                    Role = UserRole.Client
                };

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
                    Birthday = new DateTime(1990,1,1),
                    Mobile = "+375441234567",
                    Email = "client1@nosite.com",
                    PassportNo = "AB1234567",
                    PassportIdentificationNo = "ABAB1234567890",
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
                    PassportIdentificationNo = "BBBB1234567890",
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
                    PassportIdentificationNo = "AAAA1234567890",
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

                var requestRepo = new RequestRepository(context);
                requestRepo.Add(request1client1, request2client1, request3client1);
                context.SaveChanges();


                var acc1 = new Account  //Bank Account
                {
                    ClientId = null,
                    Balance = 40*1000*1000
                };

                var acc2 = new Account
                {
                    ClientId = client1Info.ClientId,
                    Balance = request2client1.AmountOfCredit
                };

                var accountRepo = new AccountRepository(context);
                accountRepo.Add(acc1, acc2);
                context.SaveChanges();

                var credit1Client1 = new Credit
                {
                    AccountId = acc2.AccountId,
                    CreditTypeId = creditMedium.CreditTypeId,
                    ContractNo = 123123, //random
                    RequestId = request2client1.RequestId,
                    AllreadyPaid = 0,
                    AmountOfPaymentPerMonth = creditMedium.PercentPerYear/12,
                    StartDate = DateTime.Now.AddDays(-(30*4+5)),
                    IsRepaid = false,
                };
                request2client1.Credit = credit1Client1; // IMPORTANT do this for 1-1 relationship (exception otherwise)

                var creditRepo = new CreditRepository(context);
                creditRepo.Add(credit1Client1);
                context.SaveChanges();

                var payment = new Payment
                {
                    OperatorId = operator1.UserId,
                    CreditId = credit1Client1.CreditId,
                    ContractNo = credit1Client1.ContractNo,
                    Amount = 75,
                    Date = credit1Client1.StartDate.AddDays(15)
                };
                //var credit

                var test = context.Requests.Where(x => x.Status == RequestStatus.Created).FirstOrDefault();
               // context.RequestStatuses.Add(new RequestStatus { Status = "Created" });
               // context.SaveChanges();
            }
        }
    }
}
