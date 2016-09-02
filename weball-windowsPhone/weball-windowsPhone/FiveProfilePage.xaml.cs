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
    public partial class FiveProfilePage : PhoneApplicationPage
    {
        private Five target;

        public FiveProfilePage()
        {
            InitializeComponent();
            FiveGrid.DataContext = null;
        }
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            string parameter = string.Empty;
            if (NavigationContext.QueryString.TryGetValue("five", out parameter))
            {
                target = JsonConvert.DeserializeObject<Five>(parameter);
                await WeBallAPI.getMatches(target._id);
                if (WeBallAPI.Success == false)
                    return;
                target = WeBallAPI.FiveList.FirstOrDefault(s => s._id == target._id);
                if (target.matchs != null)
                    target.nTotalMatchs = target.matchs.Count;
                else
                    target.nTotalMatchs = 0;
                FiveGrid.DataContext = target;
                FiveGridBis.DataContext = target;
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/" + ((Button)sender).Name.Substring(6) + "Page.xaml", UriKind.Relative));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (target != null)
            {
                var five = WeBallAPI.FiveList.Where(s => s._id == target._id).ToList();
                NavigationService.Navigate(new Uri("/MatchTimingPage.xaml?five=" + JsonConvert.SerializeObject(five[0]), UriKind.Relative)); 
            }
        }

    }
}