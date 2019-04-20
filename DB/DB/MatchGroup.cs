namespace DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("gymdb.matchgroup")]
    public partial class MatchGroup
    {
        public MatchGroup() { }

        public MatchGroup(string groupID)
        {
            GroupID = groupID;
        }

        public MatchGroup(string groupID, int judgeID, int weight)
        {
            GroupID = groupID;
            JudgeID = judgeID;
            Weight = (sbyte)weight;
        }

        [Key]
        public int Key { get; set; }

        [Required]
        [StringLength(18)]
        public string GroupID { get; set; }

        public int? JudgeID { get; set; }

        public sbyte? Weight { get; set; }

        public virtual Judge judge { get; set; }
    }
}
