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
using MaterialDesignThemes.Wpf;

namespace Login.Views
{
    /// <summary>
    /// SignUpPage.xaml 的交互逻辑
    /// </summary>
    public partial class SignUpPage : Page
    {
        bool close;
        public SignUpPage(int Tid)
        {
            InitializeComponent();
            DataContext = new ViewModels.SignUpViewModel(Tid);
        }
        private async void ShowAddDialog()
        {
            
            AddAthleteDialog samMessageDialog = new AddAthleteDialog
            {
            };
            await DialogHost.Show(samMessageDialog);
            if (close)
            {
                if (!(string.IsNullOrWhiteSpace(samMessageDialog.ID.Text)
                    || string.IsNullOrWhiteSpace(samMessageDialog.Name.Text)
                    || string.IsNullOrWhiteSpace(samMessageDialog.PhoneNum.Text))
                    &&(samMessageDialog.Gender.SelectedValue!=null)&&(samMessageDialog.SportEvent.SelectedValue!=null))
                {

                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ShowAddDialog();
            
        }

        private void Add_DialogClosing(object sender, DialogClosingEventArgs eventArgs)
        {
            close = Equals(eventArgs.Parameter, true);
            Console.WriteLine(close);
        }
    }
}
