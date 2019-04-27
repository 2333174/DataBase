using DB;
using System.Windows.Controls;

namespace Login.Views
{
    /// <summary>
    /// AthleteInfoControl.xaml 的交互逻辑
    /// </summary>
    public partial class AthleteInfoControl : UserControl
    {
        public AthleteInfoControl(Athlete athlete)
        {
            InitializeComponent();
            DataContext = new ViewModels.AthleteInfoViewModel(athlete);
        }
    }
}
