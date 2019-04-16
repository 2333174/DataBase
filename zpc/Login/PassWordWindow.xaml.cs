﻿using System;
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
        //0报名,1管理,2打分
        public int select { get; set; }
        public PassWordWindow()
        {
            InitializeComponent();
           
        }
        public PassWordWindow(int s)
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
            //数据库判断账号密码是否正确try
            try 
            {
                string sqlstr = "server = 192.168.189.1; uid = root; pwd = root; database = gymdb";
                MySqlConnection conn = new MySqlConnection(sqlstr);
                conn.Open();
                string UserNamestr = string.Format("SELECT * FROM Login where UName = '{0}' and Password = '{1}'and Role='{2}' ;",
                    user.Text, password.Password,select.ToString());
                Console.WriteLine(password .Password );
                MySqlCommand comm = new MySqlCommand(UserNamestr, conn);
                MySqlDataReader dr = comm.ExecuteReader();
                if (dr.Read())
                {
                    string name = dr.GetString(dr.GetOrdinal("UName"));
                    string pwd = dr.GetString(dr.GetOrdinal("Password"));
                    string  role = dr.GetString(dr.GetOrdinal("Role"));
                    if (name.Equals(user.Text) && pwd.Equals(password.Password)&&role.Equals(select.ToString()))
                    {
                        //MessageBox.Show("登录成功");
                        if (1 == select)
                        {
                            this.Close();
                            ManageSystem manageWindow = new ManageSystem();
                            manageWindow.ShowDialog();
                        }
                        if (2 == select)
                        {
                            string weight = dr.GetString(dr.GetOrdinal("Weight"));//weight为0是裁判，为1是主裁判
                            if (weight=="0")
                            {
                                this.Close();
                                GradeSystem gradeSystem = new GradeSystem();
                                gradeSystem.ShowDialog();
                            }
                            else
                            {
                                this.Close();
                                GSForMajorJudge gSForMajorJudge = new GSForMajorJudge();
                                gSForMajorJudge.ShowDialog();
                            }
                        }
                        if(0 ==select)
                        {
                            //打开打分系统并关闭本窗口
                            this.Close();
                            Window1 signup = new Window1();
                            signup.ShowDialog();
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

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            Close();
            mainWindow.ShowDialog();
        }
    }
}