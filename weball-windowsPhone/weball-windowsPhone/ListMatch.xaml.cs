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
    public partial class ListMatch : PhoneApplicationPage
    {
        public List<Match> matchs = null;
        public string fiveId;

        public ListMatch()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string parameter = string.Empty;
            if (NavigationContext.QueryString.TryGetValue("matchs", out parameter))
            {
                matchs = JsonConvert.DeserializeObject<List<Match>>(parameter);
                if (matchs != null)
                {
                    ListMatchs.DataContext = matchs;
                }
            }
            if (NavigationContext.QueryString.TryGetValue("five", out parameter))
            {
                fiveId = parameter;
            }

        }

        private void BindingError(object sender, ValidationErrorEventArgs e)
        {
            ((TextBlock)sender).Text = "Unknown";
        }

        private async void ListMatchs_DoubleTap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Match match;

            match = (Match)(((ListBox)sender).SelectedItem);
            if (match != null)
            {
                await WeBallAPI.getMatch(match._id);
                if (WeBallAPI.Success == false)
                    return;
                var five = WeBallAPI.FiveList.FirstOrDefault(s => s._id == fiveId);
                NavigationService.Navigate(new Uri("/ProfileMatchPage.xaml?match=" + JsonConvert.SerializeObject(five.matchs.FirstOrDefault(s => s._id == match._id)) + "&five=" + fiveId, UriKind.Relative));
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MatchSlotPage.xaml?matchs=" + JsonConvert.SerializeObject(matchs) + "&five=" + fiveId, UriKind.Relative));
        }
    }
}