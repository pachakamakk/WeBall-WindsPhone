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
    public partial class MatchTimingPage : PhoneApplicationPage
    {
        public Five five = null;
        public MatchTimingPage()
        {
            InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string parameter = string.Empty;
            List<DateTime> dates = new List<DateTime>();
            if (NavigationContext.QueryString.TryGetValue("five", out parameter))
                five = JsonConvert.DeserializeObject<Five>(parameter);
            else if (NavigationContext.QueryString.TryGetValue("fiveId", out parameter))
                five = WeBallAPI.FiveList.Where(s => s._id == (string)parameter).ToList()[0];
            if (five != null && five.matchs != null)
            {
                GridFiveImage.DataContext = five;
                foreach (Match match in five.matchs)
                {
                    dates.Add(match.startDate);
                    System.Diagnostics.Debug.WriteLine("Date: " + match.startDate);
                }
            }
            ((ColorConverter)Resources["ColorConverter"]).Dates = dates;
        }

        private void Calendar_DateClicked(object sender, WPControls.SelectionChangedEventArgs e)
        {
            DateTime date = e.SelectedDate;
            if (five != null && five.matchs != null)
            {
                System.Diagnostics.Debug.WriteLine("Sending: " + date.ToString());
                if (five.matchs.Any(d => (d.startDate.Day == e.SelectedDate.Day) &&
                                                (d.startDate.Month == e.SelectedDate.Month) &&
                                                (d.startDate.Year == e.SelectedDate.Year)))
                {
                    var index = five.matchs.Where(d => (d.startDate.Day == e.SelectedDate.Day) &&
                                                (d.startDate.Month == e.SelectedDate.Month) &&
                                                (d.startDate.Year == e.SelectedDate.Year)).ToList();
                    NavigationService.Navigate(new Uri("/MatchSlotPage.xaml?matchs=" + JsonConvert.SerializeObject(index) + "&date=" + JsonConvert.SerializeObject(date) + "&five=" + five._id, UriKind.Relative));
                }
                else
                    NavigationService.Navigate(new Uri("/MatchSlotPage.xaml?five=" + five._id + "&date=" + JsonConvert.SerializeObject(date), UriKind.Relative));
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/FiveProfilePage.xaml?five=" + JsonConvert.SerializeObject(five), UriKind.Relative)); 
        }
    }
}