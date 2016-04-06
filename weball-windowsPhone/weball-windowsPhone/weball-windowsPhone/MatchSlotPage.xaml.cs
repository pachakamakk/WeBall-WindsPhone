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
    public class Timing
    {
        public int timing { get; set; }
        public List<Match> matches { get; set; }
    }
    public partial class MatchSlotPage : PhoneApplicationPage
    {
        public Five five = null;
        public List<Match> matchs = null;
        public DateTime date;
        public MatchSlotPage()
        {
            InitializeComponent();
        }
        private void getMatchList(string parameter)
        {
            matchs = JsonConvert.DeserializeObject<List<Match>>(parameter);
            List<Timing> timings = null;
            if (matchs != null)
            {
                five = WeBallAPI.FiveList.Where(s => s._id == matchs[0].five).ToList()[0];
                GridFiveImage.DataContext = five;
                timings = new List<Timing>();
                Timing timing = null;
                int lastHour = 0;
                foreach (Match match in matchs)
                {
                    if (match.startDate.Hour > lastHour)
                    {
                        timing = new Timing();
                        timings.Add(timing);
                        timing.matches = new List<Match>();
                        timing.matches.Add(match);
                        timing.timing = match.startDate.Hour;
                        lastHour = match.startDate.Hour;
                    }
                    else if (match.startDate.Hour == lastHour)
                        timing.matches.Add(match);
                }
            }
            ListTimings.DataContext = timings;
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string parameter = string.Empty;
            if (NavigationContext.QueryString.TryGetValue("matchs", out parameter))
                getMatchList(parameter);
            else if (NavigationContext.QueryString.TryGetValue("five", out parameter))
            {
                five = WeBallAPI.FiveList.Where(s => s._id == (string)parameter).ToList()[0];
                GridFiveImage.DataContext = five;
            }
            if (NavigationContext.QueryString.TryGetValue("date", out parameter))
            {
                date = JsonConvert.DeserializeObject<DateTime>(parameter);
                if (matchs == null && five != null)
                {
                    matchs = five.matchs.Where(d => (d.startDate.Day == date.Day) &&
                            (d.startDate.Month == date.Month) &&
                            (d.startDate.Year == date.Year)).ToList();
                }
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/" + ((Button)sender).Name.Substring(6) + "Page.xaml", UriKind.Relative));
        }

        private void ListMatch_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
        }

        private void createMatch(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Date: " + date);
            NavigationService.Navigate(new Uri("/CreateMatchPage.xaml?five=" + five._id + "&date=" + JsonConvert.SerializeObject(date), UriKind.Relative));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MatchTimingPage.xaml?fiveId=" + five._id, UriKind.Relative));
        }

    }
}