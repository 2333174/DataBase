namespace DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("gymdb.team")]
    public partial class team
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public team()
        {
            athlete = new HashSet<athlete>();
            staff = new HashSet<staff>();
            teamresult = new HashSet<teamresult>();
        }

        public team(string teamName)
        {
            TName = teamName;
            athlete = new HashSet<athlete>();
            staff = new HashSet<staff>();
            teamresult = new HashSet<teamresult>();
        }

        [Key]
        public int TID { get; set; }

        [Required]
        [StringLength(20)]
        public string TName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<athlete> athlete { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<staff> staff { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<teamresult> teamresult { get; set; }
    }
}
