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
    /// ChosePage.xaml 的交互逻辑
    /// </summary>
    public partial class ChosePage : Page
    {
        public ChosePage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int select;
            if (radio_sign.IsChecked == true)
                select = 0;
            else if (radio_manage.IsChecked == true)
                select = 1;
            else if (radio_grade.IsChecked == true)
                select = 2;
            else
            {
                ShowMessageInfo("Your choice is incorrect!");
                return;
            }
            ChangePage.Content = new Frame()
            { Content = new LoginPage(select) };
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
