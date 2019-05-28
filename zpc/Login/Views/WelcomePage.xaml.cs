using Custom;
using DB;
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
        public int GudgeID;
        public welcomePage(int GudgeID)
        {
            this.GudgeID = GudgeID;
            InitializeComponent();
        }
        public void isRec()
        {
            GymDBService dBService = new GymDBService();
            while (true)
            {
                if (Client1.GroupID != null)
                {
                    this.Content = new Frame
                    {
                        Content = new GradePage(GudgeID, dBService.GetGroupKeyByJG(GudgeID, Client1.GroupID))
                    };
                }
            }
        }
    }
}
