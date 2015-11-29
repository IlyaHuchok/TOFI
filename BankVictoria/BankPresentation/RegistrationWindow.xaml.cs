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
using BankBL.BusinessComponents;


namespace BankPresentation
{
    /// <summary>
    /// Interaction logic for RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        private readonly UserBusinessComponent _userBusinessComponent;
        private readonly UserRole _role;

        public RegistrationWindow(UserBusinessComponent userBusinessComponent, UserRole userRole)
        {
            _userBusinessComponent = userBusinessComponent;
            _role = userRole;

            InitializeComponent();

            if (_role != UserRole.Admin)
            {
                roleComboBox.IsEnabled = false;
            }
            roleComboBox.Items.Add("Client");
            if (_role == UserRole.Admin)
            {
                roleComboBox.Items.Add("Operator");
                roleComboBox.Items.Add("SecurityServiceEmployee");
                roleComboBox.Items.Add("Admin");
            }
        }

        private void button_Next_Click(object sender, RoutedEventArgs e)
        {
            tabControl.SelectedIndex = 1;
        }

        private void button_Back_Click(object sender, RoutedEventArgs e)
        {
            tabControl.SelectedIndex = 0;
        }

        private void button_End_Click(object sender, RoutedEventArgs e)
        {
            //save changes
        }
    }
}
