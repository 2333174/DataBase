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
        public string sportEvent { get; set; }
        public DataGridItem(Athlete athlete, string sportEvent)
        {
            this.Athlete = athlete;
            this.sportEvent = sportEvent;
        }
    }
}
