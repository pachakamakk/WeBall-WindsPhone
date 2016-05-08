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

        private void ListMatchs_DoubleTap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Match match;

            match = (Match)(((ListBox)sender).SelectedItem);
            NavigationService.Navigate(new Uri("/ProfileMatchPage.xaml?match=" + JsonConvert.SerializeObject(match) + "&five=" + fiveId, UriKind.Relative));
        }
    }
}