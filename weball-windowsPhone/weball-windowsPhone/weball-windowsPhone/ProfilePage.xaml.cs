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
            UserPrompt.Text = WeBallAPI.CurrentUser.username;
            EmailPrompt.Text = WeBallAPI.CurrentUser.email;
            FirstNamePrompt.Text = WeBallAPI.CurrentUser.firstName;
            LastNamePrompt.Text = WeBallAPI.CurrentUser.lastName;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/"+ ((Button)sender).Name.Substring(6) + "Page.xaml", UriKind.Relative));
        }
    }
}