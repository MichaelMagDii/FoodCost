﻿#pragma checksum "..\..\..\TheForms\AllSubCategories.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "4699572A7128E7F2298E8085F18DABA1965C5F9C201C4A5ED40D2A4EF332023A"
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
    /// AllSubCategories
    /// </summary>
    public partial class AllSubCategories : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 33 "..\..\..\TheForms\AllSubCategories.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox SearchTxt;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\TheForms\AllSubCategories.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton RadioByCode;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\TheForms\AllSubCategories.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton RadioByName;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\TheForms\AllSubCategories.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid SubCategories;
        
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
            System.Uri resourceLocater = new System.Uri("/Food_Cost;component/theforms/allsubcategories.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\TheForms\AllSubCategories.xaml"
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
            this.SearchTxt = ((System.Windows.Controls.TextBox)(target));
            
            #line 33 "..\..\..\TheForms\AllSubCategories.xaml"
            this.SearchTxt.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TextDataChange);
            
            #line default
            #line hidden
            return;
            case 2:
            this.RadioByCode = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 3:
            this.RadioByName = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 4:
            this.SubCategories = ((System.Windows.Controls.DataGrid)(target));
            
            #line 40 "..\..\..\TheForms\AllSubCategories.xaml"
            this.SubCategories.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.MouseDoubleClick_click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

