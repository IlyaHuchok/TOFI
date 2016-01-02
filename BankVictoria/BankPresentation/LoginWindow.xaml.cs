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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;

using Entities.Enums;

using Ninject;
using Ninject.Modules;
using Ninject.Parameters;

using BankBL.Interfaces;

namespace BankPresentation
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class LoginWindow : Window
  {
    private readonly IKernel _ninjectKernel;
    private readonly IUserBusinessComponent _userBusinessComponent;
    // ADD YOUR COMPONENTS HERE

    public LoginWindow(IKernel ninjectKernel/*ENTER NEEDED BUSINESS COMPONENTS HERE*/,
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
    private const string errorMessage = "Wrong username/password combination!\nEXISTING COMBINATIONS:admin/admin \n security/security\n operator/operator\n client1/client1";
#else
    private const string errorMessage = "Wrong username/password combination!";
    private int _failedLoginAttempts;
#endif

    private void Login(string username, string password)
    {
      var loginResult = _userBusinessComponent.Login(username, password);
      if (loginResult != null)
      {
          this.SetPage(loginResult.Value);
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

    private void SetPage(UserRole role)
    {
        //var registrationWindow = _ninjectKernel.Get<RegistrationWindow>(new ConstructorArgument("userRole", UserRole.Admin));
        //registrationWindow.ShowDialog(); // UNCOMMENT THIS TO TEST REGISTRATION
        this.Content = _ninjectKernel.Get<MainPage>(new ConstructorArgument("userRole", role));

        //var newWindow = _ninjectKernel.Get<RegistrationWindow>(new ConstructorArgument("userRole", role));
        //newWindow.ShowDialog();      
    }
  }
}
