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
    public partial class NotifControl : UserControl
    {
        public NotifControl()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            string MatchId = (string)(((Button)sender).CommandParameter);

            if (MatchId != null)
            {
                await WeBallAPI.getMatch(MatchId, 1);
                System.Diagnostics.Debug.WriteLine("aaa");
                if (WeBallAPI.Success == false)
                    return;
                Match currentMatch = WeBallAPI.FiveList[WeBallAPI.returnedIndex].matchs.FirstOrDefault(s => s._id == MatchId);
                (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/ProfileMatchPage.xaml?match=" + JsonConvert.SerializeObject(currentMatch) + "&five=" + WeBallAPI.FiveList[WeBallAPI.returnedIndex]._id, UriKind.Relative));
            }
        }
    }
}
