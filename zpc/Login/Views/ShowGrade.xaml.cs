using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Collections.ObjectModel;
using System.Windows.Shapes;
using DB;

namespace Login.Views
{
    /// <summary>
    /// ShowGrade.xaml 的交互逻辑
    /// </summary>
    public partial class ShowGrade : Page
    {
        //ObservableCollection<string> iteminform { get; set; }
        ObservableCollection<ShowGradeGridItem> items { get; set; }
        //ShowGradeGridItem item { get; set; }
        GymDB db = new GymDB();
        GymDBService gymDBService = new GymDBService();

        public ShowGrade(string groupID, string game,string type)
        {
            InitializeComponent();
            items = new ObservableCollection<ShowGradeGridItem>();
            //iteminform = new ObservableCollection<string>();
            //this.iteminform.Add(game);
            //this.iteminform.Add(type);
            //根据构造函数传过来的参数设置两个文本框的文本
            Game.Text = game;
            Game.IsReadOnly = true;  //设置为只读
            Type.Text = type;
            Type.IsReadOnly = true;
            //给该组成绩排序
            gymDBService.Ranking(gymDBService.GetPersonalResultsByGroupID(groupID));
            List<PersonalResult> prs = gymDBService.GetPersonalResultsByGroupID(groupID);
            foreach (PersonalResult p in prs)
            {
                //获得Athlete
                Athlete athlete = gymDBService.GetAthleteByID(p.AthleteID);
                //获得运动员姓名
                string athName = athlete.Name;
                //获得Athlete所在的Team
                Team team = gymDBService.GetTeamByTID((int)athlete.TID);
                //获得队名
                string tName = team.TName;
                //获得出场循序 即号码
                sbyte suq = (sbyte)p.Suq;
                //获得分数
                short grade = (short)p.Grade;
                //获得排名
                short rank = (short)p.Ranking;
                ShowGradeGridItem showGradeGridItem = new ShowGradeGridItem(rank, athName, p.AthleteID,suq, tName, grade, game, type);
                items.Add(showGradeGridItem);
            }
            
            gradegrid.ItemsSource = items;
        }

    }
}
