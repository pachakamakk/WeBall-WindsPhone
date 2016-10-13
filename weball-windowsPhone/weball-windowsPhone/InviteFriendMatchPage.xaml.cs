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
    public partial class InviteFriendMatchPage : PhoneApplicationPage
    {
        Match match;
        public InviteFriendMatchPage()
        {
            InitializeComponent();
        }
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            string parameter;

            if (NavigationContext.QueryString.TryGetValue("match", out parameter))
                match = JsonConvert.DeserializeObject<Match>(parameter);
            await WeBallAPI.getRelations();
            if (WeBallAPI.Success == false)
                return;
            ListFriends.ItemsSource = WeBallAPI.relations;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/ProfileMatchPage.xaml?match=" + JsonConvert.SerializeObject(match) + "&five=" + match.five._id, UriKind.Relative)); 
        }

    }
}