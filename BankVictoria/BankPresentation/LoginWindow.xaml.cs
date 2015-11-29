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

using Entities.Enums;

using Ninject;
using Ninject.Modules;
using Ninject.Parameters;

namespace BankPresentation
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class LoginWindow : Window
  {
    private readonly IKernel _ninjectKernel;
    // ADD YOUR COMPONENTS HERE

    public LoginWindow(IKernel ninjectKernel/*ENTER NEEDED BUSINESS COMPONENTS HERE*/)
    {
      _ninjectKernel = ninjectKernel;

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

    private void Login(string username, string password)
    {
      //afdasdfadsf
      // BC activities

      this.SetPage(UserRole.Client);//stub
    }

    private void SetPage(UserRole role)
    {
        this.Content = _ninjectKernel.Get<MainPage>(new ConstructorArgument("userRole", role));

        //var newWindow = _ninjectKernel.Get<RegistrationWindow>(new ConstructorArgument("userRole", role));
        //newWindow.ShowDialog();      
    }
  }
}
