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
        private readonly ICreditRepository _creditRepo;
        private readonly IPaymentRepository _paymentRepo;

        private readonly IClientRepository _clientRepo;

        private BankDbContext _context;
        private TimeSpan timeDifference;

        public MainWindow()
        {
            using (var kernel = new StandardKernel(new TimeTravelBindings()))
            {
                _context = kernel.Get<BankDbContext>();
                _creditRepo = new CreditRepository(_context);
                _paymentRepo = new PaymentRepository(_context);
                _clientRepo = new ClientRepository(_context);
            }
            InitializeComponent();
            TimeMovementLabel.Content = "Time is moved by 0 days from now";
        }

        private void MoveTime(int days) // + -move forth, -move back
        {
            var daysDiff = TimeSpan.FromDays(days);
            timeDifference += daysDiff;
           var credits = _creditRepo.GetAll().ToList();
           var crArray = credits.Select(
               x =>
                   new Credit {
                       CreditId = x.CreditId,
                       AccountId = x.AccountId,
                       CreditTypeId = x.CreditTypeId,
                       RequestId = x.RequestId,
                       AllreadyPaid = x.AllreadyPaid,
                       AmountOfPaymentPerMonth = x.AmountOfPaymentPerMonth,
                       StartDate = x.StartDate -= daysDiff,
                       IsRepaid = x.IsRepaid,
                       HasDelays = x.HasDelays,
                       PaidForFine = x.PaidForFine,
                       CountFineFromThisDate = x.CountFineFromThisDate -= daysDiff
                   }).ToArray();
           _creditRepo.Update(crArray);

           _context.SaveChanges();
            var payments = _paymentRepo.GetAll().ToList();
            var paysArray = payments.Select(x => 
                new Payment{
                    PaymentId = x.PaymentId,
                    OperatorId = x.OperatorId,
                    CreditId = x.CreditId,
                    Amount = x.Amount,
                    Date = x.Date -= daysDiff}).ToArray();
            _paymentRepo.Update(paysArray);

            _context.SaveChanges();
            var clients = _clientRepo.GetAll().ToList();
            var clArray = clients.Select(x =>
                new Client()
                    {
                        ClientId = x.ClientId,
                        UserId = x.UserId,
                        LastName = x.LastName,
                        Name = x.Name,
                        Patronymic = x.Patronymic,
                        Birthday = x.Birthday -= daysDiff,
                        Mobile = x.Mobile,
                        Email =  x.Email,
                        PassportNo = x.PassportNo,
                        PassportIdentificationNo = x.PassportIdentificationNo,
                        PassportAuthority = x.PassportAuthority,
                        PassportExpirationDate = x.PassportExpirationDate -= daysDiff,
                        PlaceOfResidence = x.PlaceOfResidence,
                        RegistrationAddress = x.RegistrationAddress,
                        User = x.User
                    }).ToArray();
            _clientRepo.Update(clArray);

            _context.SaveChanges();
            //_context.Dispose();

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
