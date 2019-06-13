using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login
{
    class Manage_DataGridRow
    {
        //项目 小组编号 代表队 运动员
        public string project { get; set; }
        public string groupID { get; set; }
        public string team { get; set; }
        public string athlete { get; set; }
        public Manage_DataGridRow(string project,string groupID,string team,string athlete)
        {
            this.project = project;
            this.groupID = groupID;
            this.team = team;
            this.athlete = athlete;
        }
        public Manage_DataGridRow(string project, string groupID)
        {
            this.project = project;
            this.groupID = groupID;
        }
    }
}
