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
using BankBL.Interfaces;

namespace BankPresentation
{
    /// <summary>
    /// Логика взаимодействия для OperatorWindow.xaml
    /// </summary>
    public partial class OperatorWindow : Window
    {
        private readonly IClientBusinessComponent _clientBusinessComponent;

        public OperatorWindow(IClientBusinessComponent clientBusinessComponent)
        {
            _clientBusinessComponent = clientBusinessComponent;
            InitializeComponent();            
        }

        private void RepaymentSubmit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Details_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Name: N " + Environment.NewLine +
                            "Birthday B" + Environment.NewLine +
                            "...");
        }

        private void CreditSearch_Click(object sender, RoutedEventArgs e)
        {
            CreditFieldName.IsReadOnly = false;
            
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void FillListBox()
        {            
            for(int i=0;i<_clientBusinessComponent.Count();i++)
            { 
                Client client = _clientBusinessComponent.GetByID(i);
                ClientListBox.Items.Add(client.Name+" "+client.LastName+" "+client.Patronymic);                
            }
            
            TimeListBox.Items.Add("");
            CreditListBox.Items.Add("");
        }

    }
}
