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

namespace BankPresentation
{
    /// <summary>
    /// Логика взаимодействия для OperatorWindow.xaml
    /// </summary>
    public partial class OperatorWindow : Window
    {
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
            
        }

        private void FieldToPay_TextChanged(object sender, TextChangedEventArgs e)
        {

           // FieldDebt.Text = 
        }

        private void RepaymentSearch_Click(object sender, RoutedEventArgs e)
        {
            IList<Request> request = _requestBusinessComponent.GetByStatus(RequestStatus.CreditProvided);
            foreach (var req in request)
            {
                if (req.Client.PassportNo == RepaymentFieldPassportNo.Text)
                {
                    RepaymentFieldName.Text = req.Client.Name + " " + req.Client.LastName + " " + req.Client.Patronymic;
                    RepaymentFieldToRepayTheLoan.Text = (req.Credit.AmountOfPaymentPerMonth*req.CreditType.TimeMonths + Convert.ToInt32(FieldDebt.Text)).ToString();
                    CreditAmount.Text = req.AmountOfCredit.ToString();
                }
            }
        }

        private void RepaymentSubmit_Click(object sender, RoutedEventArgs e)
        {
            Client client = _clientBusinessComponent.GetAll().Where(x=> x.PassportNo == RepaymentFieldPassportNo.Text).FirstOrDefault();

            _paymentBusinessComponent.Add(_operatorId,
                client.Requests.Where(x =>x.Status == RequestStatus.CreditProvided && x.Credit.CreditId == Convert.ToUInt32(CreditFieldCreditId.Text)).FirstOrDefault().Credit.CreditId,
                client.Requests.Where(x=>x.Credit.CreditId == Convert.ToUInt32(CreditFieldCreditId.Text)).FirstOrDefault().Credit.ContractNo,
                Convert.ToDecimal(FieldToPay.Text),DateTime.Now);
        }             

        private void Request_Click(object sender, RoutedEventArgs e)
        {
            IList<Request> request = _requestBusinessComponent.GetByStatus(RequestStatus.Created);
            foreach (var req in request)
            {
                _requestBusinessComponent.Update(_operatorId, null, RequestStatus.ConfirmedByOperator);
            }
        }

        private void Reject_Click(object sender, RoutedEventArgs e)
        {
            IList<Request> request = _requestBusinessComponent.GetByStatus(RequestStatus.Created);
            foreach (var req in request)
            {
                _requestBusinessComponent.Update(_operatorId, null, RequestStatus.Denied);
            }
        }

        private void CreditSearch_Click(object sender, RoutedEventArgs e)
        {
            IList<Request> request = _requestBusinessComponent.GetByStatus(RequestStatus.Created);
            foreach (var req in request)
            {
                if (req.Credit.CreditId == Convert.ToUInt32(CreditFieldCreditId.Text))
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
                if(TabCreditList.IsSelected)
                {
                    FillListView();
                    CreditFieldCreditId.Text = "";
                    CreditFieldName.Text = "";
                    CreditTypeField.Text = "";
                    CreditAmount.Text = "";
                    RepaymentFieldPassportNo.Text = "";
                    RepaymentFieldName.Text = "";
                    RepaymentFieldToRepayTheLoan.Text = "";
                    FieldToPay.Text = "";
                    FieldDebt.Text = "";
                }
                if (TabCredit.IsSelected)
                {
                    listView.Items.Clear();
                    RepaymentFieldPassportNo.Text = "";
                    RepaymentFieldName.Text = "";
                    RepaymentFieldToRepayTheLoan.Text = "";
                    FieldToPay.Text = "";
                    FieldDebt.Text = "";
                }
                if(TabRepayment.IsSelected)
                {
                    listView.Items.Clear();
                    FillListView();
                    CreditFieldCreditId.Text = "";
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
                listView.Items.Add(new ClientAmountCredit()
                {
                    CreditId = req.Credit.CreditId,
                    PassportNo = req.Client.PassportNo,
                    Amount = req.AmountOfCredit.ToString(),
                    Credit = req.CreditType.Name
                });
            }
        }
    }
}
