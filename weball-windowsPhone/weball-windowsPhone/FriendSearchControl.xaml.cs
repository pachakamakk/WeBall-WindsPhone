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
    public partial class FriendSearchControl : UserControl
    {
        public FriendSearchControl()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if ((string)(((Button)sender).CommandParameter) != "")
                (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/ProfilePage.xaml?user=" + (string)(((Button)sender).CommandParameter), UriKind.Relative));
        }
    }
}
