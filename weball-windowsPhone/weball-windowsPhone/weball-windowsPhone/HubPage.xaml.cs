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
    public partial class HubPage : PhoneApplicationPage
    {
        public HubPage()
        {
            InitializeComponent();
        }

        private void ConnectButton(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/LoginPage.xaml", UriKind.Relative));
        }

        private void RegisterButton(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/RegisterPage.xaml", UriKind.Relative));
        }
    }
}