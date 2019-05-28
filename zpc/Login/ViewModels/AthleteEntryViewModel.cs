using DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login.ViewModels
{
    class AthleteEntryViewModel:BaseViewModel
    {
        private Athlete _Athlete;

        public Athlete Athlete
        {
            get { return _Athlete; }
            set
            {
                _Athlete = value;
                OnPropertyChanged();
            }
        }

        private List<PersonalResult> _Events;

        public List<PersonalResult> Events
        {
            get { return _Events; }
            set
            {
                _Events = value;
                OnPropertyChanged();
            }
        }

    }
}
