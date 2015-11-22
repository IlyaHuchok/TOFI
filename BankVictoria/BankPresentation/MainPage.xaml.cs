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

using Entities.Enums;

using Ninject;
using Ninject.Modules;

namespace BankPresentation
{
  /// <summary>
  /// Interaction logic for MainPage.xaml
  /// </summary>
  public partial class MainPage : Page, IMainPage
  {
    private const string windowTitle = "Main";
    private readonly UserRole _userRole;
    private readonly IKernel _ninjectKernel;
    // ADD YOUR COMPONENTS HERE

    public MainPage(IKernel ninjectKernel, UserRole userRole /*ENTER NEEDED BUSINESS COMPONENTS HERE*/)
    {
      _ninjectKernel = ninjectKernel;
      _userRole = userRole;

      InitializeComponent();
      this.WindowTitle = windowTitle;
    }
  }
}
