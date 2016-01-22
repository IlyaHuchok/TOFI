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
using System.Windows.Shapes;

using Entities;
using Entities.Enums;
using BankBL.Interfaces;
using System.Collections.ObjectModel;
using BankPresentation.Validation;

using Ninject;

namespace BankPresentation
{
    /// <summary>
    /// Логика взаимодействия для OperatorWindow.xaml
    /// </summary>
    public partial class OperatorWindow : Page
    {//
        private ObservableCollection<OperatorRequestListClass> RequestDataList = new ObservableCollection<OperatorRequestListClass>();
        private ObservableCollection<ContractNoCreditType> RepaymentDataList = new ObservableCollection<ContractNoCreditType>();
        private ObservableCollection<ACreditListView> AllowCreditDataList = new ObservableCollection<ACreditListView>();
        private readonly int _operatorId;
        private readonly IClientBusinessComponent _clientBusinessComponent;
        private IRequestBusinessComponent _requestBusinessComponent;
        private readonly IPaymentBusinessComponent _paymentBusinessComponent;
        private ICreditBusinessComponent _creditBusinessComponent;
        private readonly IKernel _ninjectKernel;

        public OperatorWindow(IClientBusinessComponent clientBusinessComponent, IRequestBusinessComponent requestBusinessComponent, IPaymentBusinessComponent paymentBusinessComponent,
            ICreditBusinessComponent creditBusinessComponent, int operatorId, IKernel ninjectKernel)
        {
            _clientBusinessComponent = clientBusinessComponent;
            _requestBusinessComponent = requestBusinessComponent;
            _paymentBusinessComponent = paymentBusinessComponent;
            _creditBusinessComponent = creditBusinessComponent;
            _operatorId = operatorId;
            this._ninjectKernel = ninjectKernel;

            InitializeComponent();

            RepaymentPassportNo.MaxLength = OperatorValidation.PassportNoMaxLength;
            AllowCreditPassportNo.MaxLength = OperatorValidation.PassportNoMaxLength;
            RepaymentToPay.MaxLength = OperatorValidation.ToPayMaxLength;
            RequestRequestId.MaxLength = OperatorValidation.RequestIdMaxLength;

            RequestListView.ItemsSource = RequestDataList;
            RepaymentListView.ItemsSource = RepaymentDataList;
            AllowCreditListView.ItemsSource = AllowCreditDataList;

            RequestListView.SelectionMode = SelectionMode.Single;
            RepaymentListView.SelectionMode = SelectionMode.Single;
            AllowCreditListView.SelectionMode = SelectionMode.Single;

            RepaymentOpen.IsEnabled = false;
            RequestReject.IsEnabled = false;
            RequestSendRequest.IsEnabled = false;
            RepaymentSubmit.IsEnabled = false;
        }

        private void RepaymentSearch_Click(object sender, RoutedEventArgs e)
        {
            if (Validate(true,false,false, false))
            {
                RepaymentDataList.Clear();
                IList<Request> request = _requestBusinessComponent.GetByStatus(RequestStatus.CreditProvided).Where(x => x.Credit.IsRepaid == false).ToList();
                foreach (var req in request)
                {
                    if (req.Client.PassportNo == RepaymentPassportNo.Text)
                    {
                        RepaymentDataList.Add(new ContractNoCreditType() { ContractNO = req.Credit.CreditId.ToString(), CreditType = req.CreditType.Name });
                    }
                }
            /*    if (RepaymentDataList.Count > 0)
                {
                    RepaymentListView.SelectedIndex = 0;
                    RepaymentOpen.IsEnabled = true;
                }*/
            }
        }

        private void RepaymentOpen_Click(object sender, RoutedEventArgs e)
        {

            IList<Request> request = _requestBusinessComponent.GetByStatus(RequestStatus.CreditProvided);
            foreach (var req in request)
            {
                ContractNoCreditType cnct = (ContractNoCreditType)RepaymentListView.SelectedItem;
                if ((req.Client.PassportNo == RepaymentPassportNo.Text) && (Convert.ToInt32(cnct.ContractNO) == req.Credit.CreditId)) // CreditId==ContrqctNo
                {

                    if (RepaymentListView.SelectedItem == null)
                    {
                        MessageBox.Show("Choose ContractNo, please.");
                    }
                    else
                    {
                        var debt = this.CountUpNewDebt();
                        RepaymentDebt.Text = debt.ToString("N2");//высчитываем долг
                        RepaymentName.Text = req.Client.Name + " " + req.Client.LastName;// + " " + req.Client.Patronymic;
                        //  RepaymentDebt.Text = // req.Credit.PaidForFine.ToString();
                        var monthLeft = 0;
                        var creditEndDate = req.Credit.StartDate.AddDays(req.CreditType.TimeMonths * 30);
                        if (creditEndDate > DateTime.Now)
                        {
                            monthLeft = (req.Credit.StartDate.AddDays(req.CreditType.TimeMonths * 30) - DateTime.Now).Days / 30 + 1;
                        }

                        RepaymentToRepayTheLoan.Text = (monthLeft * req.Credit.AmountOfPaymentPerMonth + debt).ToString("N2"); /*req.Credit.AmountOfPaymentPerMonth * ( req.CreditType.TimeMonths  )*//*+ req.Credit.PaidForFine*/
                
                    }
                }
            }
        }

        private decimal CountUpNewDebt()
        {
                ContractNoCreditType cnct = (ContractNoCreditType)RepaymentListView.SelectedValue;
                Credit credit =
                    _creditBusinessComponent.GetAll()
                        .FirstOrDefault(x => x.CreditId == Convert.ToInt32(cnct.ContractNO));

                decimal debt = 0;
                if (DateTime.Now > credit.CountFineFromThisDate)
                {
                    var daysDiff = (DateTime.Now - credit.CountFineFromThisDate).Days;
                    debt += credit.AmountToCountFineFromForFirstDelayedMonth + credit.AmountToCountFineFromForFirstDelayedMonth * daysDiff / 360 * credit.CreditType.FinePercent / 100;
                    //int fullMonths = (DateTime.Now - credit.CountFineFromThisDate).Days / 30;
                    daysDiff -= 30;
                    while (daysDiff > 0)
                    {
                        debt += credit.AmountOfPaymentPerMonth + credit.AmountOfPaymentPerMonth * daysDiff / 360 * credit.CreditType.FinePercent / 100;
                        daysDiff -= 30;
                    }
                    debt -= credit.PaidForFine;
                }

                return debt;
        }

        private decimal CountUpFine()
        {
            ContractNoCreditType cnct = (ContractNoCreditType)RepaymentListView.SelectedValue;
            Credit credit =
                _creditBusinessComponent.GetAll()
                    .FirstOrDefault(x => x.CreditId == Convert.ToInt32(cnct.ContractNO));

            return CountUpFine(credit);
        }

        private decimal CountUpFine(Credit credit)
        {
            decimal fine = 0;
            if (DateTime.Now > credit.CountFineFromThisDate)
            {
                var daysDiff = (DateTime.Now - credit.CountFineFromThisDate).Days;
                fine += credit.AmountToCountFineFromForFirstDelayedMonth * daysDiff / 360 * credit.CreditType.FinePercent / 100;
                //int fullMonths = (DateTime.Now - credit.CountFineFromThisDate).Days / 30;

                daysDiff -= 30;
                while (daysDiff > 0)
                {
                    fine += credit.AmountOfPaymentPerMonth * daysDiff / 360 * credit.CreditType.FinePercent / 100;
                    daysDiff -= 30;
                }
                fine -= credit.PaidForFine;
            }

            return fine;
        }

        private void RepaymentSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (Validate(false,true,false, false))
            {
                if (decimal.Parse(RepaymentToPay.Text) > decimal.Parse(RepaymentToRepayTheLoan.Text))
                {
                    MessageBox.Show("Cannot pay more, than " + RepaymentToRepayTheLoan.Text);
                }
                else
                {
                    MessageBoxResult messageBoxResult = MessageBox.Show(
                        "Are you sure?",
                        "Accept Confirmation",
                        MessageBoxButton.YesNo);
                    if (messageBoxResult == MessageBoxResult.Yes)
                    {
                        ContractNoCreditType cnct = (ContractNoCreditType)RepaymentListView.SelectedValue;
                        Client client =
                            _clientBusinessComponent.GetAll()
                                .Where(x => x.PassportNo == RepaymentPassportNo.Text)
                                .FirstOrDefault();
                        Credit credit =
                            _creditBusinessComponent.GetAll()
                                .Where(x => x.CreditId == Convert.ToInt32(cnct.ContractNO))
                                .FirstOrDefault();
                        var paymentAmount = Convert.ToDecimal(RepaymentToPay.Text);

                        var fine = this.CountUpFine();
                        if (paymentAmount < fine) // хватило только на проценты
                        {
                            credit.PaidForFine += paymentAmount;
                        }
                        else
                        {
                            if (Math.Abs(paymentAmount - decimal.Parse(RepaymentToRepayTheLoan.Text)) >= 0.01m)
                            {
                                var paymentLeft = paymentAmount - fine;
                                //уменьшаюощаяся сумма по которой будем смотреть насколько далеко можно отодвинуть дату/сумму нового долга
                                credit.PaidForFine = 0;

                                if (paymentLeft < credit.AmountToCountFineFromForFirstDelayedMonth)
                                    //хватило только на умиеньшение суммы с которой идет процент запервый месяц просрочки
                                {
                                    credit.AmountToCountFineFromForFirstDelayedMonth -= paymentLeft;
                                }
                                else
                                {
                                    paymentLeft -= credit.AmountToCountFineFromForFirstDelayedMonth;
                                    credit.CountFineFromThisDate = credit.CountFineFromThisDate.AddDays(30);

                                    while (paymentLeft >= credit.AmountOfPaymentPerMonth)
                                    {
                                        paymentLeft -= credit.AmountOfPaymentPerMonth;
                                        credit.CountFineFromThisDate = credit.CountFineFromThisDate.AddDays(30);
                                    }
                                    credit.AmountToCountFineFromForFirstDelayedMonth = credit.AmountOfPaymentPerMonth
                                                                                       - paymentLeft;
                                }
                                credit.PaidForFine = this.CountUpFine(credit);

                            }
                            else
                            {
                                credit.IsRepaid = true;
                            }
                        }
                        credit.AllreadyPaid += paymentAmount;

                        _paymentBusinessComponent.Add(
                            _operatorId,
                            client.Requests.Where(
                                x =>
                                x.Status == RequestStatus.CreditProvided
                                && x.Credit.CreditId == Convert.ToInt32(cnct.ContractNO))
                                .FirstOrDefault()
                                .Credit.CreditId,
                            paymentAmount,
                            DateTime.Now);


                        _creditBusinessComponent.Update(credit);
                        _creditBusinessComponent = _ninjectKernel.Get<ICreditBusinessComponent>();


                        // if not re-created will fail on 2nd update
                        TabRepaymentClear(true);
                    }
                }
            }
        }

        private void RequestSendRequest_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure?", "Accept Confirmation", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                IList<Request> request = _requestBusinessComponent.GetByStatus(RequestStatus.Created);
                foreach (var req in request)
                {
                    if (req.RequestId == Convert.ToInt32(RequestRequestId.Text))
                    {
                        Request request2 = new Request() {RequestId = req.RequestId, ClientId = req.ClientId, OperatorId = _operatorId, Status = RequestStatus.ConfirmedByOperator };
                        _requestBusinessComponent.Update(request2/*req.ClientId, _operatorId, null, RequestStatus.ConfirmedByOperator*/);
                        _requestBusinessComponent = _ninjectKernel.Get<IRequestBusinessComponent>(); // if not re-created will fail on 2nd update

                        this.TabRequestClear(); // added by ilya
                        RequestReject.IsEnabled = false;
                        RequestSendRequest.IsEnabled = false;
                    }
                }
            }
        }

        private void RequestReject_Click(object sender, RoutedEventArgs e)
        {

            var rw = new RejectionWindow();
            string rejectionReason;

            MessageBoxResult messageBoxResult = rw.ShowDialog(out rejectionReason);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                IList<Request> request = _requestBusinessComponent.GetByStatus(RequestStatus.Created);
                foreach (var req in request)
                {
                    if (req.RequestId == Convert.ToInt32(RequestRequestId.Text))
                    {
                        Request request2 = new Request() {RequestId = req.RequestId, ClientId = req.ClientId, OperatorId = _operatorId, Status = RequestStatus.Denied, Note = rejectionReason };
                        _requestBusinessComponent.Update(request2/*req.ClientId, _operatorId, null, RequestStatus.ConfirmedByOperator*/);
                        _requestBusinessComponent = _ninjectKernel.Get<IRequestBusinessComponent>(); // if not re-created will fail on 2nd update

                        this.TabRequestClear(); // added by ilya
                        RequestReject.IsEnabled = false;
                        RequestSendRequest.IsEnabled = false;
                    }
                }
            }
        }

        private void RequestSearch_Click(object sender, RoutedEventArgs e)
        {
            TabRequestClear();
            if (Validate(false,false,true, false))
            {
                
                IList<Request> request = _requestBusinessComponent.GetByStatus(RequestStatus.Created).Where(req => req.RequestId == Convert.ToUInt32(this.RequestRequestId.Text)).ToList();
                foreach (var req in request.Where(req => req.RequestId == Convert.ToUInt32(this.RequestRequestId.Text)))
                {
                    this.RequestName.Text = req.Client.Name + " " + req.Client.LastName;// + " " + req.Client.Patronymic;
                    this.RequestCreditType.Text = req.CreditType.Name;
                    this.RequestAmount.Text = req.AmountOfCredit.ToString();
                    this.RequestCreditTypeIsAvailable.Text = req.CreditType.IsAvailable.ToString();
                }
                RequestReject.IsEnabled = request.Any();
                RequestSendRequest.IsEnabled = request.Any();
            }
        }

        private void tabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is TabControl)
            {

                if (TabRequestList.IsSelected)
                {
                    RequestDataList.Clear();
                    FillListView();
                    // 
                }
                if (TabRequest.IsSelected)
                {

                }
                if (TabRepayment.IsSelected)
                {
                    RepaymentDataList.Clear();
                    TabRepaymentClear(true);
                    FillListView();
                }
                if(TabAllowCredit.IsSelected)
                {

                }
            }
        }

        private void FillListView()
        {
            IList<Request> request = _requestBusinessComponent.GetByStatus(RequestStatus.Created);
            foreach (var req in request)
            {
                //Client client = _clientBusinessComponent.GetByID(req.ClientId);
                RequestDataList.Add(new OperatorRequestListClass()
                {
                    RequestId = req.RequestId,
                    PassportNo = req.Client.PassportNo,
                    Amount = req.AmountOfCredit.ToString(),
                    Credit = req.CreditType.Name
                });
            }
        }
        private void TabRequestClear()
        {
           // RequestRequestId.Clear();
            RequestName.Clear();
            RequestCreditType.Clear();
            RequestAmount.Clear();
            RequestCreditTypeIsAvailable.Clear();
        }
        private void TabRepaymentClear(bool ClearList)
        {
        //    RepaymentPassportNo.Clear();
            RepaymentName.Clear();
            RepaymentToRepayTheLoan.Clear();
            RepaymentToPay.Clear();
            RepaymentDebt.Clear();
            if (ClearList == true)
                RepaymentDataList.Clear();
        }

        private bool Validate(bool RPassportNo, bool ToPay, bool RequestId , bool APassportNo)
        {
            Validation.ValidationResult validationResult = new Validation.ValidationResult();
            if (RPassportNo)
            {
                validationResult = OperatorValidation.ValidatePassportNo(RepaymentPassportNo.Text);
            }
            if(ToPay)
            {
                validationResult = OperatorValidation.ValidateToPay(RepaymentToPay.Text);
            }
            if (RequestId)
            {
                validationResult = OperatorValidation.ValidateRequestId(RequestRequestId.Text);
            }
            if (APassportNo)
            {
                validationResult = OperatorValidation.ValidatePassportNo(AllowCreditPassportNo.Text);
            }
            if (validationResult.IsValid)
            {
                return true;
            }
            else
            {
                MessageBox.Show(validationResult.Error);
                return false;
            }
        }

        private void LogOffButton_Click(object sender, RoutedEventArgs e)
        {
            var window = Window.GetWindow(this);
            window.Title = "Bank Victoria - Login";
            window.Content = _ninjectKernel.Get<LoginPage>();
        }

        private void RepaymentListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.RepaymentOpen.IsEnabled = this.RepaymentListView.SelectedItem != null;
            this.RepaymentSubmit.IsEnabled = this.RepaymentListView.SelectedItem != null;
        }

        private void AllowCreditAllow_Click(object sender, RoutedEventArgs e)
        {
            //уменьшить баланс BankAccount
            
            if (AllowCreditDataList.Count > 0)
            {
                
                Request request = _requestBusinessComponent.GetByStatus(RequestStatus.ConfirmedBySecurityOfficer).Where(x => x.RequestId ==
                                                                       Convert.ToInt32(((ACreditListView)AllowCreditListView.SelectedValue).RequestId)).FirstOrDefault();
                _creditBusinessComponent.AllowCredit(_operatorId, request);
                AllowCreditDataList.Remove((ACreditListView)AllowCreditListView.SelectedValue);
            }
            else
                MessageBox.Show("Please select request");
        }

        private void AllowCreditSearch_Click(object sender, RoutedEventArgs e)
        {
            AllowCreditDataList.Clear();
            if(Validate(false, false, false, true))
            {
                IList<Request> request = _requestBusinessComponent.GetByStatus(RequestStatus.ConfirmedBySecurityOfficer).Where(x => x.Client.PassportNo == AllowCreditPassportNo.Text).ToList();
                foreach(var req in request)
                {
                    AllowCreditDataList.Add(new ACreditListView() { RequestId = req.RequestId.ToString(), PassportNo = req.Client.PassportNo,
                                                                    Amount = req.AmountOfCredit.ToString(), CreditType = req.CreditType.Name });

                }
                if (AllowCreditDataList.Count == 0)
                    MessageBox.Show("This client has no approved requests");
                else
                    AllowCreditListView.SelectedIndex = 0;
            }
        }

    }
    public class ACreditListView
    {
        public string RequestId { get; set; }
        public string PassportNo { get; set; }
        public string CreditType { get; set; }
        public string Amount { get; set; }
    }

}
