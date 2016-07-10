using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace weball_windowsPhone
{
    public partial class FriendListElem : UserControl
    {
        public FriendListElem()
        {
            InitializeComponent();
        }

        private async void Image_Tap(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Ceci va retirer cet ami. Continuer?", "Suppression", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                System.Diagnostics.Debug.WriteLine("removing");
                await WeBallAPI.eraseRelationship((string)(((Button)sender).CommandParameter));
                (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/ProfilePage.xaml", UriKind.Relative));
            }

        }
    }
}
