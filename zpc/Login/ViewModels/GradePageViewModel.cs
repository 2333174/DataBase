using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using DB;
using Login.Views;
using System.Windows.Controls;

namespace Login.ViewModels
{
    class GradePageViewModel:BaseViewModel
    {
        public GradePage gradePage;
        public int judgeID;
        public GradePageViewModel(int judgeID, int groupKey,GradePage page)
        {
            this.judgeID = judgeID;
            gradePage = page;
            SubmitCommand = new Commands.DelegateCommand();
            SubmitCommand.ExecuteAction = new Action<object>(Submit);
            GymDBService dbs = new GymDBService();
            target = dbs.GetMatchGroupByKey(groupKey);
            var judge = dbs.GetJudgeByJudgeID(judgeID);
            JudgeName = judge.Name;
            List<PersonalResult> personalResults = dbs.GetPersonalResultsByGroupID(target.GroupID);
            RefereeScoresItems = new ObservableCollection<RefereeScoresItemsViewModel>();
            foreach (var pr in personalResults)
            {
                RefereeScore referee = new RefereeScore(pr.PRid, judgeID);
                if (!dbs.Add(referee)) referee = dbs.GetRefereeScoreByPridAndJudgeID(pr.PRid, judgeID);
                RefereeScoresItemsViewModel tmp = new RefereeScoresItemsViewModel(pr.PRid, referee);
                RefereeScoresItems.Add(tmp);
            }
        }

        public MatchGroup target;

        public Commands.DelegateCommand SubmitCommand { set; get; }

        private ObservableCollection<RefereeScoresItemsViewModel> _refereeScoresItems;

        public ObservableCollection<RefereeScoresItemsViewModel> RefereeScoresItems
        {
            get { return _refereeScoresItems; }
            set
            {
                _refereeScoresItems = value;
                OnPropertyChanged();
            }
        }

        public string JudgeName { set; get; }

        private bool IsScoreVital(int? score)
        {
            if (score <= 100 && score >= 0)
                return true;
            else
                return false;
        }

        private bool IsScoresVital(ObservableCollection<RefereeScoresItemsViewModel> refereeScores)
        {
            bool res = true;
            foreach (var rsv in refereeScores)
                res = res && IsScoreVital(rsv.Referee.Scores);
            return res;
        }

        public void Submit(object parameter)
        {
            if(!IsScoresVital(RefereeScoresItems))
            {
                ShowWarningInfo("输入成绩有误！");
                return;
            }
            foreach(var rsi in RefereeScoresItems)
            {
                var tmp = rsi.Referee;
                GymDBService dbs = new GymDBService();
                dbs.Update(tmp);
            }
            Client.ClientSendMsg("分裁判打完分:" + target.GroupID);
            ShowMessageInfo("打分成功！");
        }

        private async void ShowMessageInfo(string message)
        {
            Views.ComfirmDialog samMessageDialog = new Views.ComfirmDialog
            {
                Message = { Text = message }
            };
            var result = await MaterialDesignThemes.Wpf.DialogHost.Show(samMessageDialog);
            if (Equals(result,true))
            {
                gradePage.GradePage1.Content = new Frame
                {
                    Content = new welcomePage(judgeID)
                };
            }
        }

        private async void ShowWarningInfo(string message)
        {
            Views.MessageDialog samMessageDialog = new Views.MessageDialog
            {
                Message = { Text = message }
            };
            await MaterialDesignThemes.Wpf.DialogHost.Show(samMessageDialog);
        }
    }
}
