using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DB;
using Login.Commands;

namespace Login.ViewModels
{
    class GSFMPViewModel : BaseViewModel
    {
        private ObservableCollection<PRItemsViewModel> _GridItems;

        public ObservableCollection<PRItemsViewModel> GridItems
        {
            get { return _GridItems; }
            set
            {
                _GridItems = value;
                OnPropertyChanged();
            }
        }

        public void DetailExpanded(int index)
        {
            if (index >= 0)
            {
                GridItems[index].IsVisibility = Visibility.Visible;
            }
        }

        public void DetailCollapsed(int index)
        {
            if (index >= 0)
            {
                GridItems[index].IsVisibility = Visibility.Collapsed;
            }
        }

        public DelegateCommand PreviewCommand { set; get; }
        public DelegateCommand SubmitCommand { set; get; }
        public string groupid;
        public GSFMPViewModel(string groupid)
        {
            this.groupid = groupid;
            GymDBService dbs = new GymDBService();
            List<PersonalResult> prs = dbs.GetPersonalResultsByGroupID(groupid);
            GridItems = new ObservableCollection<PRItemsViewModel>();
            int index = 1;
            foreach (PersonalResult pr in prs)
            {
                PRItemsViewModel tmp = new PRItemsViewModel(pr, index);
                GridItems.Add(tmp);
                index++;
            }

            PreviewCommand = new DelegateCommand();
            PreviewCommand.ExecuteAction = new Action<object>(Preview);
            SubmitCommand = new DelegateCommand();
            SubmitCommand.ExecuteAction = new Action<object>(Submit);
        }

        [Obsolete("score应为Double",false)]
        private void Preview(object parameter)
        {
            GymDBService dbs = new GymDBService();
            foreach (PRItemsViewModel pRItems in GridItems)
            {
                List<int> scores = new List<int>();
                foreach (PRDetailsViewModel pR in pRItems.Details)
                {
                    scores.Add(pR.TBDScores);
                }
                if(scores.Count() >= 3)
                {
                    scores.Remove(scores.Max());
                    scores.Remove(scores.Min());
                }

                pRItems.TotalScores = (int)scores.Average() * pRItems.Details.Count() + pRItems.Bouns - pRItems.Punishment;
            }
        }

        private void Submit(object parameter)
        {
            GymDBService dbs = new GymDBService();
            List<PersonalResult> prs = new List<PersonalResult>();
            foreach (PRItemsViewModel pRItems in GridItems)
            {
                PersonalResult pr = dbs.GetPersonalResultByPRid(pRItems.prid);
                pr.Bouns = (short)pRItems.Bouns;
                pr.Punishment = (short)pRItems.Punishment;
                pr.Grade = (short)pRItems.TotalScores;
                dbs.Update(pr);
                prs.Add(pr);
            }
            dbs.Ranking(prs);
            Client1.ClientSendMsg("主裁判打完分:"+groupid);
        }
    }
}
