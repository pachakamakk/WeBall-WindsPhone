﻿#pragma checksum "C:\Users\norman_e\documents\visual studio 2013\Projects\weball-windowsPhone\weball-windowsPhone\ProfilePage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "BBB52A733A27BC49D28D7466B42C48A0"
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
    
    
    public partial class Page1 : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.StackPanel profileStack;
        
        internal System.Windows.Controls.TextBlock FirstNamePrompt;
        
        internal System.Windows.Controls.Image ProfileImage;
        
        internal System.Windows.Controls.TextBlock winBox;
        
        internal System.Windows.Controls.TextBlock drawBox;
        
        internal System.Windows.Controls.TextBlock looseBox;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/weball-windowsPhone;component/ProfilePage.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.profileStack = ((System.Windows.Controls.StackPanel)(this.FindName("profileStack")));
            this.FirstNamePrompt = ((System.Windows.Controls.TextBlock)(this.FindName("FirstNamePrompt")));
            this.ProfileImage = ((System.Windows.Controls.Image)(this.FindName("ProfileImage")));
            this.winBox = ((System.Windows.Controls.TextBlock)(this.FindName("winBox")));
            this.drawBox = ((System.Windows.Controls.TextBlock)(this.FindName("drawBox")));
            this.looseBox = ((System.Windows.Controls.TextBlock)(this.FindName("looseBox")));
        }
    }
}

