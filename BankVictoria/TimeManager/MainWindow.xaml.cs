using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

//using BankApplication;

using BankDAL.Interfaces;
using BankDAL.Repositories;

using BankDomainModel;

using Entities;

using Ninject;
using Ninject.Modules;

namespace TimeManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ICreditRepository _creditRepo;
        private IPaymentRepository _paymentRepo;
        private IClientRepository _clientRepo;

        private readonly IKernel kernel = new StandardKernel(new TimeTravelBindings());

        private TimeSpan timeDifference;

        public MainWindow()
        {
            InitializeComponent();
            TimeMovementLabel.Content = "Time is moved by 0 days from now";
        }

        private void MoveTime(int days) // + -move forth, -move back
        {

            using (var context = kernel.Get<BankDbContext>())
            {
                _creditRepo = new CreditRepository(context);
                _paymentRepo = new PaymentRepository(context);
                _clientRepo = new ClientRepository(context);
                var daysDiff = TimeSpan.FromDays(days);
                timeDifference += daysDiff;
                var credits = _creditRepo.GetAll(); //.ToList();
                var test = credits.Count();
                foreach (var item in credits)
                {
                   item.StartDate -= daysDiff;
                    item.CountFineFromThisDate -= daysDiff;
                }
                //var crArray = credits.Select(
                //    x =>
                //        new Credit {
                //            CreditId = x.CreditId,
                //            AccountId = x.AccountId,
                //            CreditTypeId = x.CreditTypeId,
                //            RequestId = x.RequestId,
                //            AllreadyPaid = x.AllreadyPaid,
                //            AmountOfPaymentPerMonth = x.AmountOfPaymentPerMonth,
                //            StartDate = x.StartDate - daysDiff,
                //            IsRepaid = x.IsRepaid,
                //            HasDelays = x.HasDelays,
                //            PaidForFine = x.PaidForFine,
                //            CountFineFromThisDate = x.CountFineFromThisDate - daysDiff//,

                //            //Account = x.Account,
                //            //CreditType = x.CreditType,
                //            //Request = x.Request//,
                //            //Payments = x.Payments
                //        }).ToArray();//
                _creditRepo.Update(credits.ToArray() /*crArray*/);

                var payments = _paymentRepo.GetAll().ToList();
                payments.ForEach(x => x.Date -= daysDiff);
                _paymentRepo.Update(payments.ToArray());

                var clients = _clientRepo.GetAll().ToList();
                foreach (var item in clients)
                {
                    item.Birthday -= daysDiff;
                }
                //var clArray =
                //    clients.Select(
                //        x =>
                //        new Client()
                //        {
                //            ClientId = x.ClientId,
                //            UserId = x.UserId,
                //            LastName = x.LastName,
                //            Name = x.Name,
                //            Patronymic = x.Patronymic,
                //            Birthday = x.Birthday - daysDiff,
                //            Mobile = x.Mobile,
                //            Email = x.Email,
                //            PassportNo = x.PassportNo,
                //            PassportIdentificationNo = x.PassportIdentificationNo,
                //            PassportAuthority = x.PassportAuthority,
                //            PassportExpirationDate = x.PassportExpirationDate - daysDiff,
                //            PlaceOfResidence = x.PlaceOfResidence,
                //            RegistrationAddress = x.RegistrationAddress,
                //            User = x.User
                //        }).ToArray();
                _clientRepo.Update(clients.ToArray());

                context.SaveChanges();
                //_context.Dispose();
            }
            //using (var kernel = new StandardKernel(new TimeTravelBindings()))
            //{
            //    _context = kernel.Get<BankDbContext>();
            //}
        }

        private void GoForthButton_Click(object sender, RoutedEventArgs e)
        {
            MoveTime(int.Parse(TimeForthTextBox.Text));
            TimeMovementLabel.Content = "Time is moved by " + timeDifference + " days forth from now";
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            MoveTime(-int.Parse(TimeBackTextBox.Text));
            TimeMovementLabel.Content = "Time is moved by " + timeDifference + " days forth from now";
        }
    }

    public class TimeTravelBindings : NinjectModule
    {
        public override void Load()
        {
            Bind<IAccountRepository>().To<AccountRepository>();
            Bind<IClientRepository>().To<ClientRepository>();
            Bind<ICreditRepository>().To<CreditRepository>();
            Bind<ICreditTypeRepository>().To<CreditTypeRepository>();
            Bind<IPaymentRepository>().To<PaymentRepository>();
            Bind<IRequestRepository>().To<RequestRepository>();
            //Bind<IRequestStatusRepository>().To<RequestStatusRepository>();
            Bind<IUserRepository>().To<UserRepository>();
        }
    }
}
