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
        public short? rank { get; set; }
        public string name { get; set; }
        public string atheleteID { get; set; }
        public string tname { get; set; }
        public int atheletegrade { get; set; }
        public int teamgrade { get; set; }
        public string game { get; set; }
        public string type { get; set; }
        public sbyte suq { get; set; }
        public string groupid { get; set; }
        
        public ShowGradeGridItem(short? rank,string name, string atheleteID, sbyte suq, string tname,int atheletegrade, string game, string type)
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
        public ShowGradeGridItem(string game, short? rank, string tname, int teamgrade)
        {
            this.game = game;
            this.rank = rank;
            this.tname = tname;
            this.teamgrade = teamgrade;
        }
        public ShowGradeGridItem(string game,string name, string groupid,sbyte suq)
        {
            this.groupid = groupid;
            this.name = name;
            this.suq = suq;
            this.game = game;
        }
        public ShowGradeGridItem(string name, string atheleteID,string game, int atheletegrade, short rank)
        {
            
            this.name = name;
            this.atheleteID = atheleteID;
            this.game = game;
            this.atheletegrade = atheletegrade;
            this.rank = rank;
        }
    }
}
