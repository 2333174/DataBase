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
using Login.Models;
using MaterialDesignThemes.Wpf;

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
        bool isAddAccount = false;
        Account account;
        public List<Account> accounts;
        GymDBService gymDBService;
        List<DB.Login> logins;
        public ManagePage()
        {
            InitializeComponent();
            accounts = new List<Account>();
            List<Manage_DataGridRow> prerows = new List<Manage_DataGridRow>();
            List<Manage_DataGridRow> finalrows = new List<Manage_DataGridRow>();
            gymDBService = new GymDBService();
            GymDB db = new GymDB();
            logins = db.login.ToList();
           List <PersonalResult> personalResults = gymDBService.GetPersonalResults();
            foreach(PersonalResult p in personalResults)
            {
                if(p.Role=='0'&&p.SportsEvent[3]=='0')
                {
                    //获得Athlete
                    Athlete athlete = gymDBService.GetAthleteByID(p.AthleteID);
                    //获得运动员姓名
                    string athName = athlete.Name;
                    //获得小组编号
                    string groupID = p.GroupID;
                    //得到项目名称
                    string pName = gymDBService.GetFullSportName(p.SportsEvent);
                    //获得Athlete所在的Team
                    Team team = gymDBService.GetTeamByTID((int)athlete.TID);
                    //获得队名
                    string tName = team.TName;
                    Manage_DataGridRow manage_DataGridRow = new Manage_DataGridRow(pName, groupID, tName, athName);
                    prerows.Add(manage_DataGridRow);
                }else if(p.Role == '1' && p.SportsEvent[3] == '1')
                {
                    //获得Athlete
                    Athlete athlete = gymDBService.GetAthleteByID(p.AthleteID);
                    //获得运动员姓名
                    string athName = athlete.Name;
                    //获得小组编号
                    string groupID = p.GroupID;
                    //得到项目名称
                    string pName = gymDBService.GetFullSportName(p.SportsEvent);
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
            accountGrid.ItemsSource = accounts;
        }




        private async void ShowMessageInfo(string message,DialogHost dialog)
        {
            MessageDialog samMessageDialog = new MessageDialog
            {
                Message = { Text = message }
            };
            await dialog.ShowDialog(samMessageDialog);
        }
        

        //根据组号获得预赛项目名称 年龄+赛事+决预赛小组号
        //public string GetPName(string groupID,int flag)
        //{
        //    //01:男子单杠；02：男子双杠；03：男子吊环；04男子跳马；05：男子自由体操；06：男子鞍马；07：男子蹦床
        //    //08：女子跳马；09：女子高低杠；10：女子平衡木；11：女子自由体操；12：女子蹦床
        //    //0:7-8岁；1：9-10岁；2：11-12岁
        //    string age=null;
        //    string project = null;
        //    char a = groupID[0];
        //    //flag为0选择预赛信息，1选择决赛信息
        //    if (flag == 0 && groupID[3] == '0')
        //    {
        //        switch (a)
        //        {
        //            case '0':
        //                age = "7-8岁";
        //                break;
        //            case '1':
        //                age = "9-10岁";
        //                break;
        //            case '2':
        //                age = "11-12岁";
        //                break;
        //        }
        //        char b = groupID[1];
        //        char c = groupID[2];
        //        if (b == '0' && c == '1')
        //        {
        //            project = "男子单杠";
        //        }
        //        else if (b == '0' && c == '2')
        //        {
        //            project = "男子双杠";
        //        }
        //        else if (b == '0' && c == '3')
        //        {
        //            project = "男子吊环";
        //        }
        //        else if (b == '0' && c == '4')
        //        {
        //            project = "男子跳马";
        //        }
        //        else if (b == '0' && c == '5')
        //        {
        //            project = "男子自由体操";
        //        }
        //        else if (b == '0' && c == '6')
        //        {
        //            project = "男子鞍马";
        //        }
        //        else if (b == '0' && c == '7')
        //        {
        //            project = "男子蹦床";
        //        }
        //        else if (b == '0' && c == '8')
        //        {
        //            project = "女子跳马";
        //        }
        //        else if (b == '0' && c == '9')
        //        {
        //            project = "女子高低杠";
        //        }
        //        else if (b == '1' && c == '0')
        //        {
        //            project = "女子平衡木";
        //        }
        //        else if (b == '1' && c == '1')
        //        {
        //            project = "女子自由体操";
        //        }
        //        else if (b == '1' && c == '2')
        //        {
        //            project = "女子蹦床";
        //        }
        //    }
        //    else if (flag == 1 && groupID[3] == '1')
        //    {
        //        switch (a)
        //        {
        //            case '0':
        //                age = "7-8岁";
        //                break;
        //            case '1':
        //                age = "9-10岁";
        //                break;
        //            case '2':
        //                age = "11-12岁";
        //                break;
        //        }
        //        char b = groupID[1];
        //        char c = groupID[2];
        //        if (b == '0' && c == '1')
        //        {
        //            project = "男子单杠";
        //        }
        //        else if (b == '0' && c == '2')
        //        {
        //            project = "男子双杠";
        //        }
        //        else if (b == '0' && c == '3')
        //        {
        //            project = "男子吊环";
        //        }
        //        else if (b == '0' && c == '4')
        //        {
        //            project = "男子跳马";
        //        }
        //        else if (b == '0' && c == '5')
        //        {
        //            project = "男子自由体操";
        //        }
        //        else if (b == '0' && c == '6')
        //        {
        //            project = "男子鞍马";
        //        }
        //        else if (b == '0' && c == '7')
        //        {
        //            project = "男子蹦床";
        //        }
        //        else if (b == '0' && c == '8')
        //        {
        //            project = "女子跳马";
        //        }
        //        else if (b == '0' && c == '9')
        //        {
        //            project = "女子高低杠";
        //        }
        //        else if (b == '1' && c == '0')
        //        {
        //            project = "女子平衡木";
        //        }
        //        else if (b == '1' && c == '1')
        //        {
        //            project = "女子自由体操";
        //        }
        //        else if (b == '1' && c == '2')
        //        {
        //            project = "女子蹦床";
        //        }

        //    }
        //    return age + project;
        //}
        //预赛添加分裁判的按钮
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            
        }


        //预赛-添加裁判按钮
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            string project = null;
            string group = null;
            //获取当前选中行
            var row = preMatchGrid.SelectedItem as Manage_DataGridRow;
            if (row == null)
            {
                ShowMessageInfo("未选中行！",prehost);
            }
            else
            {
                project = row.project;
                group = row.groupID;
            }
            ManageSystem.Content = new Frame()
            { Content = new ChooseJudge(project, group) };
        }

        //生成预赛赛事表
        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
           if(isPreArrange==true)
            {
                ShowMessageInfo("不可重复生成预赛赛事表！",prehost);
            }
            GymDBService gymDBService = new GymDBService();
            //存放预赛表信息的List
            List<Manage_DataGridRow> preMatch = new List<Manage_DataGridRow>();
            //自动分组
            gymDBService.Grouping(2);
            //获得所有的小组
            List<MatchGroup> groups = gymDBService.GetMatchGroups();
            //根据GroupID获得AthleteID
            int i = 0;
            //创建存放每个组的personalResult的数组
            List<PersonalResult>[] personalResult = new List<PersonalResult>[groups.Count];
            SortedSet<string> groupids = new SortedSet<string>(); 
            foreach (MatchGroup group in groups)
            {
                groupids.Add(group.GroupID);
                
            }
            foreach(string groupid in groupids)
            {
                personalResult[i] = gymDBService.GetPersonalResultsByGroupID(groupid);
                //对于每个组
                foreach (PersonalResult p in personalResult[i])
                {
                    //获得Athlete
                    Athlete athlete = gymDBService.GetAthleteByID(p.AthleteID);
                    //获得运动员姓名
                    string athName = athlete.Name;
                    //获得小组编号
                    string groupID = p.GroupID;
                    //得到项目名称
                    string pName = gymDBService.GetFullSportName(p.SportsEvent);
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
                ShowMessageInfo("不可重复生成决赛赛事表！",finalhost);
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
            SortedSet<string> groupids = new SortedSet<string>();
            foreach (MatchGroup group in groups)
            {
                groupids.Add(group.GroupID);

            }
            foreach (string groupid in groupids)
            {
                personalResult[i] = gymDBService.GetPersonalResultsByGroupID(groupid);
                //对于每个组
                foreach (PersonalResult p in personalResult[i])
                {
                    //获得Athlete
                    Athlete athlete = gymDBService.GetAthleteByID(p.AthleteID);
                    //获得运动员姓名
                    string athName = athlete.Name;
                    //获得小组编号
                    string groupID = p.GroupID;
                    //得到项目名称
                    string pName = gymDBService.GetFullSportName(p.SportsEvent);
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
            string project=null;
            string group=null;
            //获取当前选中行
            var row = finalMatchGrid.SelectedItem as Manage_DataGridRow;
            if (row == null)
            {
                ShowMessageInfo("未选中行！", finalhost);
            }
            else
            {
                project = row.project;
                group = row.groupID;
            }
            ManageSystem.Content = new Frame()
            { Content = new ChooseJudge(project, group) };
        }


        //保存
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var db = new GymDBService();
            GymDBService gymDBService = new GymDBService();
            gymDBService.Set(baomingCount.Text, playerCount.Text, qianjimingCount.Text);
            ShowMessageInfo("保存成功",sethost);
        }

        private void deleteGrid_Click(object sender, RoutedEventArgs e)
        {
            accounts.Remove((Account)accountGrid.SelectedItem);
            accountGrid.ItemsSource = null;
            accountGrid.ItemsSource = accounts;
        }

        private void addGrid_Click(object sender, RoutedEventArgs e)
        {
            ShowAddAccount();
        }
 
        private void addAccount_Click(object sender, RoutedEventArgs e)
        {
            foreach (var acc in accounts)
            {
                int userRole;
                switch (acc.accountRole)
                {
                    case "代表队":
                        userRole = 1;
                        break;
                    default:
                        userRole = 2;
                        break;
                }
                if (userRole == 1)
                {
                    gymDBService.Add(new Team(acc.name));
                    gymDBService.Add(new DB.Login(acc.userName, acc.password, userRole, acc.name));
                }
                else if (userRole == 2)
                {
                    gymDBService.Add(new DB.Login(acc.userName, acc.password, userRole, int.Parse(acc.name)));
                }
            }
            ShowMessageInfo("添加成功", add);
            accounts.Clear();
            accountGrid.ItemsSource = null;
            accountGrid.ItemsSource = accounts;

        }
        private async void ShowAddAccount()
        {
            AddAccountDialog samMessageDialog = new AddAccountDialog
            {
            };
            isAddAccount = Equals(await add.ShowDialog(samMessageDialog), true);
            if (isAddAccount)
            {
                bool isNotExist = true;
                foreach (var login in logins)
                {
                    if (login.UName == samMessageDialog.UserName.Text) { ShowMessageInfo("用户名重复", addFail); isNotExist = false; }
                    else if (login.TName == samMessageDialog.Name.Text && login.Role == 1 && samMessageDialog.AccountRole.Text == "代表队") { ShowMessageInfo("代表队名重复", addFail); isNotExist = false; }
                    else if (login.JudgeID.ToString() == samMessageDialog.Name.Text && login.Role == 2 && samMessageDialog.AccountRole.Text == "教练") { ShowMessageInfo("教练名重复", addFail); isNotExist = false; }
                }
                if (isNotExist)
                {
                    account = new Account(samMessageDialog.UserName.Text, samMessageDialog.Password.Text, samMessageDialog.AccountRole.Text, samMessageDialog.Name.Text);
                    accounts.Add(account);
                    accountGrid.ItemsSource = null;
                    accountGrid.ItemsSource = accounts;
                }
            }
        }
    }
}
