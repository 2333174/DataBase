namespace DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("gymdb.teamresult")]
    public partial class TeamResult
    {
        public TeamResult() { }

        public TeamResult(int teamid, string teamEvent)
        {
            TID = teamid;
            Event = teamEvent;
        }
        public int TID { get; set; }

        [Required]
        [StringLength(20)]
        public string Event { get; set; }

        public short? Grade { get; set; }

        public short? Ranking { get; set; }

        [Key]
        public int TRid { get; set; }

        public virtual Team team { get; set; }
    }
}
