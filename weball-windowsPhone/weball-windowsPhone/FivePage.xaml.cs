using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Maps.Controls;
using Microsoft.Phone.Maps.Toolkit;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Interop;
using System.IO.IsolatedStorage;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using System.Device.Location;

namespace weball_windowsPhone
{
    public partial class FivePage : PhoneApplicationPage
    {
       private void MapPivot_LoadedPivotItem(object sender, PivotItemEventArgs e)
       {
            if (MapPivot.SelectedIndex == 0) {
                LineMap.Opacity = 100;
                MapText.Opacity = 100;
                LineList.Opacity = 0;
                ListText.Opacity = .40;
            }
            else if (MapPivot.SelectedIndex == 1) {
                LineList.Opacity = 100;
                ListText.Opacity = 100;
                LineMap.Opacity = 0;
                MapText.Opacity = .40;
            }
        }
       public void loadFiveList()
       {
           ListFive.DataContext = WeBallAPI.FiveList;
       }

       private async void Menuitem_Click(object sender, RoutedEventArgs e)
       {
           MenuItem item = (MenuItem)sender;
           string selecteditem = item.Tag.ToString();
           await WeBallAPI.getFive(selecteditem);
           var selectedparkdata = WeBallAPI.FiveList.Where(s => s._id == selecteditem).ToList();
           NavigationService.Navigate(new Uri("/FiveProfilePage.xaml?five=" + JsonConvert.SerializeObject(selectedparkdata[0]), UriKind.Relative)); 
       }
       private void _tooltip_Tapimg(object sender, System.Windows.Input.GestureEventArgs e)
       {
           Image item = (Image)sender;
           string selecteditem = item.Tag.ToString();
           var selectedparkdata = WeBallAPI.FiveList.Where(s => s._id == selecteditem).ToList();
           System.Diagnostics.Debug.WriteLine("Lolololol: " + selecteditem.ToString());
           if (selectedparkdata.Count > 0)
               {
                   foreach (var items in selectedparkdata)
                   {
                       ContextMenu contextMenu =
                   ContextMenuService.GetContextMenu(item);
                       contextMenu.DataContext = items;
                       if (contextMenu.Parent == null)
                       {
                           contextMenu.IsOpen = true;

                       }
                       break;
                   }
               }
       }
       public void loadMap()
       {
           var layer = new MapLayer();
           foreach (var five in WeBallAPI.FiveList)
           {
               UCCustomToolTip _tooltip = new UCCustomToolTip
               {
                   Description = five.name,
                   DataContext = five,
               };
               _tooltip.Menuitem.Click += Menuitem_Click;
               _tooltip.imgmarker.Tap += _tooltip_Tapimg;
               var overlay = new MapOverlay
               {
                   Content = _tooltip,
                   GeoCoordinate = new GeoCoordinate(five.gps[1], five.gps[0], 0.0d),
                   PositionOrigin = new Point(0.5, 0.8)
               };
               layer.Add(overlay);
           }
           MapFive.Layers.Add(layer);
       }
        public FivePage()
        {
            InitializeComponent();
            loadMap();
            loadFiveList();
        }

        private void MapButton_Click(object sender, RoutedEventArgs e)
        {
            MapPivot.SelectedIndex = 0;
        }

        private void ListButton_Click(object sender, RoutedEventArgs e)
        {
            MapPivot.SelectedIndex = 1;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/" + ((Button)sender).Name.Substring(6) + "Page.xaml", UriKind.Relative));
        }
        private void ListFive_DoubleTap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            var list = sender as ListBox;
            Five obj = list.SelectedItem as Five;
            if (obj != null)
            {
                var selectedparkdata = WeBallAPI.FiveList.Where(s => s._id == obj._id).ToList();
                NavigationService.Navigate(new Uri("/FiveProfilePage.xaml?five=" + JsonConvert.SerializeObject(selectedparkdata[0]), UriKind.Relative));
            }
        }
    }
}