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

using BankBL.Interfaces;

using Entities;

using Ninject;

namespace BankPresentation
{
    /// <summary>
    /// Логика взаимодействия для SecurityOfficerWindow.xaml
    /// </summary>
    public partial class SecurityOfficerWindow : Window
    {
        private readonly ISecurityOfficerBusinessComponent _securityOfficerBusinessComponent;
        private readonly IKernel _ninjectKernel;

        private const int RecordsPerPage = 1000;

        private int loadedRecordsCount = 0;

        private List<Request> _requestList;
        public SecurityOfficerWindow(IKernel ninjectKernel, ISecurityOfficerBusinessComponent securityOfficerBusinessComponent)
        {
            this._ninjectKernel = ninjectKernel;
            this._securityOfficerBusinessComponent = securityOfficerBusinessComponent;

            InitializeComponent();
            
            ButtonNextRequests.IsEnabled = false;
            ButtonPreviousRequests.IsEnabled = false;
            CreditHistoryButton.IsEnabled = false;
            ClientDetailsButton.IsEnabled = false;
            RejectButton.IsEnabled = false;
            AcceptButton.IsEnabled = false;

            RequestsDataGrid.SelectionMode = DataGridSelectionMode.Single;

            _requestList = _securityOfficerBusinessComponent.GetAllRequests().ToList();
            RequestsDataGrid.ItemsSource = _requestList;
        }

        private void button_Back_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button_Next_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RequestsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void AcceptButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RejectButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ClientDetailsButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CreditHistoryButton_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
