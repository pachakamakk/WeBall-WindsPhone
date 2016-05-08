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
        public int nbMatch { get; set; }

        public Timing(int timingParam, List<Match> matches = null)
        {
            this.timing = timingParam;
            this.matches = matches;
        }
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
            if (matchs != null)
            {
                five = WeBallAPI.FiveList.Where(s => s._id == matchs[0].five).ToList()[0];
                GridFiveImage.DataContext = five;
            }
        }
        private weball_windowsPhone.Five.Timing getDaySchedule(List<weball_windowsPhone.Five.Timing> prices)
        {
            switch (date.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    return prices.ElementAt(1);
                case DayOfWeek.Tuesday:
                    return prices.ElementAt(2);
                case DayOfWeek.Wednesday:
                    return prices.ElementAt(3);
                case DayOfWeek.Thursday:
                    return prices.ElementAt(4);
                case DayOfWeek.Friday:
                    return prices.ElementAt(5);
                case DayOfWeek.Saturday:
                    return prices.ElementAt(6);
                case DayOfWeek.Sunday:
                    return prices.ElementAt(0);
            }
            return null;
        }

        private void handleTimings()
        {
            List<Timing> timings = createTimings();
            populateTimings(timings);
            ListTimings.DataContext = timings;
        }
        private void addToTiming(List<Timing> timings, int from, int to)
        {
            for (int i = from; i != to; i++)
            {
                if (timings.Any(d => d.timing == i))
                    continue;
                else
                    timings.Add(new Timing(i));
            }
        }
        private List<Timing> createTimings()
        {
            List<Timing> timings = new List<Timing>();
            weball_windowsPhone.Five.Timing timing = null;

            if (five != null)
            {
                timing = getDaySchedule(five.days);
                if (timing.morning != -1)
                    addToTiming(timings, 9, 12);
                if (timing.lunch != -1)
                    addToTiming(timings, 12, 13);
                if (timing.afternoon != -1)
                    addToTiming(timings, 14, 23);
            }
            return timings;
        }
        private void populateTimings(List<Timing> timings)
        {
            Timing timing = null;
            foreach (Match match in matchs)
            {
                if (timings.Any(d => d.timing == match.startDate.Hour))
                {
                    //if ((timing = timings.Where(d => d.timing == match.startDate.Hour).ToList()[0]) != null)
                    if ((timing = timings.First(d => d.timing == match.startDate.Hour)) != null)
                    {
                        var index = timings.IndexOf(timing);
                        if (timings[index].matches == null)
                            timings[index].matches = new List<Match>();
                        timings[index].matches.Add(match);
                        timings[index].nbMatch = timings[index].matches.Count;
                    }
                }
            }
            foreach (Timing buff in timings)
            {
                /*if (buff.matches != null)
                    buff.matches = buff.matches.OrderBy(d => d.startDate).ToList();*/
            }
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
            handleTimings();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/" + ((Button)sender).Name.Substring(6) + "Page.xaml", UriKind.Relative));
        }

        private void ListMatch_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MatchTimingPage.xaml?fiveId=" + five._id, UriKind.Relative));
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            Grid elem = (Grid)sender;
        }
        private void createMatch(object sender, RoutedEventArgs e)
        {
            var fixedDate = new DateTime(date.Year, date.Month, date.Day, (int)(((Button)sender).CommandParameter), 0, 0);
            System.Diagnostics.Debug.WriteLine("Date: " + fixedDate);
            NavigationService.Navigate(new Uri("/CreateMatchPage.xaml?five=" + five._id + "&date=" + JsonConvert.SerializeObject(fixedDate), UriKind.Relative));
        }
        private void JoinMatchButton_Click(object sender, RoutedEventArgs e)
        {
            Timing timing;
            List<Match> matches = new List<Match>();

            timing = (Timing)ListTimings.SelectedItem;
            matches = (List<Match>)(((Button)sender).CommandParameter);
            if (matches != null)
            {
                System.Diagnostics.Debug.WriteLine("Matches: " + matches.ToString());
                NavigationService.Navigate(new Uri("/ListMatch.xaml?matchs=" + JsonConvert.SerializeObject(matches) + "&five=" + five._id, UriKind.Relative));
            }
        }

    }
}