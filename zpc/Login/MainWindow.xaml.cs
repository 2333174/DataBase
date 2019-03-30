using Login.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Login
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ChangePage.Content = new Frame()
            { Content = new GradePage(5)};
        }
        
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            System.Windows.Application.Current.Shutdown();
        }

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    //用参数select来传递数据,0报名,1管理,2打分
        //    int select;
        //    //判断用户选择的是哪个窗口并显示该窗口
        //    if (radio_sign.IsChecked==true)
        //    {
        //        select = 0;
        //        PassWordWindow passWordWindow = new PassWordWindow(select);
        //        //关闭当前窗口
        //        ChangePage.Content = new Frame()
        //        {
        //            Content = new LoginPage()
        //        };
        //    }
        //    if(radio_manage.IsChecked==true)
        //    {   
        //        select = 1;
        //        //打开管理系统窗口
        //        PassWordWindow passWordWindow = new PassWordWindow(select);
        //        //关闭当前窗口
        //        //passWordWindow.ShowDialog();
        //        ChangePage.Content = new Frame()
        //        {
        //            Content = new LoginPage()
        //        };
        //    }
        //    if(radio_grade.IsChecked==true)
        //    {
        //        select = 2;
        //        PassWordWindow passWordWindow = new PassWordWindow(select);
        //        //关闭当前窗口
        //        ChangePage.Content = new Frame()
        //        {
        //            Content = new LoginPage()
        //        };
        //        //passWordWindow.ShowDialog();

        //    }
        //}
    }
}
