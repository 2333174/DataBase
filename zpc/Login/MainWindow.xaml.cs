using Login.Views;
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
using System.Windows.Shapes;

namespace Login
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ChangePage.Content = new Frame()
            { Content = new ChosePage() };
            DB.GymDBService dbs = new DB.GymDBService();
            List<DB.PersonalResult> prs = new List<DB.PersonalResult>();
            using (var db = new DB.GymDB())
                prs = db.personalresult.ToList();
            dbs.Ranking(prs);
            ChangePage.Content = new Frame()
            { Content = new ManagePage()};
        }
        
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            System.Windows.Application.Current.Shutdown();
        }
    }
}
