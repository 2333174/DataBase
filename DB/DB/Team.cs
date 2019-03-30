namespace DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("gymdb.team")]
    public partial class Team
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Team()
        {
            athlete = new HashSet<Athlete>();
            staff = new HashSet<Staff>();
            teamresult = new HashSet<TeamResult>();
        }
        public Team(string teamName, byte[] doc)
        {
            TName = teamName;
            Docs = doc;
            athlete = new HashSet<Athlete>();
            staff = new HashSet<Staff>();
            teamresult = new HashSet<TeamResult>();
        }


        [Key]
        public int TID { get; set; }

        [Required]
        [StringLength(20)]
        public string TName { get; set; }

        [Column(TypeName = "mediumblob")]
        public byte[] Docs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Athlete> athlete { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Staff> staff { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TeamResult> teamresult { get; set; }
    }
}
