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
    public partial class ChangePasswordPage : PhoneApplicationPage
    {
        public ChangePasswordPage()
        {
            InitializeComponent();
        }

        private void setPopup(string message)
        {
            MessageBoxResult result =
                MessageBox.Show(message,
                    "Erreur",
             MessageBoxButton.OK);
        }
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (OldPasswordBox.Password.Length == 0)
            {
                setPopup("Merci de saisir votre ancien mot de passe.");
                return;
            }
            if (OldPasswordBox.Password != WeBallAPI.currentUser.password)
            {
                setPopup("Ancien mot de passe incorrect.");
                return;
            }
            if (NewPasswordBox.Password.Length <= 2)
            {
                setPopup("Nouveau mot de passe trop court.");
                return;
            }
            if (NewPasswordBox.Password != VerifyPassword.Password)
            {
                setPopup("Mot de passe différent de la validation.");
                return;
            }
            WeBallAPI.currentUser.password = NewPasswordBox.Password;
            await WeBallAPI.updateUser();
            MessageBoxResult result =
                MessageBox.Show("Mot de passe changé!",
                    "Confirmation",
            MessageBoxButton.OK);
            NavigationService.Navigate(new Uri("/ProfilePage.xaml", UriKind.Relative));
        }
    }
}