#pragma checksum "..\..\..\TheForms\KitcheItemsn.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "32CAD6D7133EF86DB3C61D847DA45F959086892AA3C3B41DF10F4D1CD1BE1AEB"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Food_Cost;
using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Converters;
using MaterialDesignThemes.Wpf.Transitions;
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


namespace Food_Cost {
    
    
    /// <summary>
    /// KitcheItemsn
    /// </summary>
    public partial class KitcheItemsn : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 32 "..\..\..\TheForms\KitcheItemsn.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox Outletcbx;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\TheForms\KitcheItemsn.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox Kitchencbx;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\TheForms\KitcheItemsn.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid ItemsDGV;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\..\TheForms\KitcheItemsn.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid ButtonGrid;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\..\TheForms\KitcheItemsn.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AddBtn;
        
        #line default
        #line hidden
        
        
        #line 68 "..\..\..\TheForms\KitcheItemsn.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SaveBtn;
        
        #line default
        #line hidden
        
        
        #line 75 "..\..\..\TheForms\KitcheItemsn.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button DeleteBtn;
        
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
            System.Uri resourceLocater = new System.Uri("/Food_Cost;component/theforms/kitcheitemsn.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\TheForms\KitcheItemsn.xaml"
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
            this.Outletcbx = ((System.Windows.Controls.ComboBox)(target));
            
            #line 32 "..\..\..\TheForms\KitcheItemsn.xaml"
            this.Outletcbx.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.OutletComboBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.Kitchencbx = ((System.Windows.Controls.ComboBox)(target));
            
            #line 35 "..\..\..\TheForms\KitcheItemsn.xaml"
            this.Kitchencbx.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ComboBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.ItemsDGV = ((System.Windows.Controls.DataGrid)(target));
            
            #line 44 "..\..\..\TheForms\KitcheItemsn.xaml"
            this.ItemsDGV.MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.RowClicked);
            
            #line default
            #line hidden
            return;
            case 4:
            this.ButtonGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 5:
            this.AddBtn = ((System.Windows.Controls.Button)(target));
            
            #line 61 "..\..\..\TheForms\KitcheItemsn.xaml"
            this.AddBtn.Click += new System.Windows.RoutedEventHandler(this.AddBtn_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.SaveBtn = ((System.Windows.Controls.Button)(target));
            
            #line 68 "..\..\..\TheForms\KitcheItemsn.xaml"
            this.SaveBtn.Click += new System.Windows.RoutedEventHandler(this.SaveBtn_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.DeleteBtn = ((System.Windows.Controls.Button)(target));
            
            #line 75 "..\..\..\TheForms\KitcheItemsn.xaml"
            this.DeleteBtn.Click += new System.Windows.RoutedEventHandler(this.DeleteBtn_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

