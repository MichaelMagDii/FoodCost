#pragma checksum "..\..\BulkWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "7A5BF46E65C5AC65C239D43EAE7B3BBF0DCA6B691C0156720B560B4EB115EA0D"
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
    /// BulkWindow
    /// </summary>
    public partial class BulkWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 23 "..\..\BulkWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox ItemNametxt;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\BulkWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid BulkItemsDGV;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\BulkWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid ItemsDGV;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\BulkWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox WaisWeightttxt;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\BulkWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox WaistCosttxt;
        
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
            System.Uri resourceLocater = new System.Uri("/Food_Cost;component/bulkwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\BulkWindow.xaml"
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
            this.ItemNametxt = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.BulkItemsDGV = ((System.Windows.Controls.DataGrid)(target));
            
            #line 25 "..\..\BulkWindow.xaml"
            this.BulkItemsDGV.CellEditEnding += new System.EventHandler<System.Windows.Controls.DataGridCellEditEndingEventArgs>(this.Changes_CellEditEnding);
            
            #line default
            #line hidden
            
            #line 27 "..\..\BulkWindow.xaml"
            this.BulkItemsDGV.MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.BulkItemsDGV_MouseLeftButtonUp);
            
            #line default
            #line hidden
            return;
            case 3:
            this.ItemsDGV = ((System.Windows.Controls.DataGrid)(target));
            
            #line 31 "..\..\BulkWindow.xaml"
            this.ItemsDGV.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.ItemsDGV_MouseDoubleClick);
            
            #line default
            #line hidden
            return;
            case 4:
            this.WaisWeightttxt = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.WaistCosttxt = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            
            #line 59 "..\..\BulkWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.SaveBtn_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 66 "..\..\BulkWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.AddItemClick);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 73 "..\..\BulkWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.DeleteItemClick);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

