using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class SecurityOfficerPage : Page
    {
        private readonly ISecurityOfficerBusinessComponent securityOfficerBusinessComponent;
        private readonly IKernel _ninjectKernel;
        private readonly int securityOfficerId;

        private const int RecordsPerPage = 1000;

        private int loadedRecordsCount = 0;
        private int currentPageNo = 0;
        private int maxPageNo = 0;

        private ObservableCollection<Request> _requestList;

        public ObservableCollection<Request> RequestList
        {
            get
            {
                return _requestList;
            }
        }

        public SecurityOfficerPage(IKernel ninjectKernel, ISecurityOfficerBusinessComponent securityOfficerBusinessComponent)
        {
            this._ninjectKernel = ninjectKernel;
            this.securityOfficerBusinessComponent = securityOfficerBusinessComponent;

            InitializeComponent();
            
            ButtonNextRequests.IsEnabled = false;
            ButtonPreviousRequests.IsEnabled = false;

            DisableRequestActionButtons();

            RequestsListView.SelectionMode = SelectionMode.Single;

            this.RefreshPage();
        }

        private void RefreshPage()
        {
            _requestList = new ObservableCollection<Request>(this.securityOfficerBusinessComponent.GetAllRequests());
            RequestsListView.ItemsSource = _requestList;
        }

        private void DisableRequestActionButtons()
        {
            CreditHistoryButton.IsEnabled = false;
            ClientDetailsButton.IsEnabled = false;
            RejectButton.IsEnabled = false;
            AcceptButton.IsEnabled = false;
        }

        private void EnableRequestActionButtons()
        {
            CreditHistoryButton.IsEnabled = true;
            ClientDetailsButton.IsEnabled = true;
            RejectButton.IsEnabled = true;
            AcceptButton.IsEnabled = true;
        }

        private void button_Back_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button_Next_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AcceptButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure?", "Accept Confirmation", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                var selectedRequest = (Request)RequestsListView.SelectedItem;
                this.securityOfficerBusinessComponent.AllowCredit(securityOfficerId, selectedRequest);
                RefreshPage();//_requestList.Remove(selectedRequest);
            }
        }

        private void RejectButton_Click(object sender, RoutedEventArgs e)
        {
            var aaa = new RejectionWindow();
            string rejectionReason;
            //var bbb = aaa.ShowDialog(out rejectionReason);

            MessageBoxResult messageBoxResult = aaa.ShowDialog(out rejectionReason);//MessageBox.Show("Are you sure?", "Accept Confirmation", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                var selectedRequest = (Request)RequestsListView.SelectedItem;
                this.securityOfficerBusinessComponent.RejectRequest(securityOfficerId, selectedRequest, rejectionReason);
                RefreshPage();//_requestList.Remove(selectedRequest);
            }
        }

        private void ClientDetailsButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void CreditHistoryButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void RequestsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RequestsListView.SelectedItem != null)
            {
                this.EnableRequestActionButtons();
            }
            else
            {
                this.DisableRequestActionButtons();
            }
        }

        private void Reload_Click(object sender, RoutedEventArgs e)
        {
            this.RefreshPage();
        }
    }
}
