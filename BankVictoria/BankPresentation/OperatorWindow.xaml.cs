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
        private readonly IClientBusinessComponent _clientBusinessComponent;
        private readonly IRequestBusinessComponent _requestBusinessComponent;

        public OperatorWindow(IClientBusinessComponent clientBusinessComponent, IRequestBusinessComponent requestBusinessComponent)
        {
            _clientBusinessComponent = clientBusinessComponent;
            _requestBusinessComponent = requestBusinessComponent;
            InitializeComponent();
        }

        private void RepaymentSubmit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Details_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Name: N " + Environment.NewLine +
                            "Birthday B" + Environment.NewLine +
                            "...");
        }

        private void CreditSearch_Click(object sender, RoutedEventArgs e)
        {
            IList<Request> request = _requestBusinessComponent.GetByStatus(RequestStatus.Created);
            foreach(var req in request)
            {
                if(req.Client.PassportNo == CreditFieldPassportNo.Text)
                {
                    CreditFieldName.Text = req.Client.Name + " " + req.Client.LastName + " " + req.Client.Patronymic;
                    CreditTypeField.Text = req.CreditType.Name;
                    CreditAmount.Text = req.AmountOfCredit.ToString();
                }
            }
            
        }

        private void FillListBox()
        {
            IList<Request> request = _requestBusinessComponent.GetByStatus(RequestStatus.Created);
            foreach (var req in request)
            {
                Client client = _clientBusinessComponent.GetByID(req.ClientId);
                listView.Items.Add(new ClientAmountCredit()
                {
                    PassportNo = req.Client.PassportNo,
                    Amount = req.AmountOfCredit.ToString(),
                    Credit = req.CreditType.Name
                });
            }            
        }

        private void Reject_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Request_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
