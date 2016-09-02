using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Newtonsoft.Json;

namespace weball_windowsPhone
{
    public partial class FiveListPage : PhoneApplicationPage
    {
        public FiveListPage()
        {
            InitializeComponent();
            loadFiveList();
        }
        public void loadFiveList()
        {
            ListFive.DataContext = WeBallAPI.FiveList;
        }

        private async void ListFive_DoubleTap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            var list = sender as ListBox;
            Five obj = list.SelectedItem as Five;
            if (obj != null)
            {
                await WeBallAPI.getFive(obj._id);
                if (WeBallAPI.Success == false)
                    return;
                var selectedparkdata = WeBallAPI.FiveList.Where(s => s._id == obj._id).ToList();
                NavigationService.Navigate(new Uri("/FiveProfilePage.xaml?five=" + JsonConvert.SerializeObject(selectedparkdata[0]), UriKind.Relative));
            }
        }

    }
}