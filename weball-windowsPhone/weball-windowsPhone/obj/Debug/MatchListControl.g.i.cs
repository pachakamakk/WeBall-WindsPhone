﻿#pragma checksum "C:\Users\norman_e\documents\visual studio 2013\Projects\weball-windowsPhone\weball-windowsPhone\MatchListControl.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "E5C70147652F879CC9A6368B9D4CD20C"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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
    
    
    public partial class MatchListControl : System.Windows.Controls.UserControl {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.TextBlock TitleBox;
        
        internal System.Windows.Controls.TextBlock TeamABox;
        
        internal System.Windows.Controls.TextBlock TeamBBox;
        
        internal System.Windows.Controls.TextBlock teamACount;
        
        internal System.Windows.Controls.TextBlock teamBCount;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/weball-windowsPhone;component/MatchListControl.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.TitleBox = ((System.Windows.Controls.TextBlock)(this.FindName("TitleBox")));
            this.TeamABox = ((System.Windows.Controls.TextBlock)(this.FindName("TeamABox")));
            this.TeamBBox = ((System.Windows.Controls.TextBlock)(this.FindName("TeamBBox")));
            this.teamACount = ((System.Windows.Controls.TextBlock)(this.FindName("teamACount")));
            this.teamBCount = ((System.Windows.Controls.TextBlock)(this.FindName("teamBCount")));
        }
    }
}

