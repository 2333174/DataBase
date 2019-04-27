using DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login.ViewModels
{
    class AthleteInfoViewModel:BaseViewModel
    {
        public AthleteInfoViewModel(Athlete athlete)
        {
            AnAthlete = athlete;
            GymDBService dbs = new GymDBService();
            TeamName = dbs.GetTeamNameByTID(athlete.TID);
            var prs = dbs.GetPersonalResultsByAthleteID(athlete.IDNumber);
            PersonalResults = new List<string>();
            foreach (var pr in prs)
            {
                try
                {
                    string tmpSport = dbs.GetRealSportName(pr);
                    PersonalResults.Add(tmpSport);
                }
                catch (Exception e)
                {
                    //ShowMessageInfo(e.Message);
                    throw e;
                }
            }
        }

        public string TeamName { set; get; }

        public List<string> PersonalResults { set; get; }

        public Athlete AnAthlete { set; get; }

        private async void ShowMessageInfo(string message)
        {
            Views.MessageDialog samMessageDialog = new Views.MessageDialog
            {
                Message = { Text = message }
            };
            await MaterialDesignThemes.Wpf.DialogHost.Show(samMessageDialog);
        }
    }
}
