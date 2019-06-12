using DB;

namespace Login.ViewModels
{
    class AthletePRViewModel : BaseViewModel
    {
        private string _SportEvent;

        public string SportEvent
        {
            get { return _SportEvent; }
            set
            {
                _SportEvent = value;
                OnPropertyChanged();
            }
        }

        private string _Grade;

        public string Grade
        {
            get { return _Grade; }
            set
            {
                _Grade = value;
                OnPropertyChanged();
            }
        }

        public AthletePRViewModel(PersonalResult pr)
        {
            GymDBService dbs = new GymDBService();
            SportEvent += dbs.GetRealSportName(pr.SportsEvent);
            switch (pr.Role)
            {
                case 0:
                    SportEvent += "预赛";
                    break;
                case 1:
                    SportEvent += "决赛";
                    break;
            }
            if (pr.Grade != null)
            {
                Grade = pr.Grade.ToString();
                Grade += "(" + pr.Ranking.ToString() + ")";
            }
        }
    }
}
