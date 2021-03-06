#pragma checksum "..\..\..\TheForms\ParentWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "C2F0660CD57EE7B390DAC4337B1B753F6E5E694D64AA5206E713870B7A6556EA"
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
    /// ParentWindow
    /// </summary>
    public partial class ParentWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 11 "..\..\..\TheForms\ParentWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid AllItemsView;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\TheForms\ParentWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox SearchTxt;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\TheForms\ParentWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton RadioByCode;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\TheForms\ParentWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton RadioByName;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\TheForms\ParentWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid ItemsDGV;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\TheForms\ParentWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid ParentItemsView;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\..\TheForms\ParentWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox ItemNametxt;
        
        #line default
        #line hidden
        
        
        #line 57 "..\..\..\TheForms\ParentWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid ParentItemsDGV;
        
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
            System.Uri resourceLocater = new System.Uri("/Food_Cost;component/theforms/parentwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\TheForms\ParentWindow.xaml"
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
            this.AllItemsView = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            this.SearchTxt = ((System.Windows.Controls.TextBox)(target));
            
            #line 25 "..\..\..\TheForms\ParentWindow.xaml"
            this.SearchTxt.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.SearchTxt_TextChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.RadioByCode = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 4:
            this.RadioByName = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 5:
            this.ItemsDGV = ((System.Windows.Controls.DataGrid)(target));
            
            #line 32 "..\..\..\TheForms\ParentWindow.xaml"
            this.ItemsDGV.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.ItemsDGV_MouseDoubleClick);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 34 "..\..\..\TheForms\ParentWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.BackClickBtn);
            
            #line default
            #line hidden
            return;
            case 7:
            this.ParentItemsView = ((System.Windows.Controls.Grid)(target));
            return;
            case 8:
            this.ItemNametxt = ((System.Windows.Controls.TextBox)(target));
            return;
            case 9:
            this.ParentItemsDGV = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 10:
            
            #line 63 "..\..\..\TheForms\ParentWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.AddBtn_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            
            #line 70 "..\..\..\TheForms\ParentWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.SaveBtn_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            
            #line 77 "..\..\..\TheForms\ParentWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.CancelBtn_Click);
            
            #line default
            #line hidden
            return;
            case 13:
            
            #line 84 "..\..\..\TheForms\ParentWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.DeleteBtn_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

