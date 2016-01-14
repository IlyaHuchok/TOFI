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
using BankPresentation.ListViewClasses;
using System.Collections.ObjectModel;
using BankBL.Interfaces;
using Entities;
using BankPresentation.Validation;
namespace BankPresentation
{
    /// <summary>
    /// Логика взаимодействия для ClientWindow.xaml
    /// </summary>
    public partial class ClientWindow : Page
    {
        private ObservableCollection<CTypeListView> CTypetDataList = new ObservableCollection<CTypeListView>();
        private ObservableCollection<ClientListView> RequestDataList = new ObservableCollection<ClientListView>();
        private ObservableCollection<MCreditListView> MyCreditDataList = new ObservableCollection<MCreditListView>();
        private readonly ICreditTypeBusinessComponent _creditTypeBusinessComponent;
        private readonly ICreditBusinessComponent _creditBusinessComponent;
        private readonly IClientBusinessComponent _clientBusinessComponent;
        private readonly IRequestBusinessComponent _requestBusinessComponent;
        int _userId;
        public ClientWindow(ICreditBusinessComponent creditBusinessComponent, ICreditTypeBusinessComponent creditTypeBusinessComponent, IClientBusinessComponent clientBusinessComponent,
            IRequestBusinessComponent requestBusinessComponent, int userId)
        {
            

            _creditTypeBusinessComponent = creditTypeBusinessComponent;
            _creditBusinessComponent = creditBusinessComponent;
            _clientBusinessComponent = clientBusinessComponent;
            _requestBusinessComponent = requestBusinessComponent;
            _userId = userId;

            InitializeComponent();

            CreditSalary.MaxLength = RequestValidation.SalaryMaxLength;
            CreditAmount.MaxLength = RequestValidation.AmountMaxLength;

            FillCTypeListView();
            FillRequestListView();
            FillMyCreditsListView();

            IList<CreditType> ctype = _creditTypeBusinessComponent.GetAllActiveCreditTypes().ToList();
            foreach(var ct in ctype)
                CreditCTypeBox.Items.Add(ct.Name);
            CreditCTypeBox.SelectedIndex = 0;

            CTypeListView.ItemsSource = CTypetDataList;
            RequestListView.ItemsSource = RequestDataList;
            MyCreditListView.ItemsSource = MyCreditDataList;
        }

        private void SendRequest_Click(object sender, RoutedEventArgs e) 
        {            
            CreditType ct = _creditTypeBusinessComponent.GetAllActiveCreditTypes().Where(x => x.Name == CreditCTypeBox.SelectedValue.ToString()).FirstOrDefault();
            bool MoreThanMAX = ct.MaxAmount < Convert.ToUInt32(CreditAmount.Text);
            bool LessThanMIN = ct.MinAmount > Convert.ToUInt32(CreditAmount.Text);
            if (Validate() && !MoreThanMAX && !LessThanMIN)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure?", "Accept Confirmation", MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    int _clientId = _clientBusinessComponent.GetAll().Where(x => x.UserId == _userId).FirstOrDefault().ClientId;
                    CreditType ctype = _creditTypeBusinessComponent.GetAllActiveCreditTypes().Where(x => x.Name == CreditCTypeBox.SelectedValue.ToString()).FirstOrDefault();
                    _requestBusinessComponent.Add(_clientId, null, null, ctype.CreditTypeId, Entities.Enums.RequestStatus.Created,
                                                  Convert.ToDecimal(CreditAmount.Text), Convert.ToDecimal(CreditSalary.Text), "");
                }
                ClearRequestListView();
                FillRequestListView();
            }
            string error = "";
            if (LessThanMIN)
                error += "Amount shold be more than " + ct.MinAmount + Environment.NewLine;
            if (MoreThanMAX)
                error += "Amount shold be less than " + ct.MaxAmount + Environment.NewLine;
            if (error != "")
                MessageBox.Show(error);
            
        }

        public void FillCTypeListView()
        {
            IList<CreditType> ctype = _creditTypeBusinessComponent.GetAllActiveCreditTypes().ToList();
            foreach (var ct in ctype)
            {
                CTypetDataList.Add(new CTypeListView() { CTypeName = ct.Name});
            }
        }

        public void FillRequestListView()
        {
            //IList<Credit> credit = _creditBusinessComponent.GetAll();....
            IList<Request> request = _requestBusinessComponent.GetAll().Where(x=> x.Client.UserId == _userId).ToList();
            foreach (var req in request)
            {
                RequestDataList.Add(new ClientListView() { RequestId = req.RequestId, CType = req.CreditType.Name, Amount = req.AmountOfCredit.ToString(), Status = req.Status.ToString() });
                //CreditDataList.Add(new ClientListView() {RequestId = cr.RequestId, CType = cr.CreditType.Name, Amount = cr.PaidForFine.ToString(), Status = cr.Request.Status.ToString()  });
            }
        }
        public void FillMyCreditsListView()
        {
            IList<Credit> credit = _creditBusinessComponent.GetAll().Where(x=> x.Request.Client.UserId == _userId).ToList();
            foreach(var cre in credit)
            {
                MyCreditDataList.Add(new MCreditListView() { CreditType = cre.CreditType.Name, AllreadyPaid = cre.AllreadyPaid.ToString(),
                    AmountOfPaymentPerMonth = cre.AmountOfPaymentPerMonth.ToString(), StartDate = cre.StartDate.ToString("d"), PaidForFine = cre.PaidForFine.ToString(),
                    CountFineFromThisDate = cre.CountFineFromThisDate.Date.ToString("d") });
            }
        }

        public void ClearCTypeListView()
        {
            CTypetDataList.Clear();
        }
        public void ClearRequestListView()
        {
            RequestDataList.Clear();
        }
        public void ClearMyCreditListView()
        {
            MyCreditDataList.Clear();
        }
        private void tabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is TabControl)
            {
                if(TabCType.IsSelected)
                {
                    ClearCTypeListView();
                    ClearRequestListView();
                    FillCTypeListView();
                }
                if(TabCredit.IsSelected)
                {
                    ClearRequestListView();
                    ClearCTypeListView();
                    FillRequestListView();
                }
                if(TabCredit.IsSelected)
                {
                    ClearMyCreditListView();
                    FillMyCreditsListView();
                }
            }
        }

        private void CTypeListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try {
                CTypeListView clv = (CTypeListView)CTypeListView.SelectedValue;
                CreditType ctype = _creditTypeBusinessComponent.GetAllCreditTypes().Where(x => x.Name == clv.CTypeName).FirstOrDefault();
                CTypeName.Text = ctype.Name;
                CTypeTimeMonths.Text = ctype.TimeMonths.ToString();
                CTypePercentPerYear.Text = ctype.PercentPerYear.ToString();
                CTypeCurrency.Text = ctype.Currency;
                CTypeFinePercent.Text = ctype.FinePercent.ToString();
                CTypeMinAmount.Text = ctype.MinAmount.ToString();
                CTypeMaxAmount.Text = ctype.MaxAmount.ToString();
                CTypeIsAvailable.Text = ctype.IsAvailable.ToString();
            }
            catch  { }

        }

        private void RequestUpdate_Click(object sender, RoutedEventArgs e)
        {
            ClearRequestListView();
            FillRequestListView();
        }

        private bool Validate()
        {
            var validationResult = RequestValidation.Validate(CreditAmount.Text, CreditSalary.Text);
            if (validationResult.IsValid)
            {
                return true;
            }
            else
            {
                MessageBox.Show(validationResult.Error);
                return false;
            }
        }
    }

    public class CTypeListView
    {
        public string CTypeName { get; set; }
    }
    public class MCreditListView
    {
        public string CreditType { get; set; }
        public string AllreadyPaid { get; set; }
        public string AmountOfPaymentPerMonth { get; set; }
        public string StartDate { get; set; }
        public string PaidForFine { get; set; }
        public string CountFineFromThisDate { get; set; }
    }


}
