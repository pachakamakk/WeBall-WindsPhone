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
    public partial class ProfileMatchPage : PhoneApplicationPage
    {
        public Five five;
        public int teamId = -1;
        public Match match;
        public ProfileMatchPage()
        {
            InitializeComponent();
        }
        
        private bool checkMember(Teams team, int id)
        {
            foreach (weball_windowsPhone.Teams.littleUser user in team.users)
            {
                if (user._id == WeBallAPI.currentUser._id)
                {
                    teamId = id;
                    return true;
                }
            }
            return false;
        }
        private string getMember(string name)
        {
            foreach (Teams team in match.teams)
            {
                foreach (weball_windowsPhone.Teams.littleUser user in team.users)
                {
                    if (user.fullName == name)
                        return user._id;
                }
            }
            return "";
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string parameter;

            if (NavigationContext.QueryString.TryGetValue("match", out parameter))
            {
                match = JsonConvert.DeserializeObject<Match>(parameter);
                System.Diagnostics.Debug.WriteLine("Match: " + match.teams[0].name);
                if (match.teams[0].name != null)
                    textBlock1.Text = match.teams[0].name;
                else
                    textBlock1.Text = "Unknown";
                if (match.teams[1].name != null)
                    textBlock.Text = match.teams[1].name;
                else
                    textBlock.Text = "Unknown";

                System.Diagnostics.Debug.WriteLine("Match: " + match);
            }
            if (NavigationContext.QueryString.TryGetValue("five", out parameter))
            {
                five = WeBallAPI.FiveList.Where(s => s._id == (string)parameter).ToList()[0];
            }
            ContentPanel.DataContext = match;
            fiveGrid.DataContext = five;
            System.Diagnostics.Debug.WriteLine("BITE");
            DateBlock.Text = match.startDate.ToString();
            System.Diagnostics.Debug.WriteLine("BITEEE");
            if (checkMember(match.teams[0], 0) || checkMember(match.teams[1], 1))
            {
                QuitButton.IsEnabled = true;
                QuitButton.Opacity = 100;
                buttonJoin1.IsEnabled = false;
                buttonJoin1.Opacity = 0;
                buttonJoin2.IsEnabled = false;
                buttonJoin2.Opacity = 0;
            }
            System.Diagnostics.Debug.WriteLine("BITZZAZ");
        }

        private void TextBlock_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
        	// TODO : ajoutez ici l'implémentation du gestionnaire d'événements.
        }
        private bool setPopup(string message)
        {
            MessageBoxResult result =
                MessageBox.Show(message,
                    "Rejoindre",
             MessageBoxButton.OK);
            return false;
        }
        private async void button_Click(object sender, RoutedEventArgs e)
        {
            Button obj = (Button)sender;

            if ((string)(obj.CommandParameter) == "1")
            {
                await WeBallAPI.joinMatch(match.teams[0]._id);
            }
            else
            {
                await WeBallAPI.joinMatch(match.teams[1]._id);
            }
            if (WeBallAPI.Success)
                setPopup("Succes!");
            else
                setPopup("Erreur");
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (teamId == -1)
                return;
            await WeBallAPI.leaveMatch(match.teams[teamId]._id);
            MessageBoxResult result =
                MessageBox.Show("Match quitté!",
                    "Confirmation",
            MessageBoxButton.OK);
            NavigationService.Navigate(new Uri("/FiveProfilePage.xaml?five=" + JsonConvert.SerializeObject(five), UriKind.Relative));
        }

        private void Grid_DoubleTap(object sender, System.Windows.Input.GestureEventArgs e)
        {
        }

        private void TextBlock_Tap_1(object sender, System.Windows.Input.GestureEventArgs e)
        {
            string name = ((TextBlock)sender).Text;
            string id = getMember(name);
            
            if (id != "")
                NavigationService.Navigate(new Uri("/ProfilePage.xaml?user=" + id, UriKind.Relative));
        }
    }
}