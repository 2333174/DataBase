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
    /// ChooseJudge.xaml 的交互逻辑
    /// </summary>
    public partial class ChooseJudge : Page
    {
        List<string> g_judges;
        List<ChooseJudgeGridItem> items;
        ChooseJudgeGridItem item;
        public ChooseJudge(String project,string group)
        {
            InitializeComponent();
            
            //根据所选行初始化datagrid
            item = new ChooseJudgeGridItem(project, group, null, g_judges);
            items.Add(item);
            judgegrid.ItemsSource = items;
            GymDB db = new GymDB();
            GymDBService gymDBService = new GymDBService();
            //获取全部的裁判信息
            List<Judge> judges = db.judge.ToList();
            //把每一个裁判的名字都添加到combox中供用户选择
            foreach (Judge judge in judges )
            {
                MainJudgeName.Items.Add(judge.Name);
                GroupJudgeName.Items.Add(judge.Name);
            }
        }


        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        //分裁判添加按钮
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (GroupJudgeName.Text != null)
            {
                g_judges.Add(GroupJudgeName.Text);
                items[0].groupJudges = g_judges;
            }
            else
                MessageBox.Show("未选择");
        }

        //分裁判删除按钮
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }

        //确认按钮
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {

        }
    }
}
