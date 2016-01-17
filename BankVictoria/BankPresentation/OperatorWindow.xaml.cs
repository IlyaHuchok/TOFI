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
    {
        private ObservableCollection<OperatorRequestListClass> RequestDataList = new ObservableCollection<OperatorRequestListClass>();
        private ObservableCollection<ContractNoCreditType> RepaymentDataList = new ObservableCollection<ContractNoCreditType>();
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
            RepaymentToPay.MaxLength = OperatorValidation.ToPayMaxLength;
            RequestRequestId.MaxLength = OperatorValidation.RequestIdMaxLength;

            RequestListView.ItemsSource = RequestDataList;
            RepaymentListView.ItemsSource = RepaymentDataList;

            RepaymentOpen.IsEnabled = false;
            RequestReject.IsEnabled = false;
            RequestSendRequest.IsEnabled = false;
            RepaymentSubmit.IsEnabled = false;
        }

        private void RepaymentSearch_Click(object sender, RoutedEventArgs e)
        {
            if (Validate(true,false,false))
            {
                RequestDataList.Clear();
                IList<Request> request = _requestBusinessComponent.GetByStatus(RequestStatus.CreditProvided);
                foreach (var req in request)
                {
                    if (req.Client.PassportNo == RepaymentPassportNo.Text)
                    {
                        RepaymentDataList.Add(new ContractNoCreditType() { ContractNO = req.Credit.CreditId.ToString(), CreditType = req.CreditType.Name });
                    }
                }
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
                    CountUpNewDebt();//высчитываем долг
                    RepaymentName.Text = req.Client.Name + " " + req.Client.LastName;// + " " + req.Client.Patronymic;
                    RepaymentDebt.Text = req.Credit.PaidForFine.ToString();
                    RepaymentToRepayTheLoan.Text = (req.Credit.AmountOfPaymentPerMonth * req.CreditType.TimeMonths + req.Credit.PaidForFine).ToString();
                }
            }
        }

        private void CountUpNewDebt()
        {
            decimal standartAlreadyPaid;
            decimal allreadyPaid;
            if (RepaymentListView.SelectedItem == null)
                MessageBox.Show("Сhoose ContractNo please");
            else {
                ContractNoCreditType cnct = (ContractNoCreditType)RepaymentListView.SelectedValue;
                Request request = _requestBusinessComponent.GetByStatus(RequestStatus.CreditProvided).Where(x => x.Client.PassportNo == RepaymentPassportNo.Text &&
                                                                        Convert.ToInt32(cnct.ContractNO) == x.Credit.CreditId).FirstOrDefault();
                Credit credit = _creditBusinessComponent.GetAll().Where(x => x.CreditId == Convert.ToInt32(cnct.ContractNO)).FirstOrDefault();
                DateTime creditStart = request.Credit.StartDate;
                DateTime now = DateTime.Now;
                System.TimeSpan ts = now - creditStart;
                int Mounths = ts.Days / 30;
                standartAlreadyPaid = Mounths * request.Credit.AmountOfPaymentPerMonth;
                allreadyPaid = request.Credit.AllreadyPaid;
                if (allreadyPaid > standartAlreadyPaid)//мы переплатили и CountFineFromThisDate должен улететь вверх
                {
                    double i = 0.0;//за сколько месяцев мы заплптили
                    while (allreadyPaid > standartAlreadyPaid)
                    {

                        if (allreadyPaid - standartAlreadyPaid >= request.Credit.AmountOfPaymentPerMonth)//переплатили больше чем на месяц
                        {
                            allreadyPaid -= request.Credit.AmountOfPaymentPerMonth;  //смотрим насколько далеко улетит CountFineFromThisDate
                            i += 1.0;
                        }
                        else
                        {
                            allreadyPaid -= allreadyPaid - standartAlreadyPaid;//смотрим насколько далеко улетит CountFineFromThisDate
                            i = i + (double)((allreadyPaid - standartAlreadyPaid) / request.Credit.AmountOfPaymentPerMonth);
                        }
                    }
                    int Days = (int)(i * 30);//на сколько дней вперед улетит CountFineFromThisDate                        
                    _creditBusinessComponent.Update(credit.CreditId, credit.CountFineFromThisDate + new TimeSpan(Days, 0, 0, 0));
                    _creditBusinessComponent = _ninjectKernel.Get<ICreditBusinessComponent>(); // if not re-created will fail on 2nd update
                    allreadyPaid = request.Credit.AllreadyPaid;
                }
                else if (allreadyPaid < standartAlreadyPaid)//у нас есть долг
                {

                    TimeSpan ts3 = DateTime.Now - credit.CountFineFromThisDate; ;
                    int daysFromTheStartOfTheDebt = ts3.Days;
                    decimal Debt = daysFromTheStartOfTheDebt * credit.AmountOfPaymentPerMonth * (decimal)0.01; // 0.01 = 1% --- пеня за день
                    while (daysFromTheStartOfTheDebt > 30)
                    {
                        daysFromTheStartOfTheDebt -= 30;
                        Debt += daysFromTheStartOfTheDebt * credit.AmountOfPaymentPerMonth * (decimal)0.01;/// не Debt += Debt !!!!!
                    }
                    _creditBusinessComponent.Update(credit.CreditId, credit.AllreadyPaid, Debt);
                    _creditBusinessComponent = _ninjectKernel.Get<ICreditBusinessComponent>(); // if not re-created will fail on 2nd update
                    allreadyPaid = request.Credit.AllreadyPaid;
                }
                else if (allreadyPaid == standartAlreadyPaid)
                {
                    DateTime newDateToStartDebt = DateTime.Now;
                    while (newDateToStartDebt.Day != 1)//просрочка начинается в первый день месяца
                    {
                        newDateToStartDebt -= new TimeSpan(1, 0, 0, 0, 0);
                    }
                    _creditBusinessComponent.Update(credit.CreditId, newDateToStartDebt);
                    _creditBusinessComponent = _ninjectKernel.Get<ICreditBusinessComponent>(); // if not re-created will fail on 2nd update
                }
            }
        }

        private void RepaymentSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (Validate(false,true,false))
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure?", "Accept Confirmation", MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    ContractNoCreditType cnct = (ContractNoCreditType)RepaymentListView.SelectedValue;
                    Client client = _clientBusinessComponent.GetAll().Where(x => x.PassportNo == RepaymentPassportNo.Text).FirstOrDefault();
                    Credit credit = _creditBusinessComponent.GetAll().Where(x => x.CreditId == Convert.ToInt32(cnct.ContractNO)).FirstOrDefault();
                    _paymentBusinessComponent.Add(
                        _operatorId,
                        client.Requests.Where(x => x.Status == RequestStatus.CreditProvided && x.Credit.CreditId == Convert.ToInt32(cnct.ContractNO)).FirstOrDefault().Credit.CreditId,
                        Convert.ToDecimal(RepaymentToPay.Text),
                        DateTime.Now);

                    _creditBusinessComponent.Update(credit.CreditId, credit.AllreadyPaid + Convert.ToInt32(RepaymentToPay.Text), credit.PaidForFine - Convert.ToInt32(RepaymentToPay.Text));
                    _creditBusinessComponent = _ninjectKernel.Get<ICreditBusinessComponent>(); // if not re-created will fail on 2nd update
                    TabRepaymentClear(false);
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
            if (Validate(false,false,true))
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
            RequestRequestId.Clear();
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

        private bool Validate(bool PassportNo, bool ToPay, bool RequestId)
        {
            Validation.ValidationResult validationResult = new Validation.ValidationResult();
            if (PassportNo)
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
            window.Content = _ninjectKernel.Get<LoginPage>();
        }

        private void RepaymentListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.RepaymentOpen.IsEnabled = this.RepaymentListView.SelectedItem != null;
            this.RepaymentSubmit.IsEnabled = this.RepaymentListView.SelectedItem != null;
        }
    }
}
