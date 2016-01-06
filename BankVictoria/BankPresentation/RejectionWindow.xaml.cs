using System.Windows;

namespace BankPresentation
{
    /// <summary>
    /// Interaction logic for RejectionWindow.xaml
    /// </summary>
    public partial class RejectionWindow
    {
        public MessageBoxResult Result { get; set; }

        public string RejectionReason { get; set; }

        public RejectionWindow()
        {
            InitializeComponent();
        }

        private void Reject_Click(object sender, RoutedEventArgs e)
        {
            Result = MessageBoxResult.Yes;
            RejectionReason = RejectionReasonTextbox.Text;
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Result = MessageBoxResult.No;
            this.Close();
        }

        public MessageBoxResult ShowDialog(out string rejectionReason)
        {
            ShowDialog();

            rejectionReason = RejectionReason;
            return Result;
        }
    }
}
