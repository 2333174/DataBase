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
using System.Windows.Forms;
using System.IO;
namespace Login
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DataGrid_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DataGrid_SelectionChanged_2(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialogTemp = new OpenFileDialog();
            DialogResult dr = openFileDialogTemp.ShowDialog();
            string address = "10.132.89.16/FileSave.aspx";//服务器地址
            string fileNamePath;//上传的本地文件路径
            string saveName;//要保存的文件名称
            //ProgressBar progressBar;
            if (dr.ToString()=="OK")
            {
                saveName = openFileDialogTemp.SafeFileName; //获取文件名和扩展名
                fileNamePath = openFileDialogTemp.FileName; //全路径
                file.Text = fileNamePath;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(file.Text))
            {
                UploadFile(file.Text);
                System.Windows.MessageBox.Show("OK!");
                file.Text = string.Empty;
            }
            else
                System.Windows.MessageBox.Show("请选择文件！");
        }
        private void UploadFile(string filename)
        {
            //将所选文件的读入字节数组img
            byte[] text = File.ReadAllBytes(file.Text);
            string fileName = System.IO.Path.GetFileNameWithoutExtension(file.Text);
            //file.Text.Substring(file.Text.LastIndexOf('\\')+1);
            //Team team = new Team()
            //{
            //    docs = text,//将数据写入数据库
            //    name = fileName
            //};
            //db.Team.Insert(team);
            //db.SubmitChanges();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.Close();
            mainWindow.Show();
        }
    }
}
