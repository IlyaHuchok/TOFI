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
    /// Логика взаимодействия для AdministratorWindow.xaml
    /// </summary>
    public partial class AdministratorWindow : Window
    {
        public AdministratorWindow()
        {
            InitializeComponent();
            TimeComboBox.Items.Add("All time");
            TimeComboBox.Items.Add("Year");
            TimeComboBox.Items.Add("Month");
            TimeComboBox.Items.Add("Week");
            TimeComboBox.Items.Add("Day");

            WhereAmountsComboBox.Items.Add(">");
            WhereAmountsComboBox.Items.Add("<");
            WhereAmountsComboBox.Items.Add("=");
        }
    }
}
