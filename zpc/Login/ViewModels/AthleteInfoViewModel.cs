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
            var prs = dbs.GetPersonalResultsByAthleteID(athlete.AthleteID);
            PersonalResults = new List<string>();
            foreach (var pr in prs)
            {
                string tmpSport = null;
                switch (pr.SportsEvent)
                {
                    
                }
                PersonalResults.Add(tmpSport);
            }
        }

        public List<string> PersonalResults { set; get; }

        public Athlete AnAthlete { set; get; }
    }
}
