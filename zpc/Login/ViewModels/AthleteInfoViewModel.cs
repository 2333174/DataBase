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
            PersonalResults = dbs.GetPersonalResultsByAthleteID(athlete.AthleteID);
        }

        public List<PersonalResult> PersonalResults { set; get; }

        public Athlete AnAthlete { set; get; }
    }
}
