using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login
{
    class TeamResultGridItem
    {
        public string project
        {
            get;set;
        }
        public short rank
        {
            get; set;
        }
        public string team
        {
            get; set;
        }
        public float grade
        {
            get;set;
        }
        public TeamResultGridItem(string project, short rank, string team, float grade)
        {
            this.project = project;
            this.rank = rank;
            this.team = team;
            this.grade = grade;
        }
    }
}
