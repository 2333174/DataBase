using System.Collections.Generic;
using System.Windows.Controls;
using DB;

namespace Login.Views
{
    /// <summary>
    /// AthleteInfoPage.xaml 的交互逻辑
    /// </summary>
    public partial class AthleteInfoPage : Page
    {
        public AthleteInfoPage(int TID)
        {
            InitializeComponent();
            GymDBService dbs = new GymDBService();
            List<Athlete> athletes = dbs.GetAthletesByTID(TID);
            foreach (var athlete in athletes)
            {
                AthleteInfoControl athleteInfo = new AthleteInfoControl(athlete);
                dockpanel.Children.Add(athleteInfo);
            }
        }
    }
}
