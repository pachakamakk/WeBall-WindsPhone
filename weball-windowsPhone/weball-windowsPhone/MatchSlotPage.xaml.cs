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
        public List<Match> matchs = null;
        public MatchSlotPage()
        {
            InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string parameter = string.Empty;
            if (NavigationContext.QueryString.TryGetValue("matchs", out parameter))
            {
                matchs = JsonConvert.DeserializeObject<List<Match>>(parameter);
                List<Timing> timings = null;
                if (matchs != null)
                {
                    GridFiveImage.DataContext = WeBallAPI.FiveList.Where(s => s._id == matchs[0].five).ToList()[0];
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
                        System.Diagnostics.Debug.WriteLine("Date: " + match.startDate);
                    }
                }
                System.Diagnostics.Debug.WriteLine("Name: " + matchs[0].name);
                ListTimings.DataContext = timings;
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/" + ((Button)sender).Name.Substring(6) + "Page.xaml", UriKind.Relative));
        }

        private void ListMatch_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("BITE");
        }

    }
}