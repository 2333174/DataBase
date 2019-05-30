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
    /// AllPrePage.xaml 的交互逻辑
    /// </summary>
    public partial class AllPrePage : Page
    {
        GymDB dB = new GymDB();
        GymDBService gymDBService = new GymDBService();
        List<ShowGradeGridItem> team_items = new List<ShowGradeGridItem>();
        //记录决赛
        List<PersonalResult> f_prs = new List<PersonalResult>();
        List<ShowGradeGridItem> f_items = new List<ShowGradeGridItem>();
        //events:初赛所有赛事
        SortedSet<string> events = new SortedSet<string>();
        //个人全能成绩
        List<ShowGradeGridItem> p_items = new List<ShowGradeGridItem>();
        public AllPrePage()
        {
            InitializeComponent();
            AllEvents();
            //给teamgrid数据的函数
            TeamGrid();
            //给teamgrid绑定数据源
            teamgrid.ItemsSource = team_items;
            ShowFinalSuq();
            //给rankgrid绑定数据源
            rankgrid.ItemsSource = f_items;
            //给atheltegrid数据的函数
            //AtheleteGrid();
            //给atheltegrid绑定数据源
            //athletegrid.ItemsSource = p_items;
        }
        public void AllEvents()
        {
            //先记录所有的赛事
            List<PersonalResult> personalResults = new List<PersonalResult>();
            personalResults = gymDBService.GetPersonalResults();
            foreach (PersonalResult p in personalResults)
            {
                events.Add(p.SportsEvent);
            }
        }
        public void TeamGrid()
        {
            //num:团体比赛计算前几名的成绩
            int num = gymDBService.GetSettingPthree();
            //取前num名计算团体成绩
            foreach (string a in events)
            {
                string game = gymDBService.GetFullSportName(a);
                List<Team> teams = gymDBService.GetAllTeams();
                foreach (Team t in teams)
                {
                    //查询tid和sportsevent是否已经在数据库的teamresult表中 如果在 不做操作
                    if (!gymDBService.CheckDuplicateTeamResult(t.TID, a))
                        continue;
                    short grade = 0;
                    //该队的所有运动员
                    ICollection<Athlete> athletes = gymDBService.GetAthletesByTID(t.TID);
                    //存放该队该赛事的PersonalResults的list
                    List<PersonalResult> prs1 = new List<PersonalResult>();
                    foreach (Athlete ath in athletes)
                    {
                        try
                        {
                            PersonalResult result = gymDBService.GetPersonalResultsBySportEventAndRoleAndAID(a, 0, ath.IDNumber);
                            if (result == null)
                                continue;
                            else
                                prs1.Add(result);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }

                    }
                    if (prs1.Count == 0)
                        continue;
                    //给这些运动员的成绩排序
                    gymDBService.Ranking(prs1);
                    //计算前n名运动员的成绩
                    if (num <= prs1.Count)
                    {
                        for (int i = 0; i < num; i++)
                        {
                            grade += (short)prs1.Where(p => p.Ranking == (i + 1)).Single().Grade;
                        }
                    }
                    else
                    {
                        for (int i = 0; i < prs1.Count; i++)
                        {
                            grade += (short)prs1.ElementAt(i).Grade;
                        }
                    }
                    //int TID,string Event,short? Grade,short? Ranking,int TRid,Team team
                    //向数据库添加teamresult
                    TeamResult teamResult = new TeamResult(t.TID, a, (short)grade, t);
                    gymDBService.Add(teamResult);
                }
                gymDBService.Ranking(gymDBService.GetTeamResultsByEvent(a));
                foreach (Team t in teams)
                {
                    try
                    {
                        TeamResult tr = dB.teamresult.Where(p => p.TID.Equals(t.TID) && p.Event.Equals(a)).Single();
                        tr.Event = game;
                        Team team = gymDBService.GetTeamByTID(tr.TID);
                        ShowGradeGridItem i = new ShowGradeGridItem(game, tr.Ranking, team.TName, (int)tr.Grade);
                        //向数据源添加Teamresult
                        team_items.Add(i);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }

                }

            }
        }
        //显示决赛次序
        public void ShowFinalSuq()
        {
            foreach (string s in events)
            {
                string game = gymDBService.GetFullSportName(s);
                List<PersonalResult> personalResults = gymDBService.GetPersonalResultsBySportEventAndRole(s, 1).ToList();
                foreach (PersonalResult pr in personalResults)
                {
                    ShowGradeGridItem item = new ShowGradeGridItem(game, pr.athlete.Name, pr.GroupID, (sbyte)pr.Suq);
                    f_items.Add(item);
                }
            }
        }
        //计算单项决赛次序 当一个项目运动员数大于等于10时，一半的运动员晋级决赛
        //7、8或9个运动员晋级五个，四、五或六个运动员晋级4个，三个及三个一下不进行决赛
        //public void ComputeFinalSuq()
        //{
        //    foreach (string s in events)
        //    {
        //        string game = gymDBService.GetFullSportName(s);
        //        string sportsevent = s.Substring(0,3) + "1";
        //        List<PersonalResult> personalResults = gymDBService.GetPersonalResultsBySportEventAndRole(s, 0).ToList();
        //        int athnum = personalResults.Count;
        //        gymDBService.Ranking(personalResults);
        //        if (athnum >= 10)
        //        {
        //            for (int i = 1; i <= athnum / 2; i++)
        //            {
        //                PersonalResult pr = dB.personalresult.Where(p => p.Ranking == i).Single();
        //                PersonalResult pr1 = new PersonalResult(pr.AthleteID, sportsevent, 1, i);
        //                ShowGradeGridItem item = new ShowGradeGridItem(pr.Ranking, pr.athlete.Name, (sbyte)i, game);
        //                f_items.Add(item);
        //                f_prs.Add(pr1);
        //                gymDBService.Add(pr1);
        //                gymDBService.Grouping(3);
        //            }

        //        }
        //        else if (athnum >= 7 && athnum < 10)
        //        {
        //            for (int i = 1; i <= 5; i++)
        //            {
        //                PersonalResult pr = dB.personalresult.Where(p => p.Ranking == i).Single();
        //                PersonalResult pr1 = new PersonalResult(pr.AthleteID, sportsevent, 1, i);
        //                ShowGradeGridItem item = new ShowGradeGridItem(pr.Ranking, pr.athlete.Name, (sbyte)i, game);
        //                f_items.Add(item);
        //                f_prs.Add(pr1);
        //                gymDBService.Add(pr1);
        //                gymDBService.Grouping(3);
        //            }
        //        }
        //        else if (athnum >= 4 && athnum <= 7)
        //        {
        //            for (int i = 1; i <= 4; i++)
        //            {
        //                PersonalResult pr = dB.personalresult.Where(p => p.Ranking == i).Single();
        //                PersonalResult pr1 = new PersonalResult(pr.AthleteID, sportsevent, 1, i);
        //                ShowGradeGridItem item = new ShowGradeGridItem(pr.Ranking, pr.athlete.Name, (sbyte)i, game);
        //                f_items.Add(item);
        //                f_prs.Add(pr1);
        //                gymDBService.Add(pr1);
        //            }
        //        }
        //    }
        //}

        //个人全能成绩:将运动员总成绩算出来
        public void AtheleteGrid()
        {
            //取数据库所有运动员
            List<Athlete> athletes = new List<Athlete>();
            athletes = gymDBService.GetAllAthletes();
            List<PersonalResult> prs2 = new List<PersonalResult>();
            foreach (Athlete a in athletes)
            {
                List<PersonalResult> personalResults = new List<PersonalResult>();
                personalResults = gymDBService.GetPersonalResultsByAthleteID(a.AthleteID);
                string athName = a.Name;
                string atheleteID = a.AthleteID;
                int atheletegrade = 0;
                //将单个运动员成绩相加
                foreach (PersonalResult pr in personalResults)
                {
                    atheletegrade += (int)pr.Grade;
                }

                PersonalResult presult = new PersonalResult(atheleteID, atheletegrade);
                prs2.Add(presult);

            }
            //给个人全能成绩排序
            if (prs2.Count == 0)
                return;
            gymDBService.Ranking(prs2);
            short rank = 0;
            foreach (PersonalResult pr in prs2)
            {
                rank++;
                Athlete athe = gymDBService.GetAthleteByID(pr.AthleteID);
                ShowGradeGridItem showGradeGridItem = new ShowGradeGridItem(athe.Name, pr.AthleteID, pr.Grade, rank);
                p_items.Add(showGradeGridItem);
            }

        }

    }
}

