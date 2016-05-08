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
        public Match match;
        public ProfileMatchPage()
        {
            InitializeComponent();
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
            DateBlock.Text = match.startDate.ToString();
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

    }
}