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
    public partial class ParamsProfileControl : UserControl
    {
        public ParamsProfileControl()
        {
            InitializeComponent();
            BoxName.Text = WeBallAPI.currentUser.fullName;
            BoxEmail.Text = WeBallAPI.currentUser.email;
            BoxBirthday.Value = DateTime.ParseExact(WeBallAPI.currentUser.birthday, "MM/dd/yyyy HH:mm:ss",
                                       System.Globalization.CultureInfo.InvariantCulture);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/ChangePasswordPage.xaml", UriKind.Relative));
        }

        private void BoxName_LostFocus(object sender, RoutedEventArgs e)
        {
            if ((((TextBox)sender)).Text.Length < 2)
                ((TextBox)sender).Text = WeBallAPI.currentUser.fullName;
        }

        private void BoxEmail_LostFocus(object sender, RoutedEventArgs e)
        {
            if ((((TextBox)sender)).Text.Length < 2)
                ((TextBox)sender).Text = WeBallAPI.currentUser.email;
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (BoxName.Text != "")
                WeBallAPI.currentUser.fullName = BoxName.Text;
            if (BoxEmail.Text != "")
                WeBallAPI.currentUser.email = BoxEmail.Text;
            char[] delimiters = { '/', ' ' };
            string[] parsedBirthday = BoxBirthday.Value.ToString().Split(delimiters);
            string birthday = parsedBirthday[2] + ',' + parsedBirthday[1] + ',' + parsedBirthday[0];
            WeBallAPI.currentUser.birthday = birthday;
            await WeBallAPI.updateUser();
            MessageBoxResult result =
                MessageBox.Show("Profil édité!",
                    "Confirmation",
            MessageBoxButton.OK);
            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/ProfilePage.xaml", UriKind.Relative));
        }   
    }
}
