using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using weball_windowsPhone.Resources;

namespace weball_windowsPhone
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructeur
        public MainPage()
        {
            InitializeComponent();

            // Exemple de code pour la localisation d'ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        private async Task tryLogin(object sender, RoutedEventArgs e)
        {
            string  username;
            string  password;

            username = boxEmail.Text;
            password = boxPassword.Password;
            System.Diagnostics.Debug.WriteLine(username + " and " + password);
            await WeBallAPI.login(username, password);
        }

        private async void buttonLogin_Click(object sender, RoutedEventArgs e)
        {
            await tryLogin(sender, e);
            if (WeBallAPI.Success == false)
                return;
            await WeBallAPI.me();
            if (WeBallAPI.Success == false)
                return;
            System.Diagnostics.Debug.WriteLine("Logging in");
            if (!string.IsNullOrEmpty(WeBallAPI.Token))
            {
                try {
                    await WeBallAPI.updatePos();
                }
                catch (Exception exception)
                {
                    MessageBoxResult result =
                            MessageBox.Show("Problème de localisation, vérifiez vos paramètres.",
                                "Location",
                             MessageBoxButton.OK);
                    return;
                }
                System.Diagnostics.Debug.WriteLine("get Fives");
                await WeBallAPI.getFives();
                if (WeBallAPI.Success == false)
                    return;
                NavigationService.Navigate(new Uri("/ProfilePage.xaml", UriKind.Relative));
            }
            else
                System.Diagnostics.Debug.WriteLine("Problem");
        }

        private void buttonRegister_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/RegisterPage.xaml", UriKind.Relative));
        }

        private void removeText(object sender, RoutedEventArgs e)
        {
           if (((TextBox)sender).Text == "Email")
                ((TextBox) sender).Text = "";
        }

        private void enableText(object sender, RoutedEventArgs e)
        {
            if (((TextBox)sender).Text == "")
                ((TextBox)sender).Text = "Email";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/RegisterPage.xaml", UriKind.Relative));
        }

        private void lostPassword_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
        }
    }
}