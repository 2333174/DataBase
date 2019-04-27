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
        GymDB db = new GymDB();
        GymDBService gymDBService = new GymDBService();

        public ChooseJudge(String project,string group)
        {
            InitializeComponent();
            
            //根据所选行初始化datagrid
            item = new ChooseJudgeGridItem(project, group, null, g_judges);
            items.Add(item);
            judgegrid.ItemsSource = items;            
            //获取全部的裁判信息
            List<Judge> judges = db.judge.ToList();
            //把每一个裁判的名字都添加到combox中供用户选择
            foreach (Judge judge in judges )
            {
                MainJudgeName.Items.Add(judge.Name);
                GroupJudgeName.Items.Add(judge.Name);
            }
        }

        //获取选中的主裁判账号
        private void MainJudgeName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<Judge> judges = db.judge.ToList();
            foreach (Judge judge in judges)
            {
                if (judge.Name == MainJudgeName.SelectedItem.ToString())
                    MainID.Text = judge.JudgeID.ToString();
            }
        }

        //获取选中的分判账号
        private void GroupID_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<Judge> judges = db.judge.ToList();
            foreach (Judge judge in judges)
            {               
                if (judge.Name == GroupJudgeName.SelectedItem.ToString())
                    GroupID.Text = judge.JudgeID.ToString();
            }
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
            g_judges.Clear();
            items[0].groupJudges = null;
        }

        //确认按钮传到数据库
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if(MainID.Text!=null && item.groupID!=null&& g_judges!=null&& GroupJudgeName.SelectedItem!=null)
            {
                MatchGroup mainmatch = new MatchGroup(item.groupID, Convert.ToInt32(MainID.Text), 0);
                gymDBService.Add(mainmatch);
                List<Judge> judges = db.judge.ToList();
                foreach (String groupjudge in g_judges)
                {
                    foreach (Judge judge in judges)
                    {
                        if (judge.Name == GroupJudgeName.SelectedItem.ToString())
                        {
                            MatchGroup match = new MatchGroup(item.groupID, judge.JudgeID, 1);
                            gymDBService.Add(match);
                        }
                    }
                }
                MessageBox.Show("信息提交完成！");
            }
            else
            {
                MessageBox.Show("信息未填写完整！");
            }
        }     
    }
}
