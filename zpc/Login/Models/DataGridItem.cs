using DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login.Models
{
    class DataGridItem
    {
        public Athlete Athlete{ get; set; }
        public List<string>  sportEvent { get; set; }
        public  List<string> sportEventsID { get; set; }
        public DataGridItem(Athlete athlete, List<string> sportEvent, List<string> sportEventsID)
        {
            this.Athlete = athlete;
            this.sportEvent = sportEvent;
            this.sportEventsID = sportEventsID;
        }
    }
}
