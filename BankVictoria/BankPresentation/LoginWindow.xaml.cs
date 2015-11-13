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

using BankPresentation.Interfaces;

using Ninject.Modules;

namespace BankPresentation
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class LoginWindow : Window, ILoginWindow
  {
    private readonly INinjectModule _ninjectModule;
    // ADD YOUR COMPONENTS HERE

    public LoginWindow(INinjectModule ninjectModule/*ENTER NEEDED BUSINESS COMPONENTS HERE*/)
    {
      _ninjectModule = ninjectModule;

      InitializeComponent();
    }
  }
}
