namespace DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("gymdb.athlete")]
    public partial class Athlete
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Athlete()
        {
            personalresult = new HashSet<PersonalResult>();
        }
        public Athlete(string athleteName, string athleteIDnum, int athleteAge, string athleteGender)
        {
            Name = athleteName;
            IDNumber = athleteIDnum;
            Age = athleteAge;
            Gender = athleteGender;
            personalresult = new HashSet<PersonalResult>();
        }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        [Key]
        [StringLength(18)]
        public string IDNumber { get; set; }

        public int Age { get; set; }

        [Required]
        [StringLength(5)]
        public string Gender { get; set; }

        public int? TID { get; set; }

        [StringLength(3)]
        public string AthleteID { get; set; }

        public int? CulturalGrade { get; set; }

        public virtual Team team { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonalResult> personalresult { get; set; }
    }
}
