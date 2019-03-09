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
            manCount.Items.Add(1);
            manCount.Items.Add(2);
            manCount.Items.Add(3);
            manCount.Items.Add(4);
            manCount.Items.Add(5);
            manCount.Items.Add(6);
            womanCount.Items.Add(1);
            womanCount.Items.Add(2);
            womanCount.Items.Add(3);
            womanCount.Items.Add(4);
            womanCount.Items.Add(5);
            womanCount.Items.Add(6);
            playCount.Items.Add(1);
            playCount.Items.Add(2);
            playCount.Items.Add(3);
            playCount.Items.Add(4);
            playCount.Items.Add(5);
            playCount.Items.Add(6);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
