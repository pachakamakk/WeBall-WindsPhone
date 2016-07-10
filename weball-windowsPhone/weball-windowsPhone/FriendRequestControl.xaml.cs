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
    public partial class FriendRequestControl : UserControl
    {
        public FriendRequestControl()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            await WeBallAPI.acceptRequest((string)(((Button)sender).CommandParameter));
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            await WeBallAPI.denyRequest((string)(((Button)sender).CommandParameter));
        }
    }
}
