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
        public AllPreGrades()
        {
            InitializeComponent();
            //num:团体比赛计算前几名的成绩
            int num = gymDBService.GetSettingPthree();
            //events:初赛所有赛事
            SortedSet<string> events = new SortedSet<string>();            List<PersonalResult> personalResults = new List<PersonalResult>();            personalResults = gymDBService.GetPersonalResults();            foreach (PersonalResult p in personalResults)            {                events.Add(p.SportsEvent);            }            foreach (string a in events)            {                List<Team> teams = gymDBService.GetAllTeams();                foreach (Team t in teams)                {                    short rank = 0;                    float grade = 0;
                    //该队的所有运动员
                    ICollection<Athlete> athletes = t.athlete;
                    //存放该队该赛事的PersonalResults的list
                    List<PersonalResult> prs1 = new List<PersonalResult>();                    foreach (Athlete ath in athletes)                    {                        List<PersonalResult> results = ath.personalresult.Where(p => p.SportsEvent == a).ToList();                        foreach (PersonalResult b in results)                        {                            prs1.Add(b);                        }                    }                    for (int i = 0; i < num; i++)                    {                        grade += (float)prs1.ElementAt(i).Grade;                    }
                    //int TID,string Event,short? Grade,short? Ranking,int TRid,Team team
                    //向数据库添加teamresult
                    TeamResult teamResult = new TeamResult(t.TID, a, grade,t);
                    gymDBService.Add(teamResult);                }
                gymDBService.Ranking(gymDBService.GetTeamResultsByEvent(a));
                foreach (Team t in teams)
                {
                    TeamResult tr = dB.teamresult.Where(p => p.TID.Equals(t.TID) && p.Event.Equals(a)).Single();
                    //向数据源添加Teamresult
                    teamResults.Add(tr);
                }
                teamgrid.ItemsSource = teamResults;
            }
        }
    }
}
