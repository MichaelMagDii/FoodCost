﻿#pragma checksum "..\..\..\TheForms\ProcessBulkItem.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "74D7A9126AD1CE430018F107C70553E25EC0B1BE93090AD21DA6F630970AF80C"
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
    /// ProcessBulkItem
    /// </summary>
    public partial class ProcessBulkItem : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 18 "..\..\..\TheForms\ProcessBulkItem.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.GroupBox Details;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\TheForms\ProcessBulkItem.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox StoreIDcbx;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\TheForms\ProcessBulkItem.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox Kitchencbx;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\TheForms\ProcessBulkItem.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.GroupBox ItemsDetails;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\TheForms\ProcessBulkItem.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid ItemsDGV;
        
        #line default
        #line hidden
        
        
        #line 59 "..\..\..\TheForms\ProcessBulkItem.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BulkItems;
        
        #line default
        #line hidden
        
        
        #line 68 "..\..\..\TheForms\ProcessBulkItem.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid ItemsofBulkItemsDGV;
        
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
            System.Uri resourceLocater = new System.Uri("/Food_Cost;component/theforms/processbulkitem.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\TheForms\ProcessBulkItem.xaml"
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
            this.Details = ((System.Windows.Controls.GroupBox)(target));
            return;
            case 2:
            this.StoreIDcbx = ((System.Windows.Controls.ComboBox)(target));
            
            #line 31 "..\..\..\TheForms\ProcessBulkItem.xaml"
            this.StoreIDcbx.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ResturantComboBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.Kitchencbx = ((System.Windows.Controls.ComboBox)(target));
            
            #line 35 "..\..\..\TheForms\ProcessBulkItem.xaml"
            this.Kitchencbx.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.kitchenComboBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.ItemsDetails = ((System.Windows.Controls.GroupBox)(target));
            return;
            case 5:
            this.ItemsDGV = ((System.Windows.Controls.DataGrid)(target));
            
            #line 45 "..\..\..\TheForms\ProcessBulkItem.xaml"
            this.ItemsDGV.MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.ItemsDGV_MouseLeftButtonUp);
            
            #line default
            #line hidden
            return;
            case 6:
            this.BulkItems = ((System.Windows.Controls.Button)(target));
            
            #line 59 "..\..\..\TheForms\ProcessBulkItem.xaml"
            this.BulkItems.Click += new System.Windows.RoutedEventHandler(this.BulkItemsBtn_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.ItemsofBulkItemsDGV = ((System.Windows.Controls.DataGrid)(target));
            
            #line 69 "..\..\..\TheForms\ProcessBulkItem.xaml"
            this.ItemsofBulkItemsDGV.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.NumberValidationTextBox);
            
            #line default
            #line hidden
            
            #line 69 "..\..\..\TheForms\ProcessBulkItem.xaml"
            this.ItemsofBulkItemsDGV.PreviewKeyDown += new System.Windows.Input.KeyEventHandler(this.NeglectWhiteSpace);
            
            #line default
            #line hidden
            
            #line 69 "..\..\..\TheForms\ProcessBulkItem.xaml"
            this.ItemsofBulkItemsDGV.CellEditEnding += new System.EventHandler<System.Windows.Controls.DataGridCellEditEndingEventArgs>(this.ItemsofBulkItemsDGV_CellEditEnding);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

