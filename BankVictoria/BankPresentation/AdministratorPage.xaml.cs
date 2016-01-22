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
using Entities.Enums;

using Ninject;
using Ninject.Parameters;

namespace BankPresentation
{
    /// <summary>
    /// Логика взаимодействия для AdministratorWindow.xaml
    /// </summary>
    public partial class AdministratorPage : Page
    {
        private IClientBusinessComponent _clientBusinessComponent;
        private IUserBusinessComponent _userBusinessComponent;
        private readonly ICreditTypeBusinessComponent _creditTypeBusinessComponent;

        private readonly IKernel _ninjectKernel;

        private ObservableCollection<CreditType> _creditTypesList;

        public ObservableCollection<CreditType> CreditTypesList
        {
            get
            {
                return _creditTypesList;
            }
        }

        private ObservableCollection<User> _usersList;

        public ObservableCollection<User> UsersList
        {
            get
            {
                return _usersList;
            }
        }

        public AdministratorPage(IUserBusinessComponent userBusinessComponent, IClientBusinessComponent clientBusinessComponent, ICreditTypeBusinessComponent creditTypeBusinessComponent, IKernel ninjectKernel)
        {
            this._userBusinessComponent = userBusinessComponent;
            this._clientBusinessComponent = clientBusinessComponent;
            this._creditTypeBusinessComponent = creditTypeBusinessComponent;
            this._ninjectKernel = ninjectKernel;
            InitializeComponent();

            DeleteCreditTypeButton.IsEnabled = false;
            DisableCreditTypeButton.IsEnabled = false;

            UpdateClientButton.IsEnabled = false;
            DeleteButton.IsEnabled = false;
            DisableButton.IsEnabled = false;
            EnableButton.IsEnabled = false;


            defaultOption.IsChecked = true;

            CreditTypesListView.SelectionMode = SelectionMode.Single;
            UsersListView.SelectionMode = SelectionMode.Single;
            RefreshCreditTypes();
        }

        private void AddCreditTypeClick(object sender, RoutedEventArgs e)
        {
            var creditTypeWindow = _ninjectKernel.Get<CreditTypeWindow>();
            creditTypeWindow.ShowDialog();
            this.RefreshCreditTypes();
        }

        private void DisableCreditTypeClick(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Selected credit type will no longer be available for clients! Are you sure?", "Accept Confirmation", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                var selectedItem = (CreditType)CreditTypesListView.SelectedItem;
                selectedItem.IsAvailable = false;
                _creditTypeBusinessComponent.Disable(selectedItem);
                DisableCreditTypeButton.IsEnabled = false;
            }
        }

        private void DeleteCreditTypeClick(object sender, RoutedEventArgs e)
        {
            var selectedItem = (CreditType)CreditTypesListView.SelectedItem;
            if (_creditTypeBusinessComponent.IsUsed(selectedItem))
            {
                MessageBox.Show("Selected Credit Type currently in use! It cannot be deleted!");
            }
            else
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure?", "Accept Confirmation", MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    _creditTypeBusinessComponent.Delete(selectedItem);
                    _creditTypesList.Remove(selectedItem);
                }
            }
        }

        private void RefreshCreditTypes()
        {
            _creditTypesList = new ObservableCollection<CreditType>(this._creditTypeBusinessComponent.GetAllCreditTypes());
            CreditTypesListView.ItemsSource = _creditTypesList;
        }


        private void RefreshUsers(string option = "Clients and Employees")
        {
            switch (option)
            {
                case "Clients and Employees":
                    _usersList = new ObservableCollection<User>(this._userBusinessComponent.GetAll());
                    break;
                case "Clients":
                    _usersList = new ObservableCollection<User>(this._userBusinessComponent.GetAll().Where(x=>x.Role == UserRole.Client));
                    break;
                case "Employees":
                    _usersList = new ObservableCollection<User>(this._userBusinessComponent.GetAll().Where(x => x.Role != UserRole.Client));
                    break;
            }
            UsersListView.ItemsSource = _usersList;

            UpdateClientButton.IsEnabled = false;
            DeleteButton.IsEnabled = false;
            DisableButton.IsEnabled = false;
            EnableButton.IsEnabled = false;
        }

        private string selectedOption;
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            // ... Get RadioButton reference.
            var button = sender as RadioButton;

            selectedOption = button.Content.ToString();
            this.RefreshUsers(selectedOption);
        }

        private void EnableButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = (User)UsersListView.SelectedItem;
            selectedItem.IsActive = true;
            _userBusinessComponent.Update(selectedItem);
            EnableButton.IsEnabled = false;
            DisableButton.IsEnabled = true;
        }

        private void DisableButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = (User)UsersListView.SelectedItem;
            selectedItem.IsActive = false;
            _userBusinessComponent.Update(selectedItem);
            EnableButton.IsEnabled = true;
            DisableButton.IsEnabled = false;
        }

        private void UpdateClientButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = (User)UsersListView.SelectedItem;
            var clientToUpdate = _clientBusinessComponent.GetByUserId(selectedItem.UserId);
            var registrationWindow =
                _ninjectKernel.Get<RegistrationWindow>(
                    new ConstructorArgument("userRole", UserRole.Admin),
                    new ConstructorArgument("clientToUpdate", clientToUpdate));
            registrationWindow.ShowDialog();
        }

        private void AddUserButton_Click(object sender, RoutedEventArgs e)
        {
            var registrationWindow = _ninjectKernel.Get<RegistrationWindow>(
                new ConstructorArgument("userRole", UserRole.Admin),
                new ConstructorArgument("clientToUpdate", (Client)null));
            registrationWindow.ShowDialog();
            this.RefreshUsers(selectedOption);
        }

        private void UsersListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DeleteCreditTypeButton.IsEnabled = false;
            DisableCreditTypeButton.IsEnabled = false;

            var selectedItem = (User)UsersListView.SelectedItem;
            if (selectedItem != null)
            {
                DeleteButton.IsEnabled = true;
                if (selectedItem.Role == UserRole.Client)
                {
                    UpdateClientButton.IsEnabled = true;
                    DisableButton.IsEnabled = false;
                    EnableButton.IsEnabled = false;
                }
                else
                {
                    if (selectedItem.Role == UserRole.Admin)
                    {
                        UpdateClientButton.IsEnabled = false;
                        DisableButton.IsEnabled = false;
                        EnableButton.IsEnabled = false;
                    }
                    else
                    {
                        UpdateClientButton.IsEnabled = false;
                        DisableButton.IsEnabled = selectedItem.IsActive ?? false;
                        EnableButton.IsEnabled = selectedItem.IsActive.HasValue && !selectedItem.IsActive.Value;
                    }
                }
            }
        }

        private void CreditTypesListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = (CreditType)CreditTypesListView.SelectedItem;
            if (selectedItem != null)
            {
                DeleteCreditTypeButton.IsEnabled = true;
                this.DisableCreditTypeButton.IsEnabled = selectedItem.IsAvailable;
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure?", "Accept Confirmation", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                var selectedItem = (User)UsersListView.SelectedItem;
                if (selectedItem.Role == UserRole.Client)
                {
                    var clientToDelete = _clientBusinessComponent.GetByUserId(selectedItem.UserId);
                    _clientBusinessComponent.Delete(clientToDelete);
                    _usersList.Remove(selectedItem);
                    _clientBusinessComponent = _ninjectKernel.Get<IClientBusinessComponent>();
                }
                else
                {
                    if (selectedItem.Role == UserRole.Admin
                        && _userBusinessComponent.GetAll().Count(x => x.Role == UserRole.Admin) <= 1)
                    {
                        MessageBox.Show("This is the last admin. Record cannot be deleted!");
                    }
                    else
                    {
                        _userBusinessComponent.Delete(selectedItem);
                        _usersList.Remove(selectedItem);
                        _userBusinessComponent = _ninjectKernel.Get<IUserBusinessComponent>();
                    }
                }
            }
        }

        private void LogOffButton_Click(object sender, RoutedEventArgs e)
        {
            var window = Window.GetWindow(this);
            window.Title = "Bank Victoria - Login";
            window.Content = _ninjectKernel.Get<LoginPage>();
        }
    }
}
