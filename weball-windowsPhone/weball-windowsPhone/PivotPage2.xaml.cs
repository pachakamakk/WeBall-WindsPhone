﻿using System;
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

    public partial class PivotPage2 : PhoneApplicationPage
    {
        public PivotPage2()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/" + ((Button)sender).Name.Substring(6) + "Page.xaml", UriKind.Relative));
        }
    }
}