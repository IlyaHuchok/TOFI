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
    /// Interaction logic for ClientDetailsWindow.xaml
    /// </summary>
    public partial class ClientDetailsWindow : Window
    {
        private readonly int _clientId;
        private readonly IClientBusinessComponent _clientBusinessComponent;
        public ClientDetailsWindow(int clientId, decimal salary, IClientBusinessComponent clientBusinessComponent)
        {
            _clientId = clientId;
            _clientBusinessComponent = clientBusinessComponent;

            InitializeComponent();
            LastNameTextBox.IsEnabled = false;
            NameTextBox.IsEnabled = false;
            PatronymicTextBox.IsEnabled = false;
            BirthdayField.IsEnabled = false;
            MobilePhoneNumberTextBox.IsEnabled = false;
            EmailTextBox.IsEnabled = false;
            PassportNoTextBox.IsEnabled = false;
            PasswordExpirationField.IsEnabled = false;
            PassportIdentityNoTextBox.IsEnabled = false;
            PassportAuthorityTextBox.IsEnabled = false;
            PlaceOfResidenceTextBox.IsEnabled = false;
            RegistrationAddressTextBox.IsEnabled = false;
            SalaryTextBox.IsEnabled = false;

            Init(salary);
        }

        private void Init(decimal salary)
        {
            var client = _clientBusinessComponent.GetByID(_clientId);

            LastNameTextBox.Text = client.LastName;
            NameTextBox.Text = client.Name;
            PatronymicTextBox.Text = client.Patronymic;
            BirthdayField.Text = client.Birthday.ToString();
            MobilePhoneNumberTextBox.Text = client.Mobile;
            EmailTextBox.Text = client.Email;
            PassportNoTextBox.Text = client.PassportNo;
            PasswordExpirationField.Text = client.PassportExpirationDate.ToString();
            PassportIdentityNoTextBox.Text = client.PassportIdentificationNo;
            PassportAuthorityTextBox.Text = client.PassportAuthority;
            PlaceOfResidenceTextBox.Text = client.PlaceOfResidence;
            RegistrationAddressTextBox.Text = client.RegistrationAddress;
            SalaryTextBox.Text = salary.ToString()+"$";
        }
    }
}
