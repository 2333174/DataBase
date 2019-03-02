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
            //判断账号密码是否正确try
            try 
            {
                string sqlstr = "server = 192.168.154.70; uid = root; pwd = root; database = dbtest";
                MySqlConnection conn = new MySqlConnection(sqlstr);
                conn.Open();
                string UserNamestr = string.Format("SELECT * FROM Login where User = '{0}' and Password = {1};",
                    user.Text, password.Password);
                MySqlCommand comm = new MySqlCommand(UserNamestr, conn);
                MySqlDataReader dr = comm.ExecuteReader();
                if (dr.Read())
                {
                    string name = dr.GetString(dr.GetOrdinal("User"));
                    string pwd = dr.GetString(dr.GetOrdinal("Password"));
                    if (name == user.Text && pwd == password.Password)
                    {
                        MessageBox.Show("登录成功");
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

        //    if (user.Text.ToString() == "yjy" && password.Password.ToString() == "00000")
        //    {
        //        //正确，登陆成功
        //        if ("manage" == select)
        //        {
        //            ManageSystem manageWindow = new ManageSystem();
        //            manageWindow.ShowDialog();
        //            this.Hide();
        //        }
                
        //    }
        //    else
        //        MessageBox.Show("账号或密码有误！");
        }
    }
}
