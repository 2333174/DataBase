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
        //记录是否编排过赛事表的变量
        bool isPreArrange=false;
        bool isFinalArrange = false;
        public ManagePage()
        {
            InitializeComponent();
            List<Manage_DataGridRow> prerows = new List<Manage_DataGridRow>();
            List<Manage_DataGridRow> finalrows = new List<Manage_DataGridRow>();
            GymDBService gymDBService = new GymDBService();
            List<PersonalResult> personalResults = gymDBService.GetPersonalResults();
            foreach(PersonalResult p in personalResults)
            {
                if(p.Role=='0')
                {
                    //获得Athlete
                    Athlete athlete = gymDBService.GetAthleteByID(p.AthleteID);
                    //获得运动员姓名
                    string athName = athlete.Name;
                    //获得小组编号
                    string groupID = p.GroupID;
                    //根据小组编号得到项目名称
                    string pName = GetPName(groupID, 0);
                    //获得Athlete所在的Team
                    Team team = gymDBService.GetTeamByTID((int)athlete.TID);
                    //获得队名
                    string tName = team.TName;
                    Manage_DataGridRow manage_DataGridRow = new Manage_DataGridRow(pName, groupID, tName, athName);
                    prerows.Add(manage_DataGridRow);
                }else{
                    //获得Athlete
                    Athlete athlete = gymDBService.GetAthleteByID(p.AthleteID);
                    //获得运动员姓名
                    string athName = athlete.Name;
                    //获得小组编号
                    string groupID = p.GroupID;
                    //根据小组编号得到项目名称
                    string pName = GetPName(groupID, 1);
                    //获得Athlete所在的Team
                    Team team = gymDBService.GetTeamByTID((int)athlete.TID);
                    //获得队名
                    string tName = team.TName;
                    Manage_DataGridRow manage_DataGridRow = new Manage_DataGridRow(pName, groupID, tName, athName);
                    finalrows.Add(manage_DataGridRow);
                }
            }
            preMatchGrid.ItemsSource = prerows;
            finalMatchGrid.ItemsSource = finalrows;
        }




        private async void ShowMessageInfo(string message)
        {
            MessageDialog samMessageDialog = new MessageDialog
            {
                Message = { Text = message }
            };
            await MaterialDesignThemes.Wpf.DialogHost.Show(samMessageDialog);
        }
        

        //根据组号获得预赛项目名称 年龄+赛事+决预赛小组号
        public string GetPName(string groupID,int flag)
        {
            //01:男子单杠；02：男子双杠；03：男子吊环；04男子跳马；05：男子自由体操；06：男子鞍马；07：男子蹦床
            //08：女子跳马；09：女子高低杠；10：女子平衡木；11：女子自由体操；12：女子蹦床
            //0:7-8岁；1：9-10岁；2：11-12岁
            string age=null;
            char a = groupID[0];
            string project = null;
            //flag为0选择预赛信息，1选择决赛信息
            if (flag == 0 && groupID[3] == '0')
            {
                switch (a)
                {
                    case '0':
                        age = "7-8岁";
                        break;
                    case '1':
                        age = "9-10岁";
                        break;
                    case '2':
                        age = "11-12岁";
                        break;
                }
                char b = groupID[1];
                char c = groupID[2];
                if (b == '0' && c == '1')
                {
                    project = "男子单杠";
                }
                else if (b == '0' && c == '2')
                {
                    project = "男子双杠";
                }
                else if (b == '0' && c == '3')
                {
                    project = "男子吊环";
                }
                else if (b == '0' && c == '4')
                {
                    project = "男子跳马";
                }
                else if (b == '0' && c == '5')
                {
                    project = "男子自由体操";
                }
                else if (b == '0' && c == '6')
                {
                    project = "男子鞍马";
                }
                else if (b == '0' && c == '7')
                {
                    project = "男子蹦床";
                }
                else if (b == '0' && c == '8')
                {
                    project = "女子跳马";
                }
                else if (b == '0' && c == '9')
                {
                    project = "女子高低杠";
                }
                else if (b == '1' && c == '0')
                {
                    project = "女子平衡木";
                }
                else if (b == '1' && c == '1')
                {
                    project = "女子自由体操";
                }
                else if (b == '1' && c == '2')
                {
                    project = "女子蹦床";
                }
            }
            else if (flag == 1 && groupID[3] == '1')
            {
                switch (a)
                {
                    case '0':
                        age = "7-8岁";
                        break;
                    case '1':
                        age = "9-10岁";
                        break;
                    case '2':
                        age = "11-12岁";
                        break;
                }
                char b = groupID[1];
                char c = groupID[2];
                if (b == '0' && c == '1')
                {
                    project = "男子单杠";
                }
                else if (b == '0' && c == '2')
                {
                    project = "男子双杠";
                }
                else if (b == '0' && c == '3')
                {
                    project = "男子吊环";
                }
                else if (b == '0' && c == '4')
                {
                    project = "男子跳马";
                }
                else if (b == '0' && c == '5')
                {
                    project = "男子自由体操";
                }
                else if (b == '0' && c == '6')
                {
                    project = "男子鞍马";
                }
                else if (b == '0' && c == '7')
                {
                    project = "男子蹦床";
                }
                else if (b == '0' && c == '8')
                {
                    project = "女子跳马";
                }
                else if (b == '0' && c == '9')
                {
                    project = "女子高低杠";
                }
                else if (b == '1' && c == '0')
                {
                    project = "女子平衡木";
                }
                else if (b == '1' && c == '1')
                {
                    project = "女子自由体操";
                }
                else if (b == '1' && c == '2')
                {
                    project = "女子蹦床";
                }

            }
            return age + project;
        }
        //预赛添加分裁判的按钮
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            
        }


        //预赛-添加裁判按钮
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            //获取当前选中行
            var row = (Manage_DataGridRow)preMatchGrid.SelectedItem;
            string project = row.project;
            string group = row.groupID;
            ManageSystem.Content = new Frame()
            { Content = new ChooseJudge(project,group) };
        }

        //生成预赛赛事表
        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
           if(isPreArrange==true)
            {
                ShowMessageInfo("不可重复生成预赛赛事表！");
            }
            GymDBService gymDBService = new GymDBService();
            //存放预赛表信息的List
            List<Manage_DataGridRow> preMatch = new List<Manage_DataGridRow>();
            //自动分组
            gymDBService.Grouping(8);
            //List<PersonalResult> personalResults = gymDBService.GetPersonalResults();
            //foreach(PersonalResult p in personalResults)
            //{
            //    //根据sportsevent自动编排
            //    //gymDBService.AutoArrangeGroup(p.SportsEvent, 12 , 0);
                
            //}
            //获得所有的小组
            List<MatchGroup> groups = gymDBService.GetMatchGroups();
            //根据GroupID获得AthleteID
            int i = 0;
            //创建存放每个组的personalResult的数组
            List<PersonalResult>[] personalResult = new List<PersonalResult>[groups.Count];
            foreach (MatchGroup group in groups)
            {
                personalResult[i] = gymDBService.GetPersonalResultsByGroupID(group.GroupID);
                //对于每个组
                foreach (PersonalResult p in personalResult[i])
                {
                    //获得Athlete
                    Athlete athlete = gymDBService.GetAthleteByID(p.AthleteID);
                    //获得运动员姓名
                    string athName = athlete.Name;
                    //获得小组编号
                    string groupID = p.GroupID;
                    //根据小组编号得到项目名称
                    string pName = GetPName(groupID,0);
                    //获得Athlete所在的Team
                    Team team = gymDBService.GetTeamByTID((int)athlete.TID);
                    //获得队名
                    string tName = team.TName;
                    Manage_DataGridRow manage_DataGridRow = new Manage_DataGridRow(pName, groupID, tName, athName);
                    preMatch.Add(manage_DataGridRow);
                }
                i++;
            }

            //数据绑定
            preMatchGrid.ItemsSource = preMatch;
            isPreArrange = true;
        }

        //生成决赛赛事表
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (isFinalArrange == true)
            {
                ShowMessageInfo("不可重复生成决赛赛事表！");
            }
            GymDBService gymDBService = new GymDBService();
            //存放预赛表信息的List
            List<Manage_DataGridRow> finalMatch = new List<Manage_DataGridRow>();
            //自动分组
            gymDBService.Grouping(2);
            
            //获得所有的小组
            List<MatchGroup> groups = gymDBService.GetMatchGroups();
            //根据GroupID获得AthleteID
            int i = 0;
            //创建存放每个组的personalResult的数组
            List<PersonalResult>[] personalResult = new List<PersonalResult>[groups.Count];
            foreach (MatchGroup group in groups)
            {
                personalResult[i] = gymDBService.GetPersonalResultsByGroupID(group.GroupID);
                //对于每个组
                foreach (PersonalResult p in personalResult[i])
                {
                    //获得Athlete
                    Athlete athlete = gymDBService.GetAthleteByID(p.AthleteID);
                    //获得运动员姓名
                    string athName = athlete.Name;
                    //获得小组编号
                    string groupID = p.GroupID;
                    //根据小组编号得到项目名称
                    string pName = GetPName(groupID,1);
                    //获得Athlete所在的Team
                    Team team = gymDBService.GetTeamByTID((int)athlete.TID);
                    //获得队名
                    string tName = team.TName;
                    Manage_DataGridRow manage_DataGridRow = new Manage_DataGridRow(pName, groupID, tName, athName);
                    finalMatch.Add(manage_DataGridRow);
                }
                i++;
            }

            //数据绑定
            finalMatchGrid.ItemsSource = finalMatch;
        }

        //决赛-添加裁判
        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            //获取当前选中行
            var row = (Manage_DataGridRow)preMatchGrid.SelectedItem;
            string project = row.project;
            string group = row.groupID;
            ManageSystem.Content = new Frame()
            { Content = new ChooseJudge(project, group) };
        }


        //保存
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var db = new GymDBService();
            GymDBService gymDBService = new GymDBService();
            gymDBService.Set(baomingCount.Text, playerCount.Text, qianjimingCount.Text);
            ShowMessageInfo("保存成功");
        }
    }
}
