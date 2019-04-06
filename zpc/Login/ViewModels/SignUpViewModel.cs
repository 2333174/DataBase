﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DB;
using Login.Commands;
using Login.Models;
using Login.Views;
using MaterialDesignThemes.Wpf;

namespace Login.ViewModels
{
    class SignUpViewModel:BaseViewModel
    {
        public SignUpViewModel(int Tid)
        {
            Teamid = Tid;
            _Athletes = null;
            DeleteItem = null;
            _AthleteInfos = new ObservableCollection<DataGridItem>();
            AddCommand = new Commands.DelegateCommand();
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
            DialogClosingEventHandler dialogClosingEventHandler=null;
            Views.MessageDialog samMessageDialog = new Views.MessageDialog
            {
                Message = { Text = message }
            };
            var result =await MaterialDesignThemes.Wpf.DialogHost.Show(samMessageDialog,dialogClosingEventHandler);
            Console.WriteLine(result.ToString());
        }

        //显示添加框 并进行处理
        private async void ShowAddDialog()
        {
            DialogClosingEventHandler dialogClosingEventHandler = null;
            AddAthleteDialog samMessageDialog = new AddAthleteDialog
            {
            };
            var result = await MaterialDesignThemes.Wpf.DialogHost.Show(samMessageDialog, dialogClosingEventHandler);
            Console.WriteLine(result.ToString());
            if (Equals(result,true))
            {
                if (!(string.IsNullOrWhiteSpace(samMessageDialog.ID.Text)
                    || string.IsNullOrWhiteSpace(samMessageDialog.Name.Text)
                    || string.IsNullOrWhiteSpace(samMessageDialog.Age.Text)
                    || string.IsNullOrWhiteSpace(samMessageDialog.PhoneNum.Text))
                    && (samMessageDialog.Gender.SelectedValue != null) && (samMessageDialog.SportEvent.SelectedValue != null))
                {
                    Athlete athlete = new Athlete(samMessageDialog.Name.Text, samMessageDialog.ID.Text, int.Parse(samMessageDialog.Age.Text), samMessageDialog.Gender.Text);
                    DataGridItem dataGridItem = new DataGridItem(athlete, samMessageDialog.SportEvent.SelectedValue.ToString());
                    AthleteInfos.Add(dataGridItem);
                    Console.WriteLine("添加成功");
                }
            }
        }
    }
}