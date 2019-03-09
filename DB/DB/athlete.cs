namespace DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("gymdb.athlete")]
    public partial class athlete
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public athlete()
        {
            peasonalresult = new HashSet<peasonalresult>();
        }

        [Required]
        [StringLength(20)]
        public string name { get; set; }

        [Key]
        [StringLength(18)]
        public string idNumber { get; set; }

        public sbyte age { get; set; }

        [Required]
        [StringLength(5)]
        public string gender { get; set; }

        [StringLength(20)]
        public string teamName { get; set; }

        [StringLength(18)]
        public string athleteID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<peasonalresult> peasonalresult { get; set; }
    }
}
