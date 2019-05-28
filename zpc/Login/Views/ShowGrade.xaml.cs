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
        ObservableCollection<string> iteminform { get; set; }
        ObservableCollection<ShowGradeGridItem> items { get; set; }
        ShowGradeGridItem item { get; set; }
        GymDB db = new GymDB();
        GymDBService gymDBService = new GymDBService();

        public ShowGrade(string name, string atheleteID, string groupname, float atheletegrade,string game,string type)
        {
            InitializeComponent();
            items = new ObservableCollection<ShowGradeGridItem>();
            iteminform = new ObservableCollection<string>();
            this.iteminform.Add(game);
            this.iteminform.Add(type);

            //初始化datagrid
            this.item = new ShowGradeGridItem(0, name,  atheleteID,  groupname,  atheletegrade,null,null);
            this.items.Add(item);
            for (int i = 0; i < items.Count; i++)//将运动员按成绩排序
            {
                for (int j = 0; j < items.Count - 1 - i; j++)
                {
                    if (items[j].atheletegrade > items[j + 1].atheletegrade)
                    {
                        ShowGradeGridItem temp = items[j];
                        items[j] = items[j + 1];
                        items[j + 1] = temp;
                    }
                }
            }
            for (int i = 0; i < items.Count; i++)
            {
                items[i].rank = i + 1;
            }
            gradegrid.ItemsSource = items;
        }

        private void Item_TextChanged(object sender, TextChangedEventArgs e)
        {
            Game.Text = iteminform.First <string>();
        }

        private void Type_TextChanged(object sender, TextChangedEventArgs e)
        {
            Game.Text = iteminform.Last <string>();
        }
    }
}
