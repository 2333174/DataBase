using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using DB;
using Login.Commands;
using Login.Views;

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
        private GSForMajorJudgePage page;
        public GSFMPViewModel(string groupid, GSForMajorJudgePage page)
        {
            this.groupid = groupid;
            this.page = page;
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
            Client.ClientSendMsg("主裁判打完分:"+groupid);
            ShowMessageInfo("打分成功!");
            string type = groupid.Substring(3, 1) == "0" ? "预赛" : "决赛";
            page.MajorJudgePage.Content = new Frame
            { Content = new ShowGrade(groupid,dbs.GetRealSportName(groupid.Substring(0,4)), type) };
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
