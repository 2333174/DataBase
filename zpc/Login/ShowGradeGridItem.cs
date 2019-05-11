using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login
{
    class ShowGradeGridItem
    {
        private string athName;
        private object rank1;

        //排名 姓名 号码 年龄 分数
        public short? rank { get; set; }
        public string name { get; set; }
        public string atheleteID { get; set; }
        public string tname { get; set; }
        public float atheletegrade { get; set; }
        public string game { get; set; }
        public string type { get; set; }
        public sbyte suq { get; set; }
        public ShowGradeGridItem(short? rank,string name, string atheleteID, sbyte suq, string tname,float atheletegrade, string game, string type)
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
        public ShowGradeGridItem(short? rank, string name, sbyte suq, string game)
        {
            this.rank = rank;
            this.name = name;
            this.suq = suq;
            this.game = game;
        }
        public ShowGradeGridItem(string name, string atheleteID,float atheletegrade, short rank)
        {
            
            this.name = name;
            this.atheleteID = atheleteID;
            this.atheletegrade = atheletegrade;
            this.rank = rank;
        }

        public ShowGradeGridItem(string athName, string atheleteID, float atheletegrade, object rank1)
        {
            this.athName = athName;
            this.atheleteID = atheleteID;
            this.atheletegrade = atheletegrade;
            this.rank1 = rank1;
        }
    }
}
