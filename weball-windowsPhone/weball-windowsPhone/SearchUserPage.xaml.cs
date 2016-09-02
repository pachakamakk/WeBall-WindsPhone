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
    public partial class SearchUserPage : PhoneApplicationPage
    {
        public SearchUserPage()
        {
            InitializeComponent();
        }

        private async void Image_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (searchQuery.Text.Length >= 3)
            {
                await WeBallAPI.searchUser(searchQuery.Text);
                if (WeBallAPI.Success == false)
                    return;
                ListSearch.ItemsSource = WeBallAPI.search;
            }
        }
    }
}