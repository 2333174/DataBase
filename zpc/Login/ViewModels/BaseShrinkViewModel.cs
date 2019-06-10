using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Login.ViewModels
{
    class BaseShrinkViewModel : BaseViewModel
    {
        private Visibility isVisibility;

        public Visibility IsVisibility
        {
            get { return isVisibility; }
            set
            {
                isVisibility = value;
                OnPropertyChanged();
            }
        }

        public BaseShrinkViewModel()
        {
            IsVisibility = Visibility.Collapsed;
        }
    }
}
