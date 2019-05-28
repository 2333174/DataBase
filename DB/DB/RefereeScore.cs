namespace DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("gymdb.refereescore")]
    public partial class RefereeScore
    {
        public RefereeScore() { }

        public RefereeScore(int _Prid, int _judgeid)
        {
            PRid = _Prid;
            JudgeID = _judgeid;
        }
        [Key]
        public int Rsid { get; set; }

        public int? PRid { get; set; }

        public int? JudgeID { get; set; }

        public int? Scores { get; set; }

        public virtual Judge judge { get; set; }

        public virtual PersonalResult personalresult { get; set; }
    }
}
