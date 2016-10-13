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
    public partial class EditMatch : PhoneApplicationPage
    {
        Match match;
        public EditMatch()
        {
            InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string parameter;

            if (NavigationContext.QueryString.TryGetValue("match", out parameter))
            {
                match = JsonConvert.DeserializeObject<Match>(parameter);
                MatchName.Text = match.name;
                MatchLength.Text = (match.endDate.Hour - match.startDate.Hour).ToString();
                MatchTime.Value = match.startDate;
            }
        }
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            match.name = MatchName.Text;
            match.startDate = MatchTime.Value.Value;
            await WeBallAPI.updateMatch(match, int.Parse(MatchLength.Text));
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/ProfileMatchPage.xaml?match=" + JsonConvert.SerializeObject(match) + "&five=" + match.five._id, UriKind.Relative)); 
        }
    }
}