using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
using Windows.Devices.Geolocation;

namespace weball_windowsPhone
{
    public partial class PivotPage1 : PhoneApplicationPage
    {
        PhotoChooserTask photoChooserTask;
        public PivotPage1()
        {
            InitializeComponent();
            photoChooserTask = new PhotoChooserTask();
            photoChooserTask.Completed += new EventHandler<PhotoResult>(photoChooserTask_Completed);
        }

        private void removeText(object sender, RoutedEventArgs e)
        {
            if (((TextBox)sender).Text == ((TextBox)sender).Name.Substring(3) || ((TextBox)sender).Text == "Nom Complet")
                ((TextBox)sender).Text = "";
        }

        private void enableText(object sender, RoutedEventArgs e)
        {
            if (((TextBox)sender).Text == "")
                ((TextBox)sender).Text = ((TextBox)sender).Name.Substring(3);
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (registerPivot.SelectedIndex != 4)
                registerPivot.SelectedIndex = registerPivot.SelectedIndex + 1;
            else if (checkRegister())
                doRegister();
        }
        void photoChooserTask_Completed(object sender, PhotoResult e)
        {
            if (e.TaskResult == TaskResult.OK)
            {
                System.Windows.Media.Imaging.BitmapImage bmp = new System.Windows.Media.Imaging.BitmapImage();
                bmp.SetSource(e.ChosenPhoto);
                imageInput.Source = bmp;
            }
        }
        private bool setPopup(string message, int index)
        {
            registerPivot.SelectedIndex = index;
            MessageBoxResult result =
                MessageBox.Show(message,
                    "Erreur",
             MessageBoxButton.OK);
            return false;
        }
        private bool checkRegister()
        {
            if (BoxEmail.Text == "Email")
                return setPopup("Veuillez indiquer un email.", 0);
            if (BoxPassword.Password == "")
                return setPopup("Veuillez indiquer un mot de passe", 2);
            if (BoxPassword.Password != BoxPasswordVerify.Password)
                return setPopup("Vos mots de passes ne sont pas indentiques.", 2);
            return true;
        }
        private async void doRegister()
        {
            if (checkRegister())
            {
                Geolocator geolocator = new Geolocator();
                geolocator.DesiredAccuracyInMeters = 50;
                float[] coord = new float[2];
                try
                {
                    Geoposition geoposition = await geolocator.GetGeopositionAsync(
                        maximumAge: TimeSpan.FromMinutes(5),
                        timeout: TimeSpan.FromSeconds(10)
                        );
                    coord[0] = float.Parse(geoposition.Coordinate.Latitude.ToString("0.00"));
                    coord[1] = float.Parse(geoposition.Coordinate.Longitude.ToString("0.00"));
                }
                catch (Exception ex)
                {
                    if ((uint)ex.HResult == 0x80004004)
                    {
                        System.Diagnostics.Debug.WriteLine("Cant get location");
                        coord[0] = 50.0f;
                        coord[1] = 50.0f;
                    }
                    else
                    {
                    }
                }
                char[] delimiters = { '/', ' ' };
                string[] parsedBirthday = BoxBirthday.Value.ToString().Split(delimiters);
                string birthday = parsedBirthday[2] + ',' + parsedBirthday[1] + ',' + parsedBirthday[0];
                await WeBallAPI.register(BoxPassword.Password, BoxEmail.Text,
                    BoxNom_Complet.Text, birthday, imageInput.Source, coord);
                NavigationService.Navigate(new Uri("/LoginPage.xaml", UriKind.Relative));
            }
        }

        private void selectImage(object sender, RoutedEventArgs e)
        {
            photoChooserTask.Show();
        }
    }
}