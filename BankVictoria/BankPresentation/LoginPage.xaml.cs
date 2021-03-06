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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;

using Entities.Enums;

using Ninject;
using Ninject.Modules;
using Ninject.Parameters;

using BankBL.Interfaces;

using Entities;
using System.Runtime.InteropServices;

namespace BankPresentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        private readonly IKernel _ninjectKernel;
        private readonly IUserBusinessComponent _userBusinessComponent;
        // ADD YOUR COMPONENTS HERE

        public LoginPage(IKernel ninjectKernel/*ENTER NEEDED BUSINESS COMPONENTS HERE*/,
            IUserBusinessComponent userBusinessComponent)
        {

            _ninjectKernel = ninjectKernel;
            _userBusinessComponent = userBusinessComponent;

            InitializeComponent();
        }

        private void Passwordbox_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Login(LoginTextbox.Text, Passwordbox.Password);
            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            Login(LoginTextbox.Text, Passwordbox.Password);
        }

#if DEBUG
        private const string errorMessage = "Wrong username/password combination!\nEXISTING COMBINATIONS:admin/admin \n security/security\n operator1/operator1\n client1/client1";
#else
    private const string errorMessage = "Wrong username/password combination!";
    private int _failedLoginAttempts;
#endif

        private void Login(string username, string password)
        {
            var loginResult = _userBusinessComponent.Login(username, password);
            if (loginResult != null)
            {
                if (loginResult == UserRole.Operator || loginResult == UserRole.SecurityServiceEmployee)
                {
                    if (_userBusinessComponent.IsActive(username, password).Value)
                    {
                        this.SetPage(loginResult.Value, username);
                    }
                    else
                    {
                        MessageBox.Show("Your account has been disabled, please, contact your administrator.");
                    }
                }
                else
                {
                    this.SetPage(loginResult.Value, username);
                }
            }
            else
            {
#if RELEASE
          _failedLoginAttempts++;
          if (_failedLoginAttempts >=5)
          {
              //lock for 30 seconds 
              Thread.Sleep(30*1000);
          }
#endif
                MessageBox.Show(errorMessage);
            }
        }

        private void SetPage(UserRole role, string username)
        {
            var userId = _userBusinessComponent.GetIdByLogin(username);
            var window = Window.GetWindow(this);
            window.SizeToContent = SizeToContent.WidthAndHeight;
            window.Title = "Bank Victoria";

            switch (role)
            {
                case UserRole.Admin:
                    window.Content = _ninjectKernel.Get<AdministratorPage>(
                        new ConstructorArgument("clientToUpdate", (Client)null));
                    break;
                case UserRole.Client:
                    window.Content = _ninjectKernel.Get<ClientWindow>(new ConstructorArgument("userId", userId));//_ninjectKernel.Get<ClientWindow>();
                    break;
                case UserRole.Operator:
                    window.Content = _ninjectKernel.Get<OperatorWindow>(new ConstructorArgument("operatorId", userId));//_ninjectKernel.Get<OperatorWindow>();
                    break;
                case UserRole.SecurityServiceEmployee:
                    window.Content = _ninjectKernel.Get<SecurityOfficerPage>(new ConstructorArgument("userId", userId));
                    break;
            }
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            var registrationWindow = _ninjectKernel.Get<RegistrationWindow>(
                new ConstructorArgument("userRole", UserRole.Client),
                new ConstructorArgument("clientToUpdate", (Client)null));
            registrationWindow.ShowDialog();
        }
    }
}
