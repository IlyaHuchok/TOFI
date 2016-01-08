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
using System.Collections.ObjectModel;

using BankBL.Interfaces;

using Entities;
using Entities.Enums;

using Ninject;
using Ninject.Parameters;

namespace BankPresentation
{
    /// <summary>
    /// Interaction logic for CreditTypesWindow.xaml
    /// </summary>
    public partial class CreditTypesWindow : Window
    {
        private readonly ICreditTypeBusinessComponent _creditTypeBusinessComponent;
        private readonly UserRole _role;

        private ObservableCollection<CreditType> _creditTypesList;

        public ObservableCollection<CreditType> CreditTypesList
        {
            get
            {
                return _creditTypesList;
            }
        }

        public CreditTypesWindow(
            UserRole role,
            ICreditTypeBusinessComponent creditTypeBusinessComponent,
            int? selectedCreditTypeId = null)
        {
            _creditTypeBusinessComponent = creditTypeBusinessComponent;
            _role = role;

            InitializeComponent();
            RefreshPage();

            if (selectedCreditTypeId != null)
            {
                CreditTypesListView.SelectedIndex = _creditTypesList.IndexOf(_creditTypesList.First(x => x.CreditTypeId == selectedCreditTypeId));
            }
        }

        private void RefreshPage()
        {
            _creditTypesList = new ObservableCollection<CreditType>(this._creditTypeBusinessComponent.GetAllCreditTypes());
            CreditTypesListView.ItemsSource = _creditTypesList;
        }
    }
}
