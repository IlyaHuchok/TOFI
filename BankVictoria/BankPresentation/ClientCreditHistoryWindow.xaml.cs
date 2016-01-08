using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

using BankBL.Interfaces;

using Entities;
using Entities.Enums;

using Ninject;
using Ninject.Modules;
using Ninject.Parameters;

namespace BankPresentation
{
    /// <summary>
    /// Interaction logic for ClientCreditHistoryWindow.xaml
    /// </summary>
    public partial class ClientCreditHistoryWindow : Window
    {
        private ObservableCollection<Credit> _creditList;
        private readonly ICreditBusinessComponent _creditBusinessComponent;
        private readonly IKernel _ninjectKernel;
        private readonly int clientId;

        public ObservableCollection<Credit> RequestList
        {
            get
            {
                return _creditList;
            }
        }

        public ClientCreditHistoryWindow(ICreditBusinessComponent creditBusinessComponent, IKernel ninjectKernel, int clientId)
        {
            this._creditBusinessComponent = creditBusinessComponent;
            this._ninjectKernel = ninjectKernel;
            this.clientId = clientId;

            InitializeComponent();

            this.RefreshPage();
        }

        private void RefreshPage()
        {
            _creditList = new ObservableCollection<Credit>(_creditBusinessComponent.GetClientCredits(clientId).OrderBy(x=>x.StartDate));
            ListViewCreditHistory.ItemsSource = _creditList;
        }

        private void CreditTypeHyperLinkClick(object sender, RoutedEventArgs e)
        {
            var hyperlink = sender as Hyperlink;
            var creditTypeId = (int)(hyperlink.CommandParameter);

            var creditTypesWindow = _ninjectKernel.Get<CreditTypesWindow>(
                new ConstructorArgument("role", UserRole.Operator),
                new ConstructorArgument("selectedCreditTypeId", creditTypeId));
            creditTypesWindow.ShowDialog();
        }
    }

    //public class CreditHistoryItem
    //{
    //    public Credit CreditInfo { get; set; }
    //    public DateTime EndDate { get; set; }
    //    public DateTime EndDate { get; set; }
    //}
}
