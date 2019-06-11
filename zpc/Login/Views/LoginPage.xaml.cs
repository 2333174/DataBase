using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using DB;
using MaterialDesignThemes.Wpf;

namespace Login.Views
{
    /// <summary>
    /// LoginPage.xaml 的交互逻辑
    /// </summary>
    public partial class LoginPage : Page
    {
        public delegate void DeleFunc(string s);
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
                    ShowMessageInfo("账户或密码有误",Login2);
                    break;
                case 0:
                    if (select==0){
                        string _TName = db.GetTName(user.Text, password.Password, select);
                        if (db.GetIsSignUp(_TName) == 0)
                        {
                            ChangePage.Content = new Frame()
                            { Content = new SignUpPage(_TName) };
                            base.Title = "队伍注册";
                        }
                        else
                        {
                            ChangePage.Content = new Frame() { Content = new AthleteInfoPage(db.GetTIDByTName(_TName)) };
                            Title = "队伍信息";
                        }
                    }
                    else if (select==1){
                        try
                        {
                            Client.run("管理");
                            ChangePage.Content = new Frame()
                            { Content = new ManagePage() };
                            Title = "管理系统";
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            throw e;
                        }
                        
                    }
                    else
                    {
                        try
                        {
                            Client.run(db.GetJudgeID(user.Text, password.Password).ToString());
                            Title = "打分系统";
                            ChangePage.Content = new Frame()
                            {
                                Content = new welcomePage(db.GetJudgeID(user.Text, password.Password))
                            };
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            throw e;
                        }
                    }
                    break;
            }
        }

        private async void ShowMessageInfo(string message, DialogHost dialog)
        {
            MessageDialog samMessageDialog = new MessageDialog
            {
                Message = { Text = message }
            };
            await dialog.ShowDialog(samMessageDialog);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(async () =>
            {
                await Login.ShowDialog(new ProgressBox(), new DialogOpenedEventHandler((object Psender, DialogOpenedEventArgs args) =>
                {
                    Task.Delay(TimeSpan.FromSeconds(1))
                    .ContinueWith((t, _) =>
                    {
                        try
                        {
                            Loginf();
                            args.Session.Close(false);
                        }
                        catch (Exception)
                        {
                            args.Session.Close(false);
                            ShowMessageInfo("登陆失败!",Login2);
                        }
                    }, null,
                    TaskScheduler.FromCurrentSynchronizationContext());
                }));
            });
        }
    }
}
