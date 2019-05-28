using System.Windows.Controls;
using System.Windows.Media;
using Custom;
using Login.ViewModels;

namespace Login.Views
{
    /// <summary>
    /// GSForMajorJudgePage.xaml 的交互逻辑
    /// </summary>
    public partial class GSForMajorJudgePage : Page
    {
        
        private GSFMPViewModel viewModel;

        public GSForMajorJudgePage(string groupid)
        {
            InitializeComponent();
            viewModel = new GSFMPViewModel(groupid);
            DataContext = viewModel;
           
        }

        private void Expander_Expanded(object sender, System.Windows.RoutedEventArgs e)
        {
            viewModel.DetailExpanded(PrimaryTable.SelectedIndex);
        }

        private void Expander_Collapsed(object sender, System.Windows.RoutedEventArgs e)
        {
            viewModel.DetailCollapsed(PrimaryTable.SelectedIndex);
        }
    }
}
