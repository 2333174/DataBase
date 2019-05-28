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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PersonalResult()
        {
            refereescore = new HashSet<RefereeScore>();
        }
        public PersonalResult(string athleteID, string sportsEvent, string groupID, int role)
        {
            AthleteID = athleteID;
            SportsEvent = sportsEvent;
            GroupID = groupID;
            Role = (sbyte)role;
            refereescore = new HashSet<RefereeScore>();
        }

        public PersonalResult(string athleteID, string sportsEvent, int role)
        {
            AthleteID = athleteID;
            SportsEvent = sportsEvent;
            Role = (sbyte)role;
        }

        public PersonalResult(string athleteID, string sportsEvent, int role,int suq)
        {
            AthleteID = athleteID;
            SportsEvent = sportsEvent;
            Role = (sbyte)role;
            Suq = (sbyte)suq;
        }

        public PersonalResult(string athleteID, string sportsEvent, short grade, short ranking)
        {
            AthleteID = athleteID;
            SportsEvent = sportsEvent;
            Grade = grade;
            Ranking = ranking;
        }

        public PersonalResult(string athleteID, string athelteName, float grade)
        {
            AthleteID = athleteID;
            AthelteName = athelteName;
            Grade = grade;
        }

        [Required]
        [StringLength(18)]
        public string AthleteID { get; set; }

        [Required]
        [StringLength(18)]
        public string AthelteName { get; set; }

        [Required]
        [StringLength(20)]
        public string SportsEvent { get; set; }

        public sbyte Role { get; set; }

        [StringLength(18)]
        public string GroupID { get; set; }

        public short? Bouns { get; set; }

        public short? Punishment { get; set; }

        public short? Grade { get; set; }

        public short? Ranking { get; set; }

        public sbyte? Suq { get; set; }

        [Key]
        public int PRid { get; set; }

        public virtual Athlete athlete { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RefereeScore> refereescore { get; set; }
    }
}
