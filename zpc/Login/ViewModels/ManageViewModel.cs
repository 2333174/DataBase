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
    class ManageViewModel:BaseViewModel
    {

        public ManageViewModel()
        {
            prerows = new List<Manage_DataGridRow>();
            finalrows = new List<Manage_DataGridRow>();
        }



        //命令
        public DelegateCommand AddDataGridCommand { get; set; } //添加Datagrid数据
        public DelegateCommand DeleteDataGridCommand { get; set; }//删除Datagrid数据
        public DelegateCommand AddCommand { get; set; }

        List<Manage_DataGridRow> prerows;//初赛裁判
        List<Manage_DataGridRow> finalrows;//复赛裁判

    }
}
