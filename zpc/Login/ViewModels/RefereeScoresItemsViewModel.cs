using DB;

namespace Login.ViewModels
{
    class RefereeScoresItemsViewModel:BaseViewModel
    {
        public RefereeScoresItemsViewModel(int _PRid, RefereeScore _referee)
        {
            Referee = _referee;
            GymDBService dbs = new GymDBService();
            PersonalResult target = dbs.GetPersonalResultByPRid(_PRid);
            Athlete athlete = dbs.GetAthleteByID(target.AthleteID);
            AthleteName = athlete.Name;
            AthleteID = athlete.AthleteID;
            SportsEvent = dbs.GetRealSportName(target.SportsEvent);
            MatchType = dbs.GetMatchType(target);
        }

        public string AthleteName { set; get; }

        public string AthleteID { set; get; }

        public string SportsEvent { set; get; }

        private RefereeScore _refereeScore;

        public RefereeScore Referee
        {
            get { return _refereeScore; }
            set
            {
                _refereeScore = value;
                OnPropertyChanged();
            }
        }

        public string MatchType { set; get; }
    }
}
