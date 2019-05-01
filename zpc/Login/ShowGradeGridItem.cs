using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login
{
    class ShowGradeGridItem
    {
        //排名 姓名 号码 年龄 分数
        public float rank { get; set; }
        public string name { get; set; }
        public string atheleteID { get; set; }
        public string tname { get; set; }
        public float atheletegrade { get; set; }
        public string game { get; set; }
        public string type { get; set; }
        public sbyte suq { get; set; }
        public ShowGradeGridItem(float rank,string name, string atheleteID, sbyte suq, string tname,float atheletegrade, string game, string type)
        {
            this.rank = rank;
            this.name = name;
            this.atheleteID = atheleteID;
            this.suq = suq;
            this.tname = tname;
            this.atheletegrade = atheletegrade;
            this.game = game;
            this.type = type;
        }
    }
}
