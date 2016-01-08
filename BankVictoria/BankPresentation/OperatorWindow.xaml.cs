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
namespace BankPresentation
{
    /// <summary>
    /// Логика взаимодействия для OperatorWindow.xaml
    /// </summary>
    public partial class OperatorWindow : Window
    {
        private ObservableCollection<OperatorRequestListClass> RequestDataList = new ObservableCollection<OperatorRequestListClass>();
        private ObservableCollection<ContractNoCreditType> RepaymentDataList = new ObservableCollection<ContractNoCreditType>();
        private readonly int _operatorId;
        private readonly IClientBusinessComponent _clientBusinessComponent;
        private readonly IRequestBusinessComponent _requestBusinessComponent;
        private readonly IPaymentBusinessComponent _paymentBusinessComponent;

        public OperatorWindow(IClientBusinessComponent clientBusinessComponent, IRequestBusinessComponent requestBusinessComponent, IPaymentBusinessComponent paymentBusinessComponent,
            int operatorId)
        {
            _clientBusinessComponent = clientBusinessComponent;
            _requestBusinessComponent = requestBusinessComponent;
            _paymentBusinessComponent = paymentBusinessComponent;
            _operatorId = operatorId;

            InitializeComponent();

            RequestListView.ItemsSource = RequestDataList;
            RepaymentListView.ItemsSource = RepaymentDataList;
            
        }

        private void RepaymentSearch_Click(object sender, RoutedEventArgs e)
        {
            IList<Request> request = _requestBusinessComponent.GetByStatus(RequestStatus.CreditProvided);
            foreach (var req in request)
            {
                if (req.Client.PassportNo == RepaymentPassportNo.Text)
                {
                    RepaymentDataList.Add(new ContractNoCreditType() { ContractNO = req.Credit.CreditId.ToString(), CreditType = req.CreditType.Name});
                }
            }
        }

        private void RepaymentOpen_Click(object sender, RoutedEventArgs e)
        {
            if (RepaymentListView.SelectedItem == null)
                MessageBox.Show("Сhoose ContractNo please");
            else {
                IList<Request> request = _requestBusinessComponent.GetByStatus(RequestStatus.CreditProvided);
                foreach (var req in request)
                {
                    ContractNoCreditType cnct = (ContractNoCreditType)RepaymentListView.SelectedItem;
                    if ((req.Client.PassportNo == RepaymentPassportNo.Text) && (Convert.ToInt32(cnct.ContractNO) == req.Credit.CreditId)) // CreditId==ContrqctNo
                    {
                        RepaymentName.Text = req.Client.Name + " " + req.Client.LastName + " " + req.Client.Patronymic;
                        RepaymentDebt.Text = req.Credit.PaidForFine.ToString();//это как-то считаться должно 
                        RepaymentToRepayTheLoan.Text = (req.Credit.AmountOfPaymentPerMonth * req.CreditType.TimeMonths + Convert.ToInt32(RepaymentDebt.Text)).ToString();
                    }
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
                IList<Request> request = _requestBusinessComponent.GetByStatus(RequestStatus.CreditProvided);
                foreach (var req in request)
                {
                    ContractNoCreditType cnct = (ContractNoCreditType)RepaymentListView.SelectedItem;
                    if ((req.Client.PassportNo == RepaymentPassportNo.Text) && (Convert.ToInt32(cnct.ContractNO) == req.Credit.CreditId))
                    {
                        DateTime creditStart = req.Credit.StartDate;
                        DateTime now = DateTime.Now;
                        System.TimeSpan ts = now - creditStart;
                        int Mounths = ts.Days / 30;
                        standartAlreadyPaid = Mounths * req.Credit.AmountOfPaymentPerMonth;
                        allreadyPaid = req.Credit.AllreadyPaid;
                        if(allreadyPaid > standartAlreadyPaid)//мы переплатили и CountFineFromThisDate должен улететь вверх
                        {
                            double i = 0.0;//за сколько месяцев мы заплптили
                            while (allreadyPaid > standartAlreadyPaid)
                            {
                                
                                if (allreadyPaid - standartAlreadyPaid >= req.Credit.AmountOfPaymentPerMonth)//переплатили больше чем на месяц
                                {
                                    allreadyPaid -= req.Credit.AmountOfPaymentPerMonth;  //смотрим насколько далеко улетит CountFineFromThisDate
                                    i += 1.0;
                                }
                                else
                                {
                                    allreadyPaid -= allreadyPaid - standartAlreadyPaid;//смотрим насколько далеко улетит CountFineFromThisDate
                                    i = i + (double)((allreadyPaid - standartAlreadyPaid)/ req.Credit.AmountOfPaymentPerMonth); 
                                }
                            }
                            int Days = (int)(i * 30);//на сколько дней вперед улетит CountFineFromThisDate
                            TimeSpan ts2 = new TimeSpan(Days,0,0,0);
                            //_creditBusinessComponent.GetByContractNo(contractNo).Update();     обновление CountFineFromThisDate

                        }
                        if (allreadyPaid < standartAlreadyPaid)//у нас есть долг
                        {

                        }
                        if(allreadyPaid == standartAlreadyPaid)
                        {

                        }
                    }
                }
            }
        }

        private void RepaymentSubmit_Click(object sender, RoutedEventArgs e)
        {
            Client client = _clientBusinessComponent.GetAll().Where(x=> x.PassportNo == RepaymentPassportNo.Text).FirstOrDefault();
            ContractNoCreditType cnct = (ContractNoCreditType)RepaymentListView.SelectedValue;
            _paymentBusinessComponent.Add(
                _operatorId,
                client.Requests.Where(x =>x.Status == RequestStatus.CreditProvided && x.Credit.CreditId == Convert.ToInt32(cnct.ContractNO)).FirstOrDefault().Credit.CreditId,
                Convert.ToDecimal(RepaymentToPay.Text),
                DateTime.Now);

            //добавить взаимодействие с Credit (погашение додга? увеличение AllreadyPaid)
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
                        _requestBusinessComponent.Update(req.ClientId, _operatorId, null, RequestStatus.ConfirmedByOperator);
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
                        _requestBusinessComponent.Update(req.ClientId, _operatorId, null, RequestStatus.Denied);
                }
            }
        }

        private void RequestSearch_Click(object sender, RoutedEventArgs e)
        {
            IList<Request> request = _requestBusinessComponent.GetByStatus(RequestStatus.Created);
            foreach (var req in request)
            {
                if (req.RequestId == Convert.ToUInt32(RequestRequestId.Text))
                {
                    RequestName.Text = req.Client.Name + " " + req.Client.LastName + " " + req.Client.Patronymic;
                    RequestCreditType.Text = req.CreditType.Name;
                    RequestAmount.Text = req.AmountOfCredit.ToString();
                }
            }
        }

        private void tabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(e.Source is TabControl)
            {
                if(TabRequestList.IsSelected)
                {
                    FillListView();
                    TabRequestClear();
                    TabRepaymentClear();
                }
                if (TabRequest.IsSelected)
                {
                    RequestDataList.Clear();                    
                    TabRepaymentClear();
                }
                if(TabRepayment.IsSelected)
                {
                    RequestDataList.Clear();
                    FillListView();
                    TabRequestClear();
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
        }
        private void TabRepaymentClear()
        {
            RepaymentPassportNo.Clear();
            RepaymentName.Clear();
            RepaymentToRepayTheLoan.Clear();
            RepaymentToPay.Clear();
            RepaymentDebt.Clear();
            RepaymentDataList.Clear();
        }



    }
}
