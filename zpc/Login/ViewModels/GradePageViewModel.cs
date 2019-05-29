using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using DB;

namespace Login.ViewModels
{
    class GradePageViewModel:BaseViewModel
    {
        public GradePageViewModel(int judgeID, int groupKey)
        {
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
            ShowMessageInfo("打分成功！");
            Client1.ClientSendMsg("分裁判打完分:"+ target.GroupID);
        }

        private async void ShowMessageInfo(string message)
        {
            Views.ComfirmDialog samMessageDialog = new Views.ComfirmDialog
            {
                Message = { Text = message }
            };
            await MaterialDesignThemes.Wpf.DialogHost.Show(samMessageDialog);
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
