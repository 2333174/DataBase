using Custom;
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

namespace Login.Views
{
    /// <summary>
    /// welcomePage.xaml 的交互逻辑
    /// </summary>
    public partial class welcomePage : Page
    {
        public welcomePage(int GudgeID)
        {
            //Client client = new Client();
            //client.run(GudgeID.ToString());
            InitializeComponent();
        }
    }
}
