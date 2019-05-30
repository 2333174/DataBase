using System.Windows.Controls;

namespace Login.Views
{
    /// <summary>
    /// welcomePage.xaml 的交互逻辑
    /// </summary>
    public partial class welcomePage : Page
    {
        public int GudgeID { get; set; }
        public welcomePage(int GudgeID)
        {
            this.GudgeID = GudgeID;
            InitializeComponent();
            Client._welcomePage = this;
        }
    }
}
