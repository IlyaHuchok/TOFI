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
using System.Windows.Navigation;

using BankBL.Interfaces;

using Entities;
using Entities.Enums;

using Ninject;
using Ninject.Parameters;

namespace BankPresentation
{
    /// <summary>
    /// Логика взаимодействия для SecurityOfficerWindow.xaml
    /// </summary>
    public partial class SecurityOfficerPage : Page
    {
        private ISecurityOfficerBusinessComponent securityOfficerBusinessComponent;
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

        private void RefreshPage(string lastName)
        {
            _requestList = new ObservableCollection<Request>(this.securityOfficerBusinessComponent.GetAllRequestsByClientLastname(lastName));
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
                this.securityOfficerBusinessComponent.ApproveRequest(securityOfficerId, selectedRequest);
                RefreshPage();//_requestList.Remove(selectedRequest);
            }
        }

        private void RejectButton_Click(object sender, RoutedEventArgs e)
        {
            var rejectionWindow = _ninjectKernel.Get<RejectionWindow>();
            string rejectionReason;
            //var bbb = aaa.ShowDialog(out rejectionReason);

            MessageBoxResult messageBoxResult = rejectionWindow.ShowDialog(out rejectionReason);//MessageBox.Show("Are you sure?", "Accept Confirmation", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                var selectedRequest = (Request)RequestsListView.SelectedItem;
                this.securityOfficerBusinessComponent.RejectRequest(securityOfficerId, selectedRequest, rejectionReason);
                securityOfficerBusinessComponent = _ninjectKernel.Get<ISecurityOfficerBusinessComponent>(); // if not re-created will fail on 2nd update
                RefreshPage();//_requestList.Remove(selectedRequest);
            }
        }

        private void ClientDetailsButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedRequest = (Request)RequestsListView.SelectedItem;

            var clientDetailsWindow = _ninjectKernel.Get<ClientDetailsWindow>(
                new ConstructorArgument("clientId", selectedRequest.ClientId),
                new ConstructorArgument("salary", selectedRequest.Salary));

            clientDetailsWindow.ShowDialog();
        }

        private void CreditHistoryButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedRequest = (Request)RequestsListView.SelectedItem;

            var clientCreditHistoryWindow = _ninjectKernel.Get<ClientCreditHistoryWindow>(
                new ConstructorArgument("clientId", selectedRequest.ClientId));
            clientCreditHistoryWindow.ShowDialog();
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

        private void CreditTypeHyperLinkClick(object sender, RoutedEventArgs e)
        {
            var hyperlink = sender as Hyperlink;
            var creditTypeId = (int)(hyperlink.CommandParameter);

            var creditTypesWindow = _ninjectKernel.Get<CreditTypesWindow>(
                new ConstructorArgument("role", UserRole.Operator),
                new ConstructorArgument("selectedCreditTypeId", creditTypeId));
            creditTypesWindow.ShowDialog();
        }
        
        private void Reload_Click(object sender, RoutedEventArgs e)
        {
            this.RefreshPage();
        }

        private void LastnameTextBox_OnKeyDown_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.RefreshPage(LastnameTextBox.Text);
            }
        }

        private void SearchByLastNameButton_Click(object sender, RoutedEventArgs e)
        {
            this.RefreshPage(LastnameTextBox.Text);
        }

        private void LogOffButton_Click(object sender, RoutedEventArgs e)
        {
            var window = Window.GetWindow(this);
            window.Title = "Bank Victoria - Login";
            window.Content = _ninjectKernel.Get<LoginPage>();
        }
    }
}
