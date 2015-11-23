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


namespace BankPresentation
{
    /// <summary>
    /// Interaction logic for RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow()
        {
            InitializeComponent();
            comboBox.Items.Add("Client");  
            comboBox.Items.Add("Operator"); 
            comboBox.Items.Add("SecurityServiceEmployee"); 
            comboBox.Items.Add("Admin");
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
