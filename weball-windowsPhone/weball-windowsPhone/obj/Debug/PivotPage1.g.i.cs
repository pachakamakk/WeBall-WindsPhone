﻿#pragma checksum "C:\Users\norman_e\Documents\Visual Studio 2013\Projects\weball-windowsPhone\weball-windowsPhone\PivotPage1.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "110548031925D68CA9890F319BB7DE42"
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
        
        internal System.Windows.Controls.TextBox BoxFirstname;
        
        internal System.Windows.Controls.TextBox BoxLastname;
        
        internal Microsoft.Phone.Controls.DatePicker BoxBirthday;
        
        internal System.Windows.Controls.PasswordBox BoxPassword;
        
        internal System.Windows.Controls.PasswordBox BoxPasswordVerify;
        
        internal System.Windows.Controls.Primitives.Popup PopUp;
        
        internal System.Windows.Controls.TextBlock ErrorText;
        
        internal System.Windows.Controls.Button CloseButton;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/weball-windowsPhone;component/PivotPage1.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.registerPivot = ((Microsoft.Phone.Controls.Pivot)(this.FindName("registerPivot")));
            this.BoxEmail = ((System.Windows.Controls.TextBox)(this.FindName("BoxEmail")));
            this.BoxLogin = ((System.Windows.Controls.TextBox)(this.FindName("BoxLogin")));
            this.BoxFirstname = ((System.Windows.Controls.TextBox)(this.FindName("BoxFirstname")));
            this.BoxLastname = ((System.Windows.Controls.TextBox)(this.FindName("BoxLastname")));
            this.BoxBirthday = ((Microsoft.Phone.Controls.DatePicker)(this.FindName("BoxBirthday")));
            this.BoxPassword = ((System.Windows.Controls.PasswordBox)(this.FindName("BoxPassword")));
            this.BoxPasswordVerify = ((System.Windows.Controls.PasswordBox)(this.FindName("BoxPasswordVerify")));
            this.PopUp = ((System.Windows.Controls.Primitives.Popup)(this.FindName("PopUp")));
            this.ErrorText = ((System.Windows.Controls.TextBlock)(this.FindName("ErrorText")));
            this.CloseButton = ((System.Windows.Controls.Button)(this.FindName("CloseButton")));
            this.NextButton = ((System.Windows.Controls.Button)(this.FindName("NextButton")));
        }
    }
}

