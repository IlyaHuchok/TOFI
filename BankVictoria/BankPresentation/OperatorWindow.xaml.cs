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
        private ObservableCollection<OperatorRequestListClass> dataList = new ObservableCollection<OperatorRequestListClass>();
        private ObservableCollection<ContractNoCreditType> dataList1 = new ObservableCollection<ContractNoCreditType>();
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

            RequestListView.ItemsSource = dataList;
            listView1.ItemsSource = dataList1;
            
        }

        private void RepaymentSearch_Click(object sender, RoutedEventArgs e)
        {
            IList<Request> request = _requestBusinessComponent.GetByStatus(RequestStatus.CreditProvided);
            foreach (var req in request)
            {
                if (req.Client.PassportNo == RepaymentPassportNo.Text)
                {
                    dataList1.Add(new ContractNoCreditType() { ContractNO = req.Credit.CreditId.ToString(), CreditType = req.CreditType.Name});
                }
            }
        }

        private void RepaymentOpen_Click(object sender, RoutedEventArgs e)
        {
            if (listView1.SelectedItem == null)
                MessageBox.Show("Сhoose ContractNo please");
            else {
                IList<Request> request = _requestBusinessComponent.GetByStatus(RequestStatus.CreditProvided);
                foreach (var req in request)
                {
                    ContractNoCreditType cnct = (ContractNoCreditType)listView1.SelectedItem;
                    if ((req.Client.PassportNo == RepaymentPassportNo.Text) && (Convert.ToInt32(cnct.ContractNO) == req.Credit.CreditId)) // CreditId==ContrqctNo
                    {
                        RepaymentName.Text = req.Client.Name + " " + req.Client.LastName + " " + req.Client.Patronymic;
                        RepaymentDebt.Text = req.Credit.Debt.ToString();//это как-то считаться должно 
                        RepaymentToRepayTheLoan.Text = (req.Credit.AmountOfPaymentPerMonth * req.CreditType.TimeMonths + Convert.ToInt32(RepaymentDebt.Text)).ToString();
                    }
                }
            }
        }
        private void CountUpNewDebt()
        {
            decimal standartAlreadyPaid;
            decimal allreadyPaid;
            if (listView1.SelectedItem == null)
                MessageBox.Show("Сhoose ContractNo please");
            else {
                IList<Request> request = _requestBusinessComponent.GetByStatus(RequestStatus.CreditProvided);
                foreach (var req in request)
                {
                    ContractNoCreditType cnct = (ContractNoCreditType)listView1.SelectedItem;
                    if ((req.Client.PassportNo == RepaymentFieldPassportNo.Text) && (Convert.ToInt32(cnct.ContractNO) == req.Credit.CreditId))
                    {
                        DateTime creditStart = req.Credit.StartDate;
                        DateTime now = DateTime.Now;
                        System.TimeSpan ts = now - creditStart;
                        int Mounths = ts.Days / 30;
                        standartAlreadyPaid = Mounths * req.Credit.AmountOfPaymentPerMonth;
                        allreadyPaid = req.Credit.AllreadyPaid;
                        if(allreadyPaid > standartAlreadyPaid)//мы переплатили и DateItWasDelay должен улететь вверх
                        {
                            double i = 0.0;//за сколько месяцев мы заплптили
                            while (allreadyPaid > standartAlreadyPaid)
                            {
                                
                                if (allreadyPaid - standartAlreadyPaid >= req.Credit.AmountOfPaymentPerMonth)//переплатили больше чем на месяц
                                {
                                    allreadyPaid -= req.Credit.AmountOfPaymentPerMonth;  //смотрим насколько далеко улетит DateItWasDelay
                                    i+= 1.0;
                                }
                                else
                                {
                                    allreadyPaid -= allreadyPaid - standartAlreadyPaid;//смотрим насколько далеко улетит DateItWasDelay
                                    i = i + (double)((allreadyPaid - standartAlreadyPaid)/ req.Credit.AmountOfPaymentPerMonth); 
                                }
                            }
                            int Days = (int)(i * 30);//на сколько дней вперед улетит DateItWasDelay
                            TimeSpan ts2 = new TimeSpan(Days,0,0,0);
                            //_creditBusinessComponent.GetByContractNo(contractNo).Update();     обновление DateItWasDelay

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
            Client client = _clientBusinessComponent.GetAll().Where(x=> x.PassportNo == RepaymentFieldPassportNo.Text).FirstOrDefault();
            ContractNoCreditType cnct = (ContractNoCreditType)listView1.SelectedValue;
            _paymentBusinessComponent.Add(
                _operatorId,
                client.Requests.Where(x =>x.Status == RequestStatus.CreditProvided && x.Credit.CreditId == Convert.ToInt32(cnct.ContractNO)).FirstOrDefault().Credit.CreditId,
                Convert.ToDecimal(FieldToPay.Text),
                DateTime.Now);

            //добавить взаимодействие с Credit (погашение додга? увеличение AllreadyPaid)
        }             

        private void RequestSendRequest_Click(object sender, RoutedEventArgs e)
        {
            IList<Request> request = _requestBusinessComponent.GetByStatus(RequestStatus.Created);
            foreach (var req in request)
            {
                if(req.RequestId == Convert.ToInt32(RequestRequestId.Text))
                    _requestBusinessComponent.Update(req.ClientId, _operatorId, null, RequestStatus.ConfirmedByOperator);
            }
        }

        private void RequestReject_Click(object sender, RoutedEventArgs e)
        {
            IList<Request> request = _requestBusinessComponent.GetByStatus(RequestStatus.Created);
            foreach (var req in request)
            {
                if (req.RequestId== Convert.ToInt32(RequestRequestId.Text))
                    _requestBusinessComponent.Update(req.ClientId, _operatorId, null, RequestStatus.Denied);
            }
        }

        private void RequestSearch_Click(object sender, RoutedEventArgs e)
        {
            IList<Request> request = _requestBusinessComponent.GetByStatus(RequestStatus.Created);
            foreach (var req in request)
            {
                if (req.RequestId == Convert.ToUInt32(RequestRequestId.Text))
                {
                    CreditFieldName.Text = req.Client.Name + " " + req.Client.LastName + " " + req.Client.Patronymic;
                    CreditTypeField.Text = req.CreditType.Name;
                    CreditAmount.Text = req.AmountOfCredit.ToString();
                }
            }
        }

        private void tabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(e.Source is TabControl)
            {
                if(TabRequestList.IsSelected)
                {
                    dataList1.Clear();
                    FillListView();
                    RequestRequestId.Text = "";
                    CreditFieldName.Text = "";
                    CreditTypeField.Text = "";
                    CreditAmount.Text = "";
                    RepaymentFieldPassportNo.Text = "";
                    RepaymentFieldName.Text = "";
                    RepaymentFieldToRepayTheLoan.Text = "";
                    FieldToPay.Text = "";
                    FieldDebt.Text = "";
                }
                if (TabRequest.IsSelected)
                {
                    dataList.Clear();
                    dataList1.Clear();
                    RepaymentFieldPassportNo.Text = "";
                    RepaymentFieldName.Text = "";
                    RepaymentFieldToRepayTheLoan.Text = "";
                    FieldToPay.Text = "";
                    FieldDebt.Text = "";
                }
                if(TabRepayment.IsSelected)
                {
                    dataList.Clear();
                    FillListView();
                    RequestRequestId.Text = "";
                    CreditFieldName.Text = "";
                    CreditTypeField.Text = "";
                    CreditAmount.Text = "";
                }
            }
        }

        private void FillListView()
        {
            IList<Request> request = _requestBusinessComponent.GetByStatus(RequestStatus.Created);
            foreach (var req in request)
            {
                //Client client = _clientBusinessComponent.GetByID(req.ClientId);
                dataList.Add(new OperatorRequestListClass()
                {
                    RequestId = req.RequestId,
                    PassportNo = req.Client.PassportNo,
                    Amount = req.AmountOfCredit.ToString(),
                    Credit = req.CreditType.Name
                });
            }
        }

        
    }
}
