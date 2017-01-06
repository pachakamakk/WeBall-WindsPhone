using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Newtonsoft.Json.Linq;
using System.Windows.Media.Imaging;

namespace weball_windowsPhone
{
    public partial class MatchListControl : UserControl
    {
        public MatchListControl()
        {
            InitializeComponent();
        }

        private void TextBlock_Loaded(object sender, RoutedEventArgs e)
        {
            creator creatorInfo;

            try
            {
                JToken parsedResponse = JObject.Parse(((TextBlock)sender).Text);
                creatorInfo = parsedResponse.ToObject<creator>();
                ((TextBlock)sender).Text = creatorInfo.fullName;
                ImageCreator.Source = new BitmapImage(new Uri(creatorInfo.photo, UriKind.Absolute));
            }
            catch (Exception ex)
            {

            }
        }
    }
}
