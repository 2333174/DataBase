namespace DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("gymdb.personalresult")]
    public partial class PersonalResult
    {
        public PersonalResult() { }

        public PersonalResult(string athleteID, string sportsEvent, string groupID, int role)
        {
            AthleteID = athleteID;
            SportsEvent = sportsEvent;
            Groupid = groupID;
            Role = (sbyte)role;
        }

        [Required]
        [StringLength(18)]
        public string AthleteID { get; set; }

        [Required]
        [StringLength(20)]
        public string SportsEvent { get; set; }

        [Required]
        [StringLength(18)]
        public string Groupid { get; set; }

        public sbyte Role { get; set; }

        public short? Bouns { get; set; }

        public short? Punishment { get; set; }

        public short? Grade { get; set; }

        public short? Ranking { get; set; }

        public sbyte Suq { get; set; }

        [Key]
        public int PRid { get; set; }

        public virtual Athlete Athlete { get; set; }

        public virtual MatchGroup matchgroup { get; set; }
    }
}
