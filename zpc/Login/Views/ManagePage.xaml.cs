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
using DB;

namespace Login.Views
{
    /// <summary>
    /// ManagePage.xaml 的交互逻辑
    /// </summary>
    public partial class ManagePage : Page
    {
        public ManagePage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var db = new GymDBService();
            GymDBService gymDBService = new GymDBService();
            gymDBService.Set(baomingCount.Text, playerCount.Text, qianjimingCount.Text);
            ShowMessageInfo("保存成功");
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
