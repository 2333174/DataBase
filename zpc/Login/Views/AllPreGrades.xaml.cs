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
using DB;
namespace Login.Views
{
    /// <summary>
    /// AllPreGrades.xaml 的交互逻辑
    /// </summary>
    public partial class AllPreGrades : Window
    {
        GymDB dB = new GymDB();
        GymDBService gymDBService = new GymDBService();
        List<TeamResult> teamResults = new List<TeamResult>();
        //记录决赛
        List<PersonalResult> f_prs = new List<PersonalResult>();
        List<ShowGradeGridItem> f_items = new List<ShowGradeGridItem>();
        //events:初赛所有赛事
        SortedSet<string> events = new SortedSet<string>();
        //个人全能成绩
        List<ShowGradeGridItem> p_items = new List<ShowGradeGridItem>();
        public AllPreGrades()
        {
            InitializeComponent();
            //给teamgrid数据的函数
            TeamGrid();
            //给teamgrid绑定数据源
            teamgrid.ItemsSource = teamResults;
            //编排决赛
            ComputeFinalSuq();
            //给rankgrid绑定数据源
            rankgrid.ItemsSource = f_items;
            //给atheltegrid数据的函数
            AtheleteGrid();
            //给atheltegrid绑定数据源
            athletegrid.ItemsSource = p_items;
        }

        public void TeamGrid()
        {
            //num:团体比赛计算前几名的成绩
            int num = gymDBService.GetSettingPthree();
            List<PersonalResult> personalResults = new List<PersonalResult>();
            personalResults = gymDBService.GetPersonalResults();
            foreach (PersonalResult p in personalResults)
            {
                events.Add(p.SportsEvent);
            }
            //计算团体成绩
            foreach (string a in events)
            {
                string game = gymDBService.GetFullSportName(a);
                List<Team> teams = gymDBService.GetAllTeams();
                foreach (Team t in teams)
                {
                    float grade = 0;
                    //该队的所有运动员
                    ICollection<Athlete> athletes = t.athlete;
                    //存放该队该赛事的PersonalResults的list
                    List<PersonalResult> prs1 = new List<PersonalResult>();
                    foreach (Athlete ath in athletes)
                    {
                        List<PersonalResult> results = ath.personalresult.Where(p => p.SportsEvent == a).ToList();
                        foreach (PersonalResult b in results)
                        {
                            prs1.Add(b);
                        }
                    }
                    //给这些运动员的成绩排序
                    gymDBService.Ranking(prs1);
                    //计算前n名运动员的成绩
                    for (int i = 0; i < num; i++)
                    {
                        grade += (float)prs1.Where(p=>p.Ranking.Equals(i+1)).Single().Grade;
                    }
                    //int TID,string Event,short? Grade,short? Ranking,int TRid,Team team
                    //向数据库添加teamresult
                    TeamResult teamResult = new TeamResult(t.TID, a, grade, t);
                    gymDBService.Add(teamResult);
                }
                gymDBService.Ranking(gymDBService.GetTeamResultsByEvent(a));
                foreach (Team t in teams)
                {
                    TeamResult tr = dB.teamresult.Where(p => p.TID.Equals(t.TID) && p.Event.Equals(a)).Single();
                    tr.Event = game;
                    //向数据源添加Teamresult
                    teamResults.Add(tr);
                }
                
            }
        }
        //计算单项决赛次序 当一个项目运动员数大于等于10时，一半的运动员晋级决赛
        //7、8或9个运动员晋级五个，四、五或六个运动员晋级4个，三个及三个一下不进行决赛
        public void ComputeFinalSuq()
        {
            foreach(string s in events)
            {
                string game= gymDBService.GetFullSportName(s);
                List<PersonalResult> personalResults = gymDBService.GetPersonalResultsBySportEventAndRole(s, 0).ToList();
                int athnum = personalResults.Count;
                gymDBService.Ranking(personalResults);
                if(athnum>=10)
                {
                    for(int i=1; i <= athnum/2; i++)
                    {
                        PersonalResult pr = dB.personalresult.Where(p => p.Ranking == i).Single();
                        PersonalResult pr1 = new PersonalResult(pr.AthleteID,pr.SportsEvent,1,i);
                        ShowGradeGridItem item = new ShowGradeGridItem(pr.Ranking, pr.athlete.Name, (sbyte)i, game);
                        f_items.Add(item);
                        f_prs.Add(pr1);
                        gymDBService.Add(pr1);
                    }
                     
                }else if(athnum>=7&&athnum<10)
                {
                    for (int i = 1; i <= 5; i++)
                    {
                        PersonalResult pr = dB.personalresult.Where(p => p.Ranking == i).Single();
                        PersonalResult pr1 = new PersonalResult(pr.AthleteID, pr.SportsEvent, 1,i);
                        ShowGradeGridItem item = new ShowGradeGridItem(pr.Ranking, pr.athlete.Name, (sbyte)i, game);
                        f_items.Add(item);
                        f_prs.Add(pr1);
                        gymDBService.Add(pr1);
                    }
                }else if(athnum>=4&&athnum<=7)
                {
                    for (int i = 1; i <= 4; i++)
                    {
                        PersonalResult pr = dB.personalresult.Where(p => p.Ranking == i).Single();
                        PersonalResult pr1 = new PersonalResult(pr.AthleteID, pr.SportsEvent, 1,i);
                        ShowGradeGridItem item = new ShowGradeGridItem(pr.Ranking, pr.athlete.Name, (sbyte)i, game);
                        f_items.Add(item);
                        f_prs.Add(pr1);
                        gymDBService.Add(pr1);
                    }
                }
            }
        }

        //个人全能成绩
        public void AtheleteGrid()
        {
            List<Athlete> athletes = new List<Athlete>();
            athletes = gymDBService.GetAllAthletes();
           
            foreach (Athlete a in athletes)
            {
                List<PersonalResult> personalResults = new List<PersonalResult>();
                personalResults = gymDBService.GetPersonalResultsByAthleteID(a.AthleteID);
                foreach(PersonalResult pr in personalResults)
                {
                    string athName = a.Name;
                    string atheleteID = a.AthleteID;
                    string game = pr.SportsEvent;
                    float atheletegrade = (float)pr.Grade;
                    short rank = (short)pr.Ranking;

                    ShowGradeGridItem showGradeGridItem = new ShowGradeGridItem(athName, atheleteID, game, atheletegrade, rank);
                    p_items.Add(showGradeGridItem);
                }
            }
        }
    }
}
