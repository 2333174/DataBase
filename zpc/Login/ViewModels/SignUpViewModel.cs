using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DB;
using Login.Commands;
using Login.Models;
using Login.Views;
using MaterialDesignThemes.Wpf;

namespace Login.ViewModels
{
    class SignUpViewModel:BaseViewModel
    {
        public SignUpViewModel(string TName)
        {

            Teamid = dbs.GetTIDByTName(TName);

            //数据初始化
            _TeamLeader = new Staff();
            _Doctor = new Staff();
            _Coach = new Staff();
            _Judge = new Judge();

            _Athletes = null;
            DeleteItem = null;
            _AthleteInfos = new ObservableCollection<DataGridItem>();
            _select = new List<int>();
            for (int i = 0; i < 12; i++)
            {
                _select.Add(0);
            }
            sportEvents = new List<string>();
            Events = new List<string>();
            Events.Add("单杠");
            Events.Add("双杠");
            Events.Add("吊环");
            Events.Add("跳马");
            Events.Add("自由体操");
            Events.Add("鞍马");
            Events.Add("蹦床");
            Events.Add("跳马");
            Events.Add("高低杠");
            Events.Add("平衡木");
            Events.Add("自由体操");
            Events.Add("蹦床");

            //命令初始化
            AddCommand = new DelegateCommand();
            AddCommand.ExecuteAction = new Action<object>(Add);
            AddDataGridCommand = new DelegateCommand();
            AddDataGridCommand.ExecuteAction = new Action<object>(AddDataGrid);
            DeleteCommand = new DelegateCommand();
            DeleteCommand.ExecuteAction = new Action<object>(DeleteDataGrid);
        }

        public DelegateCommand AddDataGridCommand{ get; set; } //添加Datagrid数据
        public DelegateCommand AddCommand { get; set; }
        public DelegateCommand DeleteCommand { get; set; }
        public DelegateCommand ConfirmCommand { get; set; }

        private int Teamid;

        //存储项目名称
        public List<string> Events;

        //存储运动员报名项目
        public List<string> sportEvents;

        private GymDBService dbs = new GymDBService();

        //记录多选按钮
        private List<int> _select;

        public List<int> select
        {
            get { return _select; }
            set { _select = value; OnPropertyChanged(); }
        }


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

        //datagrid行数据
        private readonly ObservableCollection<DataGridItem> _AthleteInfos;
        public ObservableCollection<DataGridItem> AthleteInfos => _AthleteInfos;

        //删除行数据
        private DataGridItem _DeleteItem;

        public DataGridItem DeleteItem
        {
            get { return _DeleteItem; }
            set
            {
                _DeleteItem = value;
                OnPropertyChanged();
            }
        }


        private bool IsTeamInfoNull()
        {
            Console.WriteLine(TeamLeader.Gender);
            bool res = true;
            res = res && (TeamLeader.Name != null) && (TeamLeader.IDNumber != null) && (TeamLeader.Telephone != null) && (TeamLeader.Gender != null);
            res = res && (Doctor.Name != null) && (Doctor.IDNumber != null) && (Doctor.Telephone != null) && (Doctor.Gender != null);
            res = res && (Coach.Name != null) && (Coach.IDNumber != null) && (Coach.Telephone != null) && (Coach.Gender != null);
            res = res && (Judge.Name != null) && (Judge.IDNumber != null) && (Judge.Telephone != null);
            res = res && (AthleteInfos != null);
            return res;
        }

        //实现添加dataGrid数据
        private void AddDataGrid(object parameter)
        {
            ShowAddDialog();
        }

        //实现删除数据
        private void DeleteDataGrid(object parameter)
        {
            Console.WriteLine("删除数据");
            AthleteInfos.Remove(DeleteItem);   
        }
        private void Add(object parameter)
        {
            if (IsTeamInfoNull() != true) ShowMessageInfo("输入有空值！");
            else
            {
                GymDBService dbs = new GymDBService();
                Staff leader = new Staff(TeamLeader.Name, TeamLeader.IDNumber, TeamLeader.Gender, TeamLeader.Telephone, "0", Teamid);
                Staff doctor = new Staff(Doctor.Name, Doctor.IDNumber, Doctor.Gender, Doctor.Telephone, "1", Teamid);
                Staff coach = new Staff(Coach.Name, Coach.IDNumber, Coach.Gender, Coach.Telephone, "2", Teamid);
                dbs.Add(leader);
                dbs.Add(doctor);
                dbs.Add(coach);
                dbs.Add(Judge);
                foreach (var a in AthleteInfos)
                {
                    a.Athlete.TID = Teamid;
                    dbs.Add(a.Athlete);

                }
            }
        }

        

        private async void ShowMessageInfo(string message)
        {
            MessageDialog samMessageDialog = new Views.MessageDialog
            {
                Message = { Text = message }
            };
           await DialogHost.Show(samMessageDialog);
        }

        //显示添加框 并进行处理
        private async void ShowAddDialog()
        {
            //重置sportList
            sportEvents.Clear();
            //重置多选
            for (int i = 0; i < 12; i++)
            {
                select[i] = 0;
            }
            //打开对话框
            DialogClosingEventHandler dialogClosingEventHandler = null;
            AddAthleteDialog samMessageDialog = new AddAthleteDialog
            {
            };
            var result = await MaterialDesignThemes.Wpf.DialogHost.Show(samMessageDialog, dialogClosingEventHandler);
            Console.WriteLine(result.ToString());

            //添加数据
            if (Equals(result,true))
            {
                for (int i = 0; i < 12; i++)
                {
                    if (select[i] == 1) sportEvents.Add(Events[i]);
                }
                if (!(string.IsNullOrWhiteSpace(samMessageDialog.ID.Text)
                    || string.IsNullOrWhiteSpace(samMessageDialog.Name.Text)
                    || string.IsNullOrWhiteSpace(samMessageDialog.Age.Text)
                    || string.IsNullOrWhiteSpace(samMessageDialog.PhoneNum.Text))
                    && (samMessageDialog.Gender.SelectedValue != null) 
                  //  && (samMessageDialog.SportEvent.SelectedValue != null)
                    &&sportEvents.Count!=0)
                {
                    Athlete athlete = new Athlete(samMessageDialog.Name.Text, samMessageDialog.ID.Text, int.Parse(samMessageDialog.Age.Text), samMessageDialog.Gender.Text);
                    DataGridItem dataGridItem = new DataGridItem(athlete, sportEvents);
                    AthleteInfos.Add(dataGridItem);
                    
                    Console.WriteLine("添加成功");
                }
                else
                {
                    ShowMessageInfo("请完善信息");
                }
            }
        }
    }
}
