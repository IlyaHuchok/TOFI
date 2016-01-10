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

using BankPresentation.Validation;

using Entities;

namespace BankPresentation
{
    /// <summary>
    /// Interaction logic for CreditTypeWindow.xaml
    /// </summary>
    public partial class CreditTypeWindow : Window
    {
        private ICreditTypeBusinessComponent _creditTypeBc;

        public CreditTypeWindow(ICreditTypeBusinessComponent creditTypeBc)
        {
            this._creditTypeBc = creditTypeBc;
            InitializeComponent();

            CurrencyComboBox.Items.Add("USD");
            CurrencyComboBox.SelectedValue = "USD";
            CurrencyComboBox.IsEnabled = false;
        }

        private void CreditTypeAddButton_Click(object sender, RoutedEventArgs e)
        {
            var validationResult = CreditTypeValidation.Validate(
                NameField.Text,
                TimeMonthField.Text,
                PercentPerYearField.Text,
                (string)CurrencyComboBox.SelectedValue,
                FinePercentField.Text,
                MinAmountField.Text,
                MaxAmountField.Text);
            if (validationResult.IsValid)
            {
                var creditType = new CreditType()
                {
                    Name = NameField.Text,
                    TimeMonths = int.Parse(TimeMonthField.Text),
                    PercentPerYear = decimal.Parse(PercentPerYearField.Text),
                    Currency = "USD", //
                    FinePercent = decimal.Parse(FinePercentField.Text),
                    MinAmount = decimal.Parse(MinAmountField.Text),
                    MaxAmount = decimal.Parse(MaxAmountField.Text)
                };

                _creditTypeBc.Add(creditType);

                MessageBox.Show("Added successfully");
                this.Close();
            }
            else
            {
                MessageBox.Show(validationResult.Error);
            }
        }
    }
}
