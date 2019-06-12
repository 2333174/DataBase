using DB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

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
            PersonalResults = new ObservableCollection<AthletePRViewModel>();
            foreach (var pr in prs)
            {
                try
                {
                    PersonalResults.Add(new AthletePRViewModel(pr));
                }
                catch (Exception e)
                {
                    //ShowMessageInfo(e.Message);
                    throw e;
                }
            }
        }

        public string TeamName { set; get; }

        private ObservableCollection<AthletePRViewModel> _PersonalResults;

        public ObservableCollection<AthletePRViewModel> PersonalResults
        {
            get { return _PersonalResults; }
            set
            {
                _PersonalResults = value;
                OnPropertyChanged();
            }
        }

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
