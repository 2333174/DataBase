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
using System.Collections.ObjectModel;
using System.Windows.Shapes;
using DB;

namespace Login.Views
{
    /// <summary>
    /// ChooseJudge.xaml 的交互逻辑
    /// </summary>
    public partial class ChooseJudge : Page
    {
        ObservableCollection<string> g_judges { get; set; }
        ObservableCollection<ChooseJudgeGridItem> items { get; set; }
        ChooseJudgeGridItem item { get; set; }
        GymDB db = new GymDB();
        GymDBService gymDBService = new GymDBService();
        private readonly string groupid;
        public ChooseJudge(string project,string group)
        {
            InitializeComponent();
            items = new ObservableCollection<ChooseJudgeGridItem>();
            g_judges = new ObservableCollection<string>();
            //根据所选行初始化datagrid
            item = new ChooseJudgeGridItem(project, group, null, null);
            items.Add(item);
            judgegrid.ItemsSource = items;
            groupid = group;

            //获取全部的裁判信息
            List<Judge> judges = db.judge.ToList();
            //把每一个裁判的名字都添加到combox中供用户选择
            foreach (Judge judge in judges )
            {
                MainJudgeName.Items.Add(judge.Name);
                GroupJudgeName.Items.Add(judge.Name);
            }
        }

        //分裁判添加按钮
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (GroupJudgeName.SelectedItem!=null)
            {
                if (g_judges.Contains(GroupJudgeName.Text))
                {
                    ShowMessageInfo("已存在该教练!");
                    return;
                }
                g_judges.Add(GroupJudgeName.Text);
                items[0].groupJudges= items[0].groupJudges+" "+g_judges.Last(); 
                items[0].mainJudge = MainJudgeName.Text;
                judgegrid.ItemsSource = null;
                judgegrid.ItemsSource = items;
            }
            else
                ShowMessageInfo("未选择");
        }

        //分裁判删除按钮
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            g_judges.Clear();
            items[0].groupJudges = null;
            judgegrid.ItemsSource = null;
            judgegrid.ItemsSource = items;

        }

        //确认按钮
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (MainID.Text != null && item.groupID != null && g_judges != null && GroupJudgeName.SelectedItem != null)
            {
                MatchGroup mainmatch = new MatchGroup(item.groupID, Convert.ToInt32(MainID.Text), 0);
                Client1.ClientSendMsg("主裁判" + "," + Convert.ToInt32(MainID.Text) +","+ groupid);
                gymDBService.Add(mainmatch);
                List<Judge> judges = db.judge.ToList();
                foreach (string groupjudge in g_judges)
                {
                    foreach (Judge judge in judges)
                    {
                        if (judge.Name == GroupJudgeName.SelectedItem.ToString())
                        {
                            MatchGroup match = new MatchGroup(item.groupID, judge.JudgeID, 1);
                            gymDBService.Add(match);
                            Client1.ClientSendMsg(judge.JudgeID + "," + groupid);
                        }
                    }
                }
            }
            else
            {
                ShowMessageInfo("信息未填写完整！");
            }


        }

        private void MainJudgeName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<Judge> judges = db.judge.ToList();
            if(MainJudgeName.SelectedItem!=null)
            {
                foreach (Judge judge in judges)
                {
                    if (judge.Name == MainJudgeName.SelectedItem.ToString())
                        MainID.Text = judge.JudgeID.ToString();
                    if (GroupJudgeName.SelectedItem != null)
                    {
                        if (judge.Name == GroupJudgeName.SelectedItem.ToString())
                            GroupID.Text = judge.JudgeID.ToString();
                    }
                        
                }
            }
   
        }

        private void GroupJudgeName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<Judge> judges = db.judge.ToList();
            foreach (Judge judge in judges)
            {
                if (judge.Name == GroupJudgeName.SelectedItem.ToString())
                    GroupID.Text = judge.JudgeID.ToString();
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
