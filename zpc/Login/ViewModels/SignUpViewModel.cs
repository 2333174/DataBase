using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using DB;
using Login.Commands;
using Login.Models;
using Login.Views;
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;

namespace Login.ViewModels
{
    class SignUpViewModel:BaseViewModel
    {
        public SignUpViewModel(string TName,SignUpPage signUp)
        {
            Teamid = dbs.GetTIDByTName(TName);
            this.signUp = signUp;
            //数据初始化
            _TeamLeader = new Staff();
            _Doctor = new Staff();
            _Coach = new Staff();
            _Judge = new Judge();
            _fileName = "";

            _Visibility1 = Visibility.Visible;
            _Visibility2 = Visibility.Hidden;

            _Athletes = null;
            DeleteItem = null;
            _AthleteInfos = new ObservableCollection<DataGridItem>();
            _select = new List<int>();
            for (int i = 0; i < 12; i++)
            {
                _select.Add(0);
            }
            Events = new List<string>();
            Events.Add("男子单杠");
            Events.Add("男子双杠");
            Events.Add("男子吊环");
            Events.Add("男子跳马");
            Events.Add("男子自由体操");
            Events.Add("男子鞍马");
            Events.Add("男子蹦床");
            Events.Add("女子跳马");
            Events.Add("女子高低杠");
            Events.Add("女子平衡木");
            Events.Add("女子自由体操");
            Events.Add("女子蹦床");

            //命令初始化
            AddCommand = new DelegateCommand();
            AddCommand.ExecuteAction = new Action<object>(Add);
            AddDataGridCommand = new DelegateCommand();
            AddDataGridCommand.ExecuteAction = new Action<object>(AddDataGrid);
            DeleteCommand = new DelegateCommand();
            DeleteCommand.ExecuteAction = new Action<object>(DeleteDataGrid);
            AddDosCommand = new DelegateCommand();
            AddDosCommand.ExecuteAction = new Action<object>(AddDos);
            DeleteDosCommand = new DelegateCommand();
            DeleteDosCommand.ExecuteAction = new Action<object>(DeleteDos);
        }

        public DelegateCommand AddDataGridCommand{ get; set; } //添加Datagrid数据
        public DelegateCommand AddCommand { get; set; }
        public DelegateCommand AddDosCommand { get; set; }
        public DelegateCommand DeleteCommand { get; set; }
        public DelegateCommand ConfirmCommand { get; set; }
        public DelegateCommand DeleteDosCommand { get; set; }

        private int Teamid;
        private SignUpPage signUp;

        //控件显示
        private Visibility _Visibility1;
        private Visibility _Visibility2;
        public Visibility Visibility1
        {
            get { return _Visibility1; }
            set
            {
                _Visibility1 = value;
                OnPropertyChanged();
            }
        }
        public Visibility Visibility2
        {
            get { return _Visibility2; }
            set
            {
                _Visibility2 = value;
                OnPropertyChanged();
            }
        }

        //存储项目名称
        public List<string> Events;

        //存储文件信息
        byte[] bytes;

        private string _fileName;
        public string fileName
        {
            get { return _fileName; }
            set
            {
                _fileName = value;
                OnPropertyChanged();
            }
        }

        //存储运动员报名项目
        public List<string> sportEvents;
        private List<string> sportEventsID;


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
            res = res && (AthleteInfos != null)&&(bytes!=null);
            return res;
        }

        private void AddDos(object parameter)
        {
            OpenFileDialog file = new OpenFileDialog();
            
            if (file.ShowDialog() == true)
            {
                Visibility1 = Visibility.Hidden;
                Visibility2 = Visibility.Visible;
                fileName = System.IO.Path.GetFileName(file.FileName);
                Console.WriteLine(fileName);
                FileInfo fi = new FileInfo(file.FileName);
                FileStream fs = fi.OpenRead();
                bytes = new byte[fs.Length];
                fs.Read(bytes, 0, Convert.ToInt32(fs.Length));
            }
        }

        private void DeleteDos(object parameter)
        {
            Visibility1 = Visibility.Visible;
            Visibility2 = Visibility.Hidden;
            bytes = null;
            Console.WriteLine("删除文件");

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
            _AthleteInfos.Remove(DeleteItem);   
        }
        private void Add(object parameter)
        {
            if (IsTeamInfoNull() != true) ShowMessageInfo("输入有空值！");
            else
            {
                ShowMessageInfo("保存成功");
                GymDBService dbs = new GymDBService();
                Staff leader = new Staff(TeamLeader.Name, TeamLeader.IDNumber, TeamLeader.Gender, TeamLeader.Telephone, "0", Teamid);
                Staff doctor = new Staff(Doctor.Name, Doctor.IDNumber, Doctor.Gender, Doctor.Telephone, "1", Teamid);
                Staff coach = new Staff(Coach.Name, Coach.IDNumber, Coach.Gender, Coach.Telephone, "2", Teamid);
                dbs.Add(leader);
                dbs.Add(doctor);
                dbs.Add(coach);
                dbs.Add(Judge);
                Team team = dbs.GetTeamByTID(Teamid);
                team.Docs = bytes;
                team.isSignUp = 1;
                dbs.Update(team);
                foreach (var a in AthleteInfos)
                {
                    a.Athlete.TID = Teamid;
                    dbs.Add(a.Athlete);
                    string s = null;
                    switch (a.Athlete.Age)
                    {
                        case 7:
                        case 8:
                            s = s + "0";
                            break;
                        case 9:
                        case 10:
                            s = s + "1";
                            break;
                        case 11:
                        case 12:
                            s = s + "2";
                            break;
                    }
                    string t = s;
                    foreach (var q in a.sportEventsID)
                    {
                        s = t;
                        s = s + q;
                        s = s + "0";
                        Console.WriteLine(s);
                        PersonalResult personalResult = new PersonalResult(a.Athlete.IDNumber, s, 0);//初赛为0 决赛为1
                        dbs.Add(personalResult);
                    }
                    
                }
                signUp.SignUp.Content = new Frame()
                { Content = new AthleteInfoPage(Teamid) };
            }
            //01:男子单杠；02：男子双杠；03：男子吊环；04男子跳马；05：男子自由体操；06：男子鞍马；07：男子蹦床
            //08：女子跳马；09：女子高低杠；10：女子平衡木；11：女子自由体操；12：女子蹦床
            //0:7-8岁；1：9-10岁；2：11-12岁
        }



        private async void ShowMessageInfo(string message)
        {
            Views.MessageDialog samMessageDialog = new Views.MessageDialog
            {
                Message = { Text = message }
            };
           await MaterialDesignThemes.Wpf.DialogHost.Show(samMessageDialog);
           
        }

        //显示添加框 并进行处理
        private async void ShowAddDialog()
        {
            //重置sportList
            sportEvents = new List<string>();
            sportEventsID = new List<string>();
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
                    if (select[i] == 1)
                    {
                        sportEvents.Add(Events[i]);
                        sportEventsID.Add(string.Format("{0:D2}", i + 1));
                    }
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
                    DataGridItem dataGridItem = new DataGridItem(athlete, sportEvents,sportEventsID);
                    _AthleteInfos.Add(dataGridItem);
            
                    Console.WriteLine("添加成功");
                    foreach (var q in sportEventsID)
                    {
                        Console.WriteLine(q);
                    }
                }
                else
                {
                    ShowMessageInfo("请完善信息");
                }
            }
        }
    }
}
