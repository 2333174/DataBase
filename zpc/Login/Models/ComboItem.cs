using DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login.Models
{
    class ComboItem
    {
        public ComboItem(Judge judge)
        {
            JudgeName = judge.Name;
            JudgeID = judge.JudgeID;
        }

        private string _JudgeName;

        public string JudgeName
        {
            get { return _JudgeName; }
            set { _JudgeName = value; }
        }

        private int _JudgeID;

        public int JudgeID
        {
            get { return _JudgeID; }
            set { _JudgeID = value; }
        }

    }
}
