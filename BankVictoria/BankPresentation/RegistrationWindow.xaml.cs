﻿using System;
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
using BankPresentation.Validation;


namespace BankPresentation
{
    /// <summary>
    /// Interaction logic for RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        private readonly IUserBusinessComponent _userBusinessComponent;
        private readonly IClientBusinessComponent _clientBusinessComponent;
        private readonly UserRole _role;

        public RegistrationWindow(IUserBusinessComponent userBusinessComponent, UserRole userRole,
            IClientBusinessComponent clientBusinessComponent)
        {
            _userBusinessComponent = userBusinessComponent;
            _clientBusinessComponent = clientBusinessComponent;
            _role = userRole;

            InitializeComponent();

            // tab 1 setup
            textBox_Create_Login.MaxLength = UserValidation.UserNameMaxLength;
            passwordBox_Create_Password.MaxLength = UserValidation.PasswordMaxLength;
            passwordBox_Create_Password_Copy.MaxLength = UserValidation.PasswordMaxLength;

            ClientInfoTabItem.IsEnabled = false;
            ClientInfoTabItem.Visibility = Visibility.Hidden; 
            
            // tab 2 setup
            LastNameTextBox.MaxLength = ClientValidation.LastNameMaxLength;
            NameTextBox.MaxLength = ClientValidation.NameMaxLength;
            PatronymicTextBox.MaxLength = ClientValidation.PatronymicMaxLength;
            BirthdayDatePicker.DisplayDateStart = ClientValidation.MinBirthDate;
            BirthdayDatePicker.DisplayDateEnd = ClientValidation.MaxBirthDate;
            MobilePhoneNumberTextBox.MaxLength = ClientValidation.MobileMaxLength;
            PassportNoTextBox.MaxLength = ClientValidation.PassportNoMaxLength;
            PasswordExpirationDatePicker.DisplayDateStart = ClientValidation.MinPassportExpirationDate;
            PasswordExpirationDatePicker.DisplayDateEnd = ClientValidation.MaxPassportExpirationDate;
            PassportIdentityNoTextBox.MaxLength = ClientValidation.PassportIdentityNoMaxLength;
            PassportAuthorityTextBox.MaxLength = ClientValidation.PasswordAuthorityMaxLength;
            PlaceOfResidenceTextBox.MaxLength = ClientValidation.AddressMaxLength;
            RegistrationAddressTextBox.MaxLength = ClientValidation.AddressMaxLength;

            if (_role != UserRole.Admin) // client
            {
                roleComboBox.Visibility = Visibility.Hidden;
                roleComboBox.IsEnabled = false;

                button_End_Tab1.Visibility = Visibility.Hidden;
                button_End_Tab1.IsEnabled = false;
                button_Next.Visibility = Visibility.Visible;
                button_Next.IsEnabled = true;
            }
            else
            {
                roleComboBox.Items.Add("Client");
                if (_role == UserRole.Admin)
                {
                    roleComboBox.Items.Add("Operator");
                    roleComboBox.Items.Add("Security Service Employee");
                    roleComboBox.Items.Add("Admin");
                }
            }
        }

        private void button_Next_Click(object sender, RoutedEventArgs e)
        {
            if (Validate())
            {
                tabControl.SelectedIndex = 1;
                UserInfoTabItem.IsEnabled = false;
                UserInfoTabItem.Visibility = Visibility.Hidden;
                
                ClientInfoTabItem.Visibility = Visibility.Visible;
                ClientInfoTabItem.IsEnabled = true;
            }
        }

        private void button_End_Tab1_Click(object sender, RoutedEventArgs e)
        {
            if (Validate())
            {
                UserRole selectedRole;
                switch ((string)roleComboBox.SelectedValue)
                {
                    case "Client":
                        selectedRole = UserRole.Client;
                        break;
                    case "Admin":
                        selectedRole = UserRole.Admin;
                        break;
                    case "Operator":
                        selectedRole = UserRole.Operator;
                        break;
                    case "Security Service Employee":
                        selectedRole = UserRole.SecurityServiceEmployee;
                        break;
                    default:
                        selectedRole = UserRole.Client;
                        break;
                }
                //register user if it's not a client
                _userBusinessComponent.Add(textBox_Create_Login.Text,
                passwordBox_Create_Password.Password,
                selectedRole);
                MessageBox.Show("Registered successfully!");
            }
        }
        private bool Validate()
        {
            var validationResult = UserValidation.Validate(
                textBox_Create_Login.Text,
                passwordBox_Create_Password.Password,
                passwordBox_Create_Password_Copy.Password);
            if (validationResult.IsValid)
            {
                if (_userBusinessComponent.Exists(textBox_Create_Login.Text, passwordBox_Create_Password.Password))
                {
                    MessageBox.Show("A user with such username already exists!");
                    return false;
                }
                return true;
            }
            else
            {
                MessageBox.Show(validationResult.Error);
                return false;
            }
        }

        private void button_Back_Click(object sender, RoutedEventArgs e)
        {
            ClientInfoTabItem.IsEnabled = false;
            ClientInfoTabItem.Visibility = Visibility.Hidden;

            tabControl.SelectedIndex = 0;
            //#s#
            UserInfoTabItem.Visibility = Visibility.Visible;
            UserInfoTabItem.IsEnabled = true;
        }

        private void button_End_Click(object sender, RoutedEventArgs e)
        {
            string error = string.Empty;
            if (BirthdayDatePicker.SelectedDate == null)
            {
                error += "Please, select your birth date.\n";
            }
            if (PasswordExpirationDatePicker.SelectedDate == null)
            {
                error += "Please, select your password expiration date.";
            }

            if (string.IsNullOrEmpty(error))
            {
                var validationResult = ClientValidation.Validate(
                    LastNameTextBox.Text,
                    NameTextBox.Text,
                    PatronymicTextBox.Text,
                    BirthdayDatePicker.SelectedDate.Value,
                    MobilePhoneNumberTextBox.Text,
                    EmailTextBox.Text,
                    PassportNoTextBox.Text,
                    PasswordExpirationDatePicker.SelectedDate.Value,
                    PassportIdentityNoTextBox.Text,
                    PassportAuthorityTextBox.Text,
                    PlaceOfResidenceTextBox.Text,
                    RegistrationAddressTextBox.Text);
                if (validationResult.IsValid)
                {
                    UserRole selectedRole;
                    switch ((string)roleComboBox.SelectedValue)
                    {
                        case "Client":
                            selectedRole = UserRole.Client;
                            break;
                        case "Admin":
                            selectedRole = UserRole.Admin;
                            break;
                        case "Operator":
                            selectedRole = UserRole.Operator;
                            break;
                        case "Security Service Employee":
                            selectedRole = UserRole.SecurityServiceEmployee;
                            break;
                        default:
                            selectedRole = UserRole.Client;
                            break;
                    }
                    _clientBusinessComponent.Add(
                        textBox_Create_Login.Text,
                        passwordBox_Create_Password.Password,
                        selectedRole,
                        LastNameTextBox.Text,
                        NameTextBox.Text,
                        PatronymicTextBox.Text,
                        BirthdayDatePicker.SelectedDate.Value,
                        MobilePhoneNumberTextBox.Text,
                        EmailTextBox.Text,
                        PassportNoTextBox.Text,
                        PasswordExpirationDatePicker.SelectedDate.Value,
                        PassportIdentityNoTextBox.Text,
                        PassportAuthorityTextBox.Text,
                        PlaceOfResidenceTextBox.Text,
                        RegistrationAddressTextBox.Text
                        );
                    MessageBox.Show("Registered successfully!");

                    //go back
                    ClientInfoTabItem.IsEnabled = false;
                    ClientInfoTabItem.Visibility = Visibility.Hidden;

                    tabControl.SelectedIndex = 0;
                    UserInfoTabItem.Visibility = Visibility.Visible;
                    UserInfoTabItem.IsEnabled = true;
                }
                else
                {
                    MessageBox.Show(validationResult.Error);
                }
            }
            else
            {
                MessageBox.Show(error);
            }
        }

        private void roleComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedItem = (sender as ComboBox).SelectedItem as string;

            if (selectedItem == "Client")
            {
                button_Next.Visibility = Visibility.Visible;
                button_Next.IsEnabled = true;
                button_End_Tab1.Visibility = Visibility.Hidden;
                button_End_Tab1.IsEnabled = false;
            }
            else
            {
                button_Next.Visibility = Visibility.Hidden;
                button_Next.IsEnabled = false;
                button_End_Tab1.Visibility = Visibility.Visible;
                button_End_Tab1.IsEnabled = true;
            }
        }
    }
}
