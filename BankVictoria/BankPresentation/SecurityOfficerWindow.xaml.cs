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

using BankBL.Interfaces;

using Ninject;

namespace BankPresentation
{
    /// <summary>
    /// Логика взаимодействия для SecurityOfficerWindow.xaml
    /// </summary>
    public partial class SecurityOfficerWindow : Window
    {
        private readonly ISecurityOfficerBusinessComponent _securityOfficerBusinessComponent;
        private readonly IKernel _ninjectKernel;

        private const int RecordsPerPage = 1000;

        private int loadedRecordsCount = 0;

        public SecurityOfficerWindow(IKernel ninjectKernel, ISecurityOfficerBusinessComponent securityOfficerBusinessComponent)
        {
            this._ninjectKernel = ninjectKernel;
            this._securityOfficerBusinessComponent = securityOfficerBusinessComponent;

            InitializeComponent();

            ButtonNextRequests.IsEnabled = false;
            ButtonPreviousRequests.IsEnabled = false;

            RequestsDataGrid.SelectionMode = DataGridSelectionMode.Single;
            RequestsDataGrid.ItemsSource = _securityOfficerBusinessComponent.GetAllRequests();
        }

        private void button_Back_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button_Next_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
