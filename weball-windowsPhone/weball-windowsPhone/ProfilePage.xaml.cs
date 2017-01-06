using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media;

namespace weball_windowsPhone
{
    public partial class Page1 : PhoneApplicationPage
    {
        public Page1()
        {
            InitializeComponent();
        }
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            string parameter;
            
            if (NavigationContext.QueryString.TryGetValue("user", out parameter))
            {
                if (parameter == WeBallAPI.currentUser._id)
                {
                    FirstNamePrompt.Text = WeBallAPI.currentUser.fullName;
                    profileStack.DataContext = WeBallAPI.currentUser;
                }
                else
                {
                    await WeBallAPI.getUser(parameter);
                    addFriend.Opacity = 100;
                    addFriend.IsEnabled = true;
                    if (WeBallAPI.Success == false)
                        return;
/*                    if (WeBallAPI.profileUser.relationStatus.isRelation == 0)
                    {
                        addFriend.Opacity = 100;
                        GridAdd.Background = new SolidColorBrush(Colors.Orange);
                    }
                    else if (WeBallAPI.profileUser.relationStatus.isRelation == 2)
                    {
                        addFriend.Opacity = 100;
                        GridAdd.Background = new SolidColorBrush(Colors.Green);
                    }
                    else
                    {
                        addFriend.Opacity = 100;
                        addFriend.IsEnabled = true;
                    }*/
                    FirstNamePrompt.Text = WeBallAPI.profileUser.fullName;
                    profileStack.DataContext = WeBallAPI.profileUser;
                }
            }
            else
            {
                await WeBallAPI.me();
                if (WeBallAPI.Success == false)
                    return;
                FirstNamePrompt.Text = WeBallAPI.currentUser.fullName;
                profileStack.DataContext = WeBallAPI.currentUser;
            }
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/"+ ((Button)sender).Name.Substring(6) + "Page.xaml", UriKind.Relative));
        }

        private async void addFriend_Click(object sender, RoutedEventArgs e)
        {
            await WeBallAPI.sendRequest(WeBallAPI.profileUser._id);
        }

        private void Image_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/ParamProfilePage.xaml", UriKind.Relative));
        }

        private void goSearch(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/SearchUserPage.xaml", UriKind.Relative));
        }
    }
}