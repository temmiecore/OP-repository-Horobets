#pragma checksum "..\..\FeeW.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "4B1F361F603E9540AF4BE7BD3B4E90520C8ADE43EC7C3D9E6D9EA8127B97041D"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using ConnectToSQLServer;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
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


namespace ConnectToSQLServer {
    
    
    /// <summary>
    /// FeeW
    /// </summary>
    public partial class FeeW : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 20 "..\..\FeeW.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox IDBox;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\FeeW.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label FeeLabel;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\FeeW.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button OneAttBtn;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\FeeW.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AllAttBtn;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\FeeW.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AllCasesBtn;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\FeeW.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ExitBtn;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\FeeW.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button PrintBtn;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/ConnectToSQLServer;component/feew.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\FeeW.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.IDBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.FeeLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.OneAttBtn = ((System.Windows.Controls.Button)(target));
            
            #line 23 "..\..\FeeW.xaml"
            this.OneAttBtn.Click += new System.Windows.RoutedEventHandler(this.OneAttBtn_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.AllAttBtn = ((System.Windows.Controls.Button)(target));
            
            #line 24 "..\..\FeeW.xaml"
            this.AllAttBtn.Click += new System.Windows.RoutedEventHandler(this.AllAttBtn_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.AllCasesBtn = ((System.Windows.Controls.Button)(target));
            
            #line 25 "..\..\FeeW.xaml"
            this.AllCasesBtn.Click += new System.Windows.RoutedEventHandler(this.AllCasesBtn_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.ExitBtn = ((System.Windows.Controls.Button)(target));
            
            #line 26 "..\..\FeeW.xaml"
            this.ExitBtn.Click += new System.Windows.RoutedEventHandler(this.ExitBtn_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.PrintBtn = ((System.Windows.Controls.Button)(target));
            
            #line 29 "..\..\FeeW.xaml"
            this.PrintBtn.Click += new System.Windows.RoutedEventHandler(this.PrintBtn_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

