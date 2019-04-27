using System.Collections.Generic;
using System.Collections.ObjectModel;
using DB;

namespace Login.ViewModels
{
    class PRItemsViewModel : BaseShrinkViewModel
    {
        public PRItemsViewModel(PersonalResult pr, int index)
        {
            GymDBService dbs = new GymDBService();
            prid = pr.PRid;
            List<RefereeScore> refereeScores = dbs.GetRefereeScoresByPRid(prid);
            this.index = index;
            AthleteName = dbs.GetAthleteByID(pr.AthleteID).Name;
            SportEvent = dbs.GetRealSportName(pr);
            MatchType = dbs.GetMatchType(pr);
            Details = new ObservableCollection<PRDetailsViewModel>();
            foreach (var rs in refereeScores)
            {
                PRDetailsViewModel tmp = new PRDetailsViewModel(rs);
                Details.Add(tmp);
            }
        }

        public int prid;

        private int _index;

        public int index
        {
            get { return _index; }
            set
            {
                _index = value;
                OnPropertyChanged();
            }
        }

        private string _AthleteName;

        public string AthleteName
        {
            get { return _AthleteName; }
            set
            {
                _AthleteName = value;
                OnPropertyChanged();
            }
        }

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

        private string _MatchType;

        public string MatchType
        {
            get { return _MatchType; }
            set
            {
                _MatchType = value;
                OnPropertyChanged();
            }
        }

        private int _Bouns;

        public int Bouns
        {
            get { return _Bouns; }
            set
            {
                _Bouns = value;
                OnPropertyChanged();
            }
        }

        private int _Punishment;

        public int Punishment
        {
            get { return _Punishment; }
            set
            {
                _Punishment = value;
                OnPropertyChanged();
            }
        }

        private int _TotalScores;

        public int TotalScores
        {
            get { return _TotalScores; }
            set
            {
                _TotalScores = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<PRDetailsViewModel> _Details;

        public ObservableCollection<PRDetailsViewModel> Details
        {
            get { return _Details; }
            set
            {
                _Details = value;
                OnPropertyChanged();
            }
        }
    }
}
