using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
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
        bool isPreArrange = false;
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
            List<PersonalResult> personalResults = gymDBService.GetPersonalResults();
            foreach (PersonalResult p in personalResults)
            {
                if (p.Role == 0 && p.SportsEvent[3] == '0')
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
                }
                else if (p.Role == 1 && p.SportsEvent[3] == '1')
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




        private async void ShowMessageInfo(string message, DialogHost dialog)
        {
            MessageDialog samMessageDialog = new MessageDialog
            {
                Message = { Text = message }
            };
            await dialog.ShowDialog(samMessageDialog);
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
                ShowMessageInfo("未选中行！", prehost);
                return;
            }
            else
            {

                if (row.groupID == null)
                {
                    ShowMessageInfo("请先生成赛事表再添加裁判！", prehost);
                    return;
                }
                else
                {
                    List<MatchGroup> mgs = gymDBService.GetMatchGroupsByGroupID(row.groupID);
                    foreach (MatchGroup gp in mgs)
                    {
                        if (gp.JudgeID != null)
                        {
                            ShowMessageInfo("已经添加过裁判信息！", prehost);
                            return;
                        }
                    }
                }
                project = row.project;
                group = row.groupID;
            }
            ManageSystem.Content = new Frame()
            { Content = new ChooseJudge(project, group) };
        }

        //生成预赛赛事表
        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            if (isPreArrange == true)
            {
                ShowMessageInfo("不可重复生成预赛赛事表！", prehost);
                return;
            }
            GymDBService gymDBService = new GymDBService();
            //存放预赛表信息的List
            List<Manage_DataGridRow> preMatch = new List<Manage_DataGridRow>();
            //自动分组
            gymDBService.Grouping(3);
            //获得所有的小组
            List<MatchGroup> groups = gymDBService.GetMatchGroups();
            //获得预赛小组
            SortedSet<string> groupids = new SortedSet<string>();
            foreach (MatchGroup group in groups)
            {
                if (group.GroupID[3] == '0')
                    groupids.Add(group.GroupID);

            }
            //创建存放每个组的personalResult的数组
            List<PersonalResult>[] personalResult = new List<PersonalResult>[groupids.Count];
            int i = 0;
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
                    preMatch.Add(manage_DataGridRow);
                }
                i++;
            }

            //数据绑定
            preMatchGrid.ItemsSource = preMatch;
            isPreArrange = true;
        }

        //显示决赛赛事表
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            GymDBService gymDBService = new GymDBService();
            //存放决赛表信息的List
            List<Manage_DataGridRow> finalMatch = new List<Manage_DataGridRow>();
            //获得所有的小组
            List<MatchGroup> groups = gymDBService.GetMatchGroups();
            //获得决赛小组
            SortedSet<string> groupids = new SortedSet<string>();
            foreach (MatchGroup group in groups)
            {
                if (group.GroupID.Substring(3, 1) == "1")
                    groupids.Add(group.GroupID);

            }
            //创建存放每个组的personalResult的数组
            List<PersonalResult>[] personalResult = new List<PersonalResult>[groupids.Count];
            int i = 0;
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
            string project = null;
            string group = null;
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
            ShowMessageInfo("保存成功", sethost);
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
                        userRole = 0;
                        break;
                    default:
                        userRole = 2;
                        break;
                }
                if (userRole == 0)
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
                    else if (login.TName == samMessageDialog.Name.Text && login.Role == 0 && samMessageDialog.AccountRole.Text == "代表队") { ShowMessageInfo("代表队名重复", addFail); isNotExist = false; }
                    else if (login.JudgeID.ToString() == samMessageDialog.Name.SelectedValue.ToString() && login.Role == 2 && samMessageDialog.AccountRole.Text == "教练") { ShowMessageInfo("教练名重复", addFail); isNotExist = false; }
                }
                if (isNotExist)
                {
                    account = new Account(samMessageDialog.UserName.Text, samMessageDialog.Password.Password, samMessageDialog.AccountRole.Text, samMessageDialog.Name.SelectedValue.ToString());
                    accounts.Add(account);
                    accountGrid.ItemsSource = null;
                    accountGrid.ItemsSource = accounts;
                }
            }
        }
    }
}
