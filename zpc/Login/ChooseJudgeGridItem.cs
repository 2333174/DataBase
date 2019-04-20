using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Login
{
    class ChooseJudgeGridItem
    {

        //项目 小组编号 总裁判 分裁判
        public string project { get; set; }
        public string groupID { get; set; }
        public string mainJudge { get; set; }
        public string groupJudges { get; set; }
        public ChooseJudgeGridItem(string project, string groupID, string mainJudge, string groupJudges)
        {
            this.project = project;
            this.groupID = groupID;
            this.mainJudge = mainJudge;
            this.groupJudges = groupJudges;
        }
    }
}
