using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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

using Ninject;

namespace BankPresentation
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        private static bool mWinVisible = false;

        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();
        private static IntPtr mThisConsole = GetConsoleWindow();

        private readonly IKernel _ninjectKernel;

        public LoginWindow(IKernel ninjectKernel)
        {
            this._ninjectKernel = ninjectKernel;
            ShowWindow(mThisConsole, 0);

            InitializeComponent();

            this.Content = _ninjectKernel.Get<LoginPage>();
        }
    }
}
