﻿#pragma checksum "..\..\..\StudyModeWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9238E0359E1F5B60A8D4D46C00E46CFBB699E6DC"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Prj_Soft_Protection;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace Prj_Soft_Protection {
    
    
    /// <summary>
    /// StudyModeWindow
    /// </summary>
    public partial class StudyModeWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 19 "..\..\..\StudyModeWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox InputField;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\StudyModeWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button CloseStudyMode;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\..\StudyModeWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label SymbolCount;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\..\StudyModeWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Verified;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\..\StudyModeWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox CountProtection;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.13.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Prj_Soft_Protection;component/studymodewindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\StudyModeWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.13.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.InputField = ((System.Windows.Controls.TextBox)(target));
            
            #line 22 "..\..\..\StudyModeWindow.xaml"
            this.InputField.KeyDown += new System.Windows.Input.KeyEventHandler(this.InputField_KeyDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.CloseStudyMode = ((System.Windows.Controls.Button)(target));
            
            #line 32 "..\..\..\StudyModeWindow.xaml"
            this.CloseStudyMode.Click += new System.Windows.RoutedEventHandler(this.CloseStudyMode_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.SymbolCount = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.Verified = ((System.Windows.Controls.TextBox)(target));
            
            #line 50 "..\..\..\StudyModeWindow.xaml"
            this.Verified.KeyDown += new System.Windows.Input.KeyEventHandler(this.Verified_KeyDown);
            
            #line default
            #line hidden
            return;
            case 5:
            this.CountProtection = ((System.Windows.Controls.TextBox)(target));
            
            #line 51 "..\..\..\StudyModeWindow.xaml"
            this.CountProtection.KeyDown += new System.Windows.Input.KeyEventHandler(this.CountProtection_KeyDown);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
