using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace weball_windowsPhone
{
    public partial class ForgotPasswordPage : PhoneApplicationPage
    {
        public ForgotPasswordPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Ceci va vous envoyer un mail. Continuer?", "Oubli", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {

            }
        }
    }
}