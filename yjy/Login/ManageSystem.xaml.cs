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
using System.Windows.Shapes;

namespace Login
{
    /// <summary>
    /// ManageSystem.xaml 的交互逻辑
    /// </summary>
    public partial class ManageSystem : Window
    {
        public ManageSystem()
        {
            InitializeComponent();
            baomingCount.Items.Add(1);
            baomingCount.Items.Add(2);
            baomingCount.Items.Add(3);
            baomingCount.Items.Add(4);
            baomingCount.Items.Add(5);
            baomingCount.Items.Add(6);
            playerCount.Items.Add(1);
            playerCount.Items.Add(2);
            playerCount.Items.Add(3);
            playerCount.Items.Add(4);
            playerCount.Items.Add(5);
            playerCount.Items.Add(6);
            qianjimingCount.Items.Add(1);
            qianjimingCount.Items.Add(2);
            qianjimingCount.Items.Add(3);
            qianjimingCount.Items.Add(4);
            qianjimingCount.Items.Add(5);
            qianjimingCount.Items.Add(6);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
