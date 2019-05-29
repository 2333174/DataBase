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
      
        public int TID { get; set; }

        [Required]
        [StringLength(20)]
        public string Event { get; set; }

        public short? Grade { get; set; }

        public short? Ranking { get; set; }

        [Key]
        public int TRid { get; set; }

        public virtual Team team { get; set; }
        public TeamResult()
        {

        }
        public TeamResult(int tid,string _event)
        {
            this.TID=tid;
            this.Event=Event;
            this.team = team;
        }
        public TeamResult(int tid, string _event,short? grade,Team team)
        {
            this.TID = tid;
            this.Event = _event;
            this.Grade=grade;
            this.team = team;
        }
    }
}
