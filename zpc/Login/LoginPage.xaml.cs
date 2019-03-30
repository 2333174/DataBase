﻿using System;
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
using DB;

namespace Login
{
    /// <summary>
    /// LoginPage.xaml 的交互逻辑
    /// </summary>
    public partial class LoginPage : Page
    {
        public int select { get; set; }
        public LoginPage(int s)
        {
            InitializeComponent();
            select = s;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ChangePage.Content = new Frame()
            { Content = new ChosePage() };
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Loginf(object sender, RoutedEventArgs e)
        {
            var db = new GymDBService();
            switch (db.loginf(user.Text, password.Password, select))
            {
                case -1:
                    ShowMessageInfo("账户或密码有误");
                    break;
                case 0:
                    if (select==0){
                        ChangePage.Content = new Frame()
                        { Content = new SignUpPage() };
                    }
                    else {
                        ChangePage.Content = new Frame()
                        { Content = new ManagePage() };
                    }
                    break;
                case 1:
                    ChangePage.Content = new Frame()
                    {
                        Content = new GradePage()
                    };
                    break;
                default:
                    ChangePage.Content = new Frame()
                    {
                        Content = new GSForMajorJudgePage()
                    };
                    break;
            }
        }
        private async void ShowMessageInfo(string message)
        {
            MessageDialog samMessageDialog = new MessageDialog
            {
                Message = { Text = message }
            };
            await MaterialDesignThemes.Wpf.DialogHost.Show(samMessageDialog);
        }
    }
}
