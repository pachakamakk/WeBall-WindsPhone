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
    public partial class NotificationsPage : PhoneApplicationPage
    {
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            await WeBallAPI.getNotifications();
            if (WeBallAPI.Success == false)
                return;
            ListNotifs.ItemsSource = WeBallAPI.notifs.notifications;
            ListFriendsRequests.ItemsSource = WeBallAPI.notifs.requests;
        }
        public NotificationsPage()
        {
            InitializeComponent();
        }
    }
}