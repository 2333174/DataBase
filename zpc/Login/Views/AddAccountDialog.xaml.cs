using System.Collections.Generic;
using System.Windows.Controls;
using DB;
using Login.Models;
using System.Windows;

namespace Login.Views
{
    /// <summary>
    /// AddAccountDialog.xaml 的交互逻辑
    /// </summary>
    public partial class AddAccountDialog : UserControl
    {
        public AddAccountDialog()
        {
            InitializeComponent();
            GymDBService dbs = new GymDBService();
            List<ComboItem> items = new List<ComboItem>();
            List<Judge> UnLoginjudges = dbs.GetUnLoginJudges();
            foreach (var n in UnLoginjudges)
                items.Add(new ComboItem(n));
            Name.ItemsSource = items;
            Name.DisplayMemberPath = "JudgeName";
            Name.SelectedValuePath = "JudgeID";
        }

        private void choosejudge(object sender, RoutedEventArgs e)
        {
            Name.Visibility = Visibility.Visible;
            TeamName.Visibility = Visibility.Hidden;
        }

        private void chooseTeam(object sender, RoutedEventArgs e)
        {
            Name.Visibility = Visibility.Hidden;
            TeamName.Visibility = Visibility.Visible;
        }
    }
}
