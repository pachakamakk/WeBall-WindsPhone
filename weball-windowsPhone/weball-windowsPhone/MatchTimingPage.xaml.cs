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
            {
                five = JsonConvert.DeserializeObject<Five>(parameter);
                if (five.matchs != null)
                {
                    GridFiveImage.DataContext = five;
                    foreach (Match match in five.matchs)
                    {
                        dates.Add(match.startDate);
                        System.Diagnostics.Debug.WriteLine("Date: " + match.startDate);
                    }
                }
            }
            ((ColorConverter)Resources["ColorConverter"]).Dates = dates;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/" + ((Button)sender).Name.Substring(6) + "Page.xaml", UriKind.Relative));
        }

        private void Calendar_DateClicked(object sender, WPControls.SelectionChangedEventArgs e)
        {
            DateTime date = e.SelectedDate;
            if (five != null && five.matchs != null)
            {
                System.Diagnostics.Debug.WriteLine("Date: " + e.SelectedDate.ToString());
                if (five.matchs.Any(d => (d.startDate.Day == e.SelectedDate.Day) &&
                                                (d.startDate.Month == e.SelectedDate.Month) &&
                                                (d.startDate.Year == e.SelectedDate.Year)))
                {
                    var index = five.matchs.Where(d => (d.startDate.Day == e.SelectedDate.Day) &&
                                                (d.startDate.Month == e.SelectedDate.Month) &&
                                                (d.startDate.Year == e.SelectedDate.Year)).ToList();
                    NavigationService.Navigate(new Uri("/MatchSlotPage.xaml?matchs=" + JsonConvert.SerializeObject(index), UriKind.Relative));
                }
            }
        }
    }
}