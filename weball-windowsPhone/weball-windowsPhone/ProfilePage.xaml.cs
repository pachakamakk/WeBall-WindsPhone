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
    public partial class Page1 : PhoneApplicationPage
    {
        public Page1()
        {
            InitializeComponent();
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            FirstNamePrompt.Text = WeBallAPI.currentUser.fullName;
            profileStack.DataContext = WeBallAPI.currentUser;
            if (WeBallAPI.currentUser != null)
                System.Diagnostics.Debug.WriteLine("Total " + WeBallAPI.currentUser._nMatches.total); ;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/"+ ((Button)sender).Name.Substring(6) + "Page.xaml", UriKind.Relative));
        }
    }
}