#pragma checksum "..\..\All_Kitchens.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "97F0957CE9C9B52B43132A9709B7EA4031E9EFCCF30ADE2ADB6ED49E24E3EBA7"
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
    /// All_Kitchens
    /// </summary>
    public partial class All_Kitchens : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 31 "..\..\All_Kitchens.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox SearchTxt;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\All_Kitchens.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton RadioByCode;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\All_Kitchens.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton RadioByName;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\All_Kitchens.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid KitchensDGV;
        
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
            System.Uri resourceLocater = new System.Uri("/Food_Cost;component/all_kitchens.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\All_Kitchens.xaml"
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
            
            #line 31 "..\..\All_Kitchens.xaml"
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
            this.KitchensDGV = ((System.Windows.Controls.DataGrid)(target));
            
            #line 37 "..\..\All_Kitchens.xaml"
            this.KitchensDGV.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.KitchenDGV_MouseDoubleClick);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

