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

namespace Login.Views
{
    /// <summary>
    /// AddAthleteDialog.xaml 的交互逻辑
    /// </summary>
    public partial class AddAthleteDialog : UserControl
    {
        public AddAthleteDialog()
        {
            InitializeComponent();
        }

        private void ComboBoxItem_Selected(object sender, RoutedEventArgs e)
        {
            MenEvent.Visibility = Visibility.Visible;
            WomenEvent.Visibility = Visibility.Hidden;
            warning.Visibility = Visibility.Hidden;
            SportEvent.Items.Clear();
            SportEvent.Items.Add("单杠");
            SportEvent.Items.Add("双杠");
            SportEvent.Items.Add("吊环");
            SportEvent.Items.Add("跳马");
            SportEvent.Items.Add("自由体操");
            SportEvent.Items.Add("鞍马");
            SportEvent.Items.Add("蹦床");
        }

        private void ComboBoxItem_Selected_1(object sender, RoutedEventArgs e)
        {
            WomenEvent.Visibility = Visibility.Visible;
            MenEvent.Visibility = Visibility.Hidden;
            warning.Visibility = Visibility.Hidden;
            SportEvent.Items.Clear();
            SportEvent.Items.Add("跳马");
            SportEvent.Items.Add("高低杠");
            SportEvent.Items.Add("平衡木");
            SportEvent.Items.Add("自由体操");
            SportEvent.Items.Add("蹦床");
        }
    }
}
