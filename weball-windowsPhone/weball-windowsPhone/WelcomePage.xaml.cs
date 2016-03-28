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
    public partial class WelcomePage : PhoneApplicationPage
    {
        public WelcomePage()
        {
            InitializeComponent();
        }

        private void mainpageButton(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/HubPage.xaml", UriKind.Relative));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/FivePage.xaml", UriKind.Relative));
        }
    }
}