using DB;

namespace Login.ViewModels
{
    class PRDetailsViewModel : BaseViewModel
    {
        public PRDetailsViewModel(RefereeScore refereeScore)
        {
            JudgeID = (int)refereeScore.JudgeID;
            TBDScores = (int)refereeScore.Scores;
        }

        private int _judgeID;

        public int JudgeID
        {
            get { return _judgeID; }
            set
            {
                _judgeID = value;
                OnPropertyChanged();
            }
        }

        private int _TBDScores;

        public int TBDScores
        {
            get { return _TBDScores; }
            set
            {
                _TBDScores = value;
                OnPropertyChanged();
            }
        }
    }
}
