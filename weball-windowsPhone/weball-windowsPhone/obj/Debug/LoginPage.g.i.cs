﻿#pragma checksum "C:\Users\norman_e\documents\visual studio 2013\Projects\weball-windowsPhone\weball-windowsPhone\LoginPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "D2B688A2A376FC5F0D3AEC452762D323"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace weball_windowsPhone {
    
    
    public partial class MainPage : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal System.Windows.Controls.Button buttonLogin;
        
        internal System.Windows.Controls.TextBox boxEmail;
        
        internal System.Windows.Controls.PasswordBox boxPassword;
        
        internal System.Windows.Controls.TextBlock lostPassword;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/weball-windowsPhone;component/LoginPage.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.buttonLogin = ((System.Windows.Controls.Button)(this.FindName("buttonLogin")));
            this.boxEmail = ((System.Windows.Controls.TextBox)(this.FindName("boxEmail")));
            this.boxPassword = ((System.Windows.Controls.PasswordBox)(this.FindName("boxPassword")));
            this.lostPassword = ((System.Windows.Controls.TextBlock)(this.FindName("lostPassword")));
        }
    }
}

