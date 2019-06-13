using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login
{
    class Manage_DataGridRow
    {
        //项目 小组编号 代表队
        public string project { get; set; }
        public string groupID { get; set; }
        public string team { get; set; }

        public Manage_DataGridRow(string project,string groupID,string team)
        {
            this.project = project;
            this.groupID = groupID;
            this.team = team;
        }

        public Manage_DataGridRow(string project, string groupID)
        {
            this.project = project;
            this.groupID = groupID;
        }

        public override bool Equals(object obj)
        {
            Manage_DataGridRow tmp = (Manage_DataGridRow)obj;
            if (project.Equals(tmp.project) && groupID.Equals(tmp.groupID) && team.Equals(tmp.team))
                return true;
            return false;
        }
    }
}
