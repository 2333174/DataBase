﻿#pragma checksum "..\..\..\Views\ChooseJudge.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "AF1A404FB8E60CE7BA34F95D6C1FE147429A0FB7"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using Login.Views;
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


namespace Login.Views {
    
    
    /// <summary>
    /// ChooseJudge
    /// </summary>
    public partial class ChooseJudge : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 2 "..\..\..\Views\ChooseJudge.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Login.Views.ChooseJudge ChooseJudgePage;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\..\Views\ChooseJudge.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ContentControl ChangePage;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\Views\ChooseJudge.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid judgegrid;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\Views\ChooseJudge.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox MainJudgeName;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\Views\ChooseJudge.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox MainID;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\Views\ChooseJudge.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox GroupJudgeName;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\Views\ChooseJudge.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox GroupID;
        
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
            System.Uri resourceLocater = new System.Uri("/Login;component/views/choosejudge.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\ChooseJudge.xaml"
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
            this.ChooseJudgePage = ((Login.Views.ChooseJudge)(target));
            return;
            case 2:
            this.ChangePage = ((System.Windows.Controls.ContentControl)(target));
            return;
            case 3:
            this.judgegrid = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 4:
            this.MainJudgeName = ((System.Windows.Controls.ComboBox)(target));
            
            #line 25 "..\..\..\Views\ChooseJudge.xaml"
            this.MainJudgeName.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.MainJudgeName_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 27 "..\..\..\Views\ChooseJudge.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click_3);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 28 "..\..\..\Views\ChooseJudge.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click_1);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 29 "..\..\..\Views\ChooseJudge.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click_2);
            
            #line default
            #line hidden
            return;
            case 8:
            this.MainID = ((System.Windows.Controls.TextBox)(target));
            return;
            case 9:
            this.GroupJudgeName = ((System.Windows.Controls.ComboBox)(target));
            
            #line 31 "..\..\..\Views\ChooseJudge.xaml"
            this.GroupJudgeName.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.GroupJudgeName_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 10:
            this.GroupID = ((System.Windows.Controls.TextBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

