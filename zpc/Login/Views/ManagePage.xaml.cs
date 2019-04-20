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

        //决赛添加总裁判的按钮
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        //根据组号获得项目名称 年龄+赛事+小组号
        public string GetPName(string groupID)
        {
            //01:男子单杠；02：男子双杠；03：男子吊环；04男子跳马；05：男子自由体操；06：男子鞍马；07：男子蹦床
            //08：女子跳马；09：女子高低杠；10：女子平衡木；11：女子自由体操；12：女子蹦床
            //0:7-8岁；1：9-10岁；2：11-12岁
            string age=null;
            char a = groupID[0];
            switch(a)
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
            string project=null;
            if (b == '0' && c == '1')
            {
                project = "男子单杠";
            } else if (b == '0' && c == '2')
            {
                project = "男子双杠";
            } else if (b == '0' && c == '3')
            {
                project = "男子吊环";
            } else if (b == '0' && c == '4')
            {
                project = "男子跳马";
            } else if (b == '0' && c == '5')
            {
                project = "男子自由体操";
            }else if (b == '0' && c == '6')
            {
                project = "男子鞍马";
            }else if (b == '0' && c == '7')
            {
                project = "男子蹦床";
            }else if (b == '0' && c == '8')
            {
                project = "女子跳马";
            }else if (b == '0' && c == '9')
            {
                project = "女子高低杠";
            }else if (b == '1' && c == '0')
            {
                project = "女子平衡木";
            }else if (b == '1' && c == '1')
            {
                project = "女子自由体操";
            }else if (b == '1' && c == '2')
            {
                project = "女子蹦床";
            }
            return age + project;
        }
        //预赛添加分裁判的按钮
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            
        }

        //点击生成预赛赛事表的按钮
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            //设计算法自动编排预赛赛事
            //存放预赛表信息的List
            List<Manage_DataGridRow> preMatch = new List<Manage_DataGridRow>();

            GymDBService gymDBService = new GymDBService();
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
                    string pName = GetPName(groupID);
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
        }

        //添加裁判按钮
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            //获取当前选中行
            var row = (Manage_DataGridRow)preMatchGrid.SelectedItem;
            string project = row.project;
            string group = row.groupID;
            ManageSystem.Content = new Frame()
            { Content = new ChooseJudge(project,group) };
        }
    }
}
