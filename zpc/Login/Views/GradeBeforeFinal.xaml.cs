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
    /// GradeBeforeFinal.xaml 的交互逻辑
    /// </summary>
    public partial class GradeBeforeFinal : Window
    {
        GymDB dB = new GymDB();
        GymDBService gymDBService = new GymDBService();
        List<TeamResultGridItem> teamResultGridItems = new List<TeamResultGridItem>();
        public GradeBeforeFinal()
        {
            InitializeComponent();
            //num:团体比赛计算前几名的成绩
            int num = gymDBService.GetSettingPthree();
            //events:初赛所有赛事
            SortedSet<string> events = new SortedSet<string>();            List<PersonalResult> personalResults = new List<PersonalResult>();            personalResults = gymDBService.GetPersonalResults();            foreach (PersonalResult p in personalResults)            {                events.Add(p.SportsEvent);            }            foreach (string a in events)            {                List<Team> teams = gymDBService.GetAllTeams();                foreach (Team t in teams)                {                    short rank = 0;                    float grade = 0;
                    //该队的所有运动员
                    ICollection<Athlete> athletes = t.athlete;
                    //存放该队该赛事的PersonalResults的list
                    List<PersonalResult> prs1 = new List<PersonalResult>();                    foreach (Athlete ath in athletes)                    {                        List<PersonalResult> results = ath.personalresult.Where(p => p.SportsEvent == a).ToList();                        foreach (PersonalResult b in results)                        {                            prs1.Add(b);                        }                    }                    for (int i = 0; i < num; i++)                    {                        grade += (float)prs1.ElementAt(i).Grade;                    }                    TeamResultGridItem teamResultGridItem = new TeamResultGridItem(a, 0, t.TName, grade);                    teamResultGridItems.Add(teamResultGridItem);                }

            }
            
        }
    }
}
