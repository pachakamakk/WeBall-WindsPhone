﻿#pragma checksum "C:\Users\norman_e\documents\visual studio 2013\Projects\weball-windowsPhone\weball-windowsPhone\RegisterPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "6AF7A346B275095C58C04A1BC12C0A5F"
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
    
    
    public partial class PivotPage1 : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal Microsoft.Phone.Controls.Pivot registerPivot;
        
        internal System.Windows.Controls.TextBox BoxEmail;
        
        internal System.Windows.Controls.TextBox BoxLogin;
        
        internal System.Windows.Controls.TextBox BoxNom;
        
        internal System.Windows.Controls.TextBox BoxPrénom;
        
        internal Microsoft.Phone.Controls.DatePicker BoxBirthday;
        
        internal System.Windows.Controls.PasswordBox BoxPassword;
        
        internal System.Windows.Controls.PasswordBox BoxPasswordVerify;
        
        internal System.Windows.Controls.Image imageInput;
        
        internal System.Windows.Controls.Button NextButton;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/weball-windowsPhone;component/RegisterPage.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.registerPivot = ((Microsoft.Phone.Controls.Pivot)(this.FindName("registerPivot")));
            this.BoxEmail = ((System.Windows.Controls.TextBox)(this.FindName("BoxEmail")));
            this.BoxLogin = ((System.Windows.Controls.TextBox)(this.FindName("BoxLogin")));
            this.BoxNom = ((System.Windows.Controls.TextBox)(this.FindName("BoxNom")));
            this.BoxPrénom = ((System.Windows.Controls.TextBox)(this.FindName("BoxPrénom")));
            this.BoxBirthday = ((Microsoft.Phone.Controls.DatePicker)(this.FindName("BoxBirthday")));
            this.BoxPassword = ((System.Windows.Controls.PasswordBox)(this.FindName("BoxPassword")));
            this.BoxPasswordVerify = ((System.Windows.Controls.PasswordBox)(this.FindName("BoxPasswordVerify")));
            this.imageInput = ((System.Windows.Controls.Image)(this.FindName("imageInput")));
            this.NextButton = ((System.Windows.Controls.Button)(this.FindName("NextButton")));
        }
    }
}
