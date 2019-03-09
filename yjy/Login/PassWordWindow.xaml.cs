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
using System.Windows.Shapes;
using MySql.Data.MySqlClient;

namespace Login
{
    /// <summary>
    /// PassWordWindow.xaml 的交互逻辑
    /// </summary>
    public partial class PassWordWindow : Window
    {
        public string select { get; set; }
        public PassWordWindow()
        {
            InitializeComponent();
           
        }
        public PassWordWindow(string s)
        {
            select = s;
            InitializeComponent();
            //设置背景图片
            Uri uri = new Uri(@"images/login.jpg", UriKind.Relative);
            ImageBrush ib = new ImageBrush();
            ib.ImageSource = new BitmapImage(uri);
            this.Background = ib;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //若用户没有输入账号密码就提示用户
            if (user.Text.Length==0 && password.Password.Length==0)
            {
                MessageBox.Show("请输入账号和密码！");
            }
            if (user.Text.Length==0 && password.Password.Length!=0)
            {
                MessageBox.Show("请输入账号！");
            }
            if (user.Text.Length!=0&&password.Password.Length==0)
            {
                MessageBox.Show("请输入密码！");
            }
            //数据库判断账号密码是否正确try
            try 
            {
                string sqlstr = "server = 192.168.154.70; uid = root; pwd = root; database = gymdb";
                MySqlConnection conn = new MySqlConnection(sqlstr);
                conn.Open();
                string UserNamestr = string.Format("SELECT * FROM Login where User = '{0}' and Password = '{1}';",
                    user.Text, password.Password);
                Console.WriteLine(password .Password );
                MySqlCommand comm = new MySqlCommand(UserNamestr, conn);
                MySqlDataReader dr = comm.ExecuteReader();
                if (dr.Read())
                {
                    string name = dr.GetString(dr.GetOrdinal("User"));
                    string pwd = dr.GetString(dr.GetOrdinal("Password"));
                    if (name.Equals(user.Text) && pwd.Equals(password.Password))
                    {
                        //MessageBox.Show("登录成功");
                        if ("manage" == select)
                        {
                            ManageSystem manageWindow = new ManageSystem();
                            manageWindow.ShowDialog();
                            this.Hide();
                        }
                        //if ("grade" == select)
                        //{
                        //    if (账号是小组裁判)
                        //    {
                        //        GradeSystem gradeSystem = new GradeSystem();
                        //        gradeSystem.ShowDialog();
                        //        this.Hide();
                        //    }
                        //    else
                        //    {
                        //        GSForMajorJudge gSForMajorJudge = new GSForMajorJudge();
                        //        gSForMajorJudge.ShowDialog();
                        //        this.Hide();
                        //    }
                        //}
                        if("signup"==select)
                        {
                            //打开打分系统并关闭本窗口
                            this.Hide();
                        }
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("用户名或密码错误，请检查后输入！", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString());
            }
            
        }
    }
}
