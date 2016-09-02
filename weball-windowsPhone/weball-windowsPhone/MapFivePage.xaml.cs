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
    public partial class MapFivePage : PhoneApplicationPage
    {
        public MapFivePage()
        {
            InitializeComponent();
            loadMap();
        }
        private async void Menuitem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem item = (MenuItem)sender;
            string selecteditem = item.Tag.ToString();
            await WeBallAPI.getFive(selecteditem);
            if (WeBallAPI.Success == false)
                return;
            var selectedparkdata = WeBallAPI.FiveList.Where(s => s._id == selecteditem).ToList();
            NavigationService.Navigate(new Uri("/FiveProfilePage.xaml?five=" + JsonConvert.SerializeObject(selectedparkdata[0]), UriKind.Relative));
        }
        private async void _tooltip_Tapimg(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Image item = (Image)sender;
            string selecteditem = item.Tag.ToString();
            await WeBallAPI.getFive(selecteditem);
            if (WeBallAPI.Success == false)
                return;
            var selectedparkdata = WeBallAPI.FiveList.Where(s => s._id == selecteditem).ToList();
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

    }
}