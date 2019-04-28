using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using DB;
using MaterialDesignThemes.Wpf;

namespace Login.Views
{
    /// <summary>
    /// LoginPage.xaml 的交互逻辑
    /// </summary>
    public partial class LoginPage : Page
    {
        public int select { get; set; }
        public LoginPage(int s)
        {
            InitializeComponent();
            select = s;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ChangePage.Content = new Frame()
            { Content = new ChosePage() };
        }

        private void Loginf()
        {
            var db = new GymDBService();
            switch (db.Loginf(user.Text, password.Password, select))
            {
                case -1:
                    ShowMessageInfo("账户或密码有误");
                    break;
                case 0:
                    if (select==0){
                        string _TName = db.GetTName(user.Text, password.Password, select);
                        if (db.GetIsSignUp(_TName) == 0)
                        {
                            ChangePage.Content = new Frame()
                            { Content = new SignUpPage(_TName) };
                        }
                        else
                        {
                            ChangePage.Content = new Frame() { Content = new AthleteInfoPage(db.GetTIDByTName(_TName)) };
                        }
                    }
                    else {
                        ChangePage.Content = new Frame()
                        { Content = new ManagePage() };
                    }
                    break;
                case 1:
                    //ChangePage.Content = new Frame()
                    //{
                    //    Content = new GradePage(db.GetJudgeID(user.Text, password.Password))
                    //};
                    break;
                default:
                    ChangePage.Content = new Frame()
                    {
                        Content = new GSForMajorJudgePage(db.GetJudgeID(user.Text, password.Password).ToString())
                    };
                    break;
            }
        }

        private async void ShowMessageInfo(string message)
        {
            MessageDialog samMessageDialog = new MessageDialog
            {
                Message = { Text = message }
            };
            await DialogHost.Show(samMessageDialog);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(async () =>
            {
                await DialogHost.Show(new ProgressBox(), new DialogOpenedEventHandler((object Psender, DialogOpenedEventArgs args) =>
                {
                    Task.Delay(TimeSpan.FromSeconds(1))
                    .ContinueWith((t, _) => 
                    {
                        Loginf();
                        args.Session.Close(false);
                    }, null,
                    TaskScheduler.FromCurrentSynchronizationContext());
                }));
            });
            }
    }
}
