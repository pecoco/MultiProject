﻿#pragma checksum "..\..\MainWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "877FEE250D54EFAC7D5CC5354316662D"
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
using Wpf.RadarWindow;


namespace Wpf.RadarWindow {
    
    
    /// <summary>
    /// MainWindow
    /// </summary>
    public partial class MainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 1 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Wpf.RadarWindow.MainWindow RadarWindow;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid vbox;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image img;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Media.DrawingGroup dg;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Rectangle rect;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.ToggleButton btResize;
        
        #line default
        #line hidden
        
        
        #line 122 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.ToggleButton btAntiPersonnel;
        
        #line default
        #line hidden
        
        
        #line 177 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.ToggleButton btSelect;
        
        #line default
        #line hidden
        
        
        #line 233 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btZoomIn;
        
        #line default
        #line hidden
        
        
        #line 243 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btZoomOut;
        
        #line default
        #line hidden
        
        
        #line 252 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btSwitch;
        
        #line default
        #line hidden
        
        
        #line 258 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Rectangle rtClipBar;
        
        #line default
        #line hidden
        
        
        #line 272 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btFlagOn;
        
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
            System.Uri resourceLocater = new System.Uri("/RadarWindow;component/mainwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\MainWindow.xaml"
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
            this.RadarWindow = ((Wpf.RadarWindow.MainWindow)(target));
            
            #line 16 "..\..\MainWindow.xaml"
            this.RadarWindow.SizeChanged += new System.Windows.SizeChangedEventHandler(this.RadarWindow_SizeChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.vbox = ((System.Windows.Controls.Grid)(target));
            
            #line 26 "..\..\MainWindow.xaml"
            this.vbox.SizeChanged += new System.Windows.SizeChangedEventHandler(this.Grid_SizeChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.img = ((System.Windows.Controls.Image)(target));
            return;
            case 4:
            this.dg = ((System.Windows.Media.DrawingGroup)(target));
            return;
            case 5:
            this.rect = ((System.Windows.Shapes.Rectangle)(target));
            return;
            case 6:
            this.btResize = ((System.Windows.Controls.Primitives.ToggleButton)(target));
            
            #line 52 "..\..\MainWindow.xaml"
            this.btResize.Click += new System.Windows.RoutedEventHandler(this.btResie_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.btAntiPersonnel = ((System.Windows.Controls.Primitives.ToggleButton)(target));
            return;
            case 8:
            this.btSelect = ((System.Windows.Controls.Primitives.ToggleButton)(target));
            return;
            case 9:
            this.btZoomIn = ((System.Windows.Controls.Button)(target));
            
            #line 233 "..\..\MainWindow.xaml"
            this.btZoomIn.Click += new System.Windows.RoutedEventHandler(this.btZoomIn_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.btZoomOut = ((System.Windows.Controls.Button)(target));
            return;
            case 11:
            this.btSwitch = ((System.Windows.Controls.Button)(target));
            
            #line 252 "..\..\MainWindow.xaml"
            this.btSwitch.Click += new System.Windows.RoutedEventHandler(this.btSwitch_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            this.rtClipBar = ((System.Windows.Shapes.Rectangle)(target));
            return;
            case 13:
            this.btFlagOn = ((System.Windows.Controls.Button)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

