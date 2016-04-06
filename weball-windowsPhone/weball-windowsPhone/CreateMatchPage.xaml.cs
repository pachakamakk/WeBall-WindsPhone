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
using System.Windows.Media;

namespace weball_windowsPhone
{
    public partial class CreateMatchPage : PhoneApplicationPage
    {
        DateTime date;
        Five five = null;
        public CreateMatchPage()
        {
            InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string parameter = string.Empty;

            if (NavigationContext.QueryString.TryGetValue("date", out parameter) && five == null)
            {
                date = JsonConvert.DeserializeObject<DateTime>(parameter);
                BoxHour.Value = date.Date + new TimeSpan(10, 30, 0);
                DatePrompt.Text = date.ToString();
            }
            if (NavigationContext.QueryString.TryGetValue("five", out parameter) && five == null)
            {
                five = WeBallAPI.FiveList.Where(s => s._id == (string)parameter).ToList()[0];
            }

        }
        private bool setPopup(string message)
        {
            MessageBoxResult result =
                MessageBox.Show(message,
                    "Erreur",
             MessageBoxButton.OK);
            return false;
        }

        private async void ValidateMatch(object sender, RoutedEventArgs e)
        {
            List<Field> fields = five.fields;
            string selectedField = "";
            List<string> invalid = new List<string>();
            
            if (NameBox.Text == "" || NumberBox.Text == "")
                setPopup("Merci de renseinger tous les champs!");
            else if (Int32.Parse(NumberBox.Text) < 2 || Int32.Parse(NumberBox.Text) > 5)
                setPopup("Merci de renseinger un nombre de joueurs entre 2 et 5!");
            else if (DateTime.Compare(date, DateTime.Today) < 0)
                setPopup("Date du match dépassée!");
            else
            {
                if (five != null)
                {
                    foreach (Match elem in (five.matchs.Where(match => (match.startDate.Day == date.Day) &&
                                (match.startDate.Month == date.Month) &&
                                (match.startDate.Year == date.Year) &&
                                (match.startDate.Hour == ((DateTime)BoxHour.Value).Hour)).ToList()))
                    {

                        if (!invalid.Any(s => (s == elem.field)))
                            invalid.Add(elem.field);
                    }
                    foreach (Field field in fields)
                    {
                        if (!invalid.Any(s => (s == field._id)))
                            selectedField = field._id;
                    }
                    if (selectedField == "")
                        setPopup("Impossible de créer un match a cette horaire.");
                    else
                    {
                        await WeBallAPI.addMatch(NameBox.Text, date, (DateTime)BoxHour.Value, ((DateTime)(BoxHour.Value)).AddHours(1),
                                                    Int32.Parse(NumberBox.Text) * 2, selectedField);
                        if (!WeBallAPI.Success)
                            setPopup("Erreur. Impossible de créer un match!");
                        else
                        {
                            await WeBallAPI.getFive(five._id);
                            var selectedparkdata = WeBallAPI.FiveList.Where(s => s._id == five._id).ToList();
                            NavigationService.Navigate(new Uri("/FiveProfilePage.xaml?five=" + JsonConvert.SerializeObject(selectedparkdata[0]), UriKind.Relative));
                        }
                    }
                }
            }
        }

        private void NameBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void NameBox_GotFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).Background = new SolidColorBrush(Colors.Gray);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MatchSlotPage.xaml?five=" + five._id + "&date=" + JsonConvert.SerializeObject(date), UriKind.Relative));
        }

    }
}