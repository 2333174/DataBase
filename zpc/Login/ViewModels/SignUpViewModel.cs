using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DB;

namespace Login.ViewModels
{
    class SignUpViewModel:BaseViewModel
    {
        public SignUpViewModel(int Tid)
        {
            Teamid = Tid;
            _Athletes = null;
            AddCommand = new Commands.DelegateCommand();
            AddCommand.ExecuteAction = new Action<object>(Add);
        }

        public Commands.DelegateCommand AddCommand { get; set; }
        public Commands.DelegateCommand DeleteCommand { get; set; }
        public Commands.DelegateCommand ConfirmCommand { get; set; }

        private int Teamid;

        private Staff _TeamLeader;

        public Staff TeamLeader
        {
            get { return _TeamLeader; }
            set
            {
                _TeamLeader = value;
                OnPropertyChanged();
            }
        }

        private Staff _Doctor;

        public Staff Doctor
        {
            get { return _Doctor; }
            set
            {
                _Doctor = value;
                OnPropertyChanged();
            }
        }

        private Staff _Coach;

        public Staff Coach
        {
            get { return _Coach; }
            set
            {
                _Coach = value;
                OnPropertyChanged();
            }
        }

        private Judge _Judge;

        public Judge Judge
        {
            get { return _Judge; }
            set
            {
                _Judge = value;
                OnPropertyChanged();
            }
        }

        private readonly ObservableCollection<AthleteEntryViewModel> _Athletes;
        public ObservableCollection<AthleteEntryViewModel> Athletes => _Athletes;

        private bool IsTeamInfoNull()
        {
            bool res = true;
            res = res && (TeamLeader.Name != null) && (TeamLeader.IDNumber != null) && (TeamLeader.Telephone != null) && (TeamLeader.Gender != null);
            res = res && (Doctor.Name != null) && (Doctor.IDNumber != null) && (Doctor.Telephone != null) && (Doctor.Gender != null);
            res = res && (Coach.Name != null) && (Coach.IDNumber != null) && (Coach.Telephone != null) && (Coach.Gender != null);
            res = res && (Judge.Name != null) && (Judge.IDNumber != null) && (Judge.Telephone != null);
            foreach(var a in Athletes)
            {
                res = res && (a.Athlete.Name != null) && (a.Athlete.IDNumber != null) && (!a.Athlete.Age.Equals(null)) && (a.Athlete.CulturalGrade != null) && (a.Athlete.Gender != null);
                foreach(var e in a.Events) { } // 组别
            }
            return res;
        }

        private void Add(object parameter)
        {
            if (IsTeamInfoNull() != true) ShowMessageInfo("输入有空值！");
            GymDBService dbs = new GymDBService();
            Staff leader = new Staff(TeamLeader.Name, TeamLeader.IDNumber, TeamLeader.Gender, TeamLeader.Telephone, "0", Teamid);
            Staff doctor = new Staff(Doctor.Name, Doctor.IDNumber, Doctor.Gender, Doctor.Telephone, "1", Teamid);
            Staff coach = new Staff(Coach.Name, Coach.IDNumber, Coach.Gender, Coach.Telephone, "2", Teamid);
            dbs.Add(leader);
            dbs.Add(doctor);
            dbs.Add(coach);
            foreach(var a in Athletes)
            {
                Athlete athlete = new Athlete(a.Athlete.Name, a.Athlete.IDNumber, a.Athlete.Age, a.Athlete.Gender);
                dbs.Add(athlete);
                // 添加PersonalResult
            }
        }

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
