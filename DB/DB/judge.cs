namespace DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("gymdb.judge")]
    public partial class judge
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public judge()
        {
            matchgroup = new HashSet<matchgroup>();
        }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        [Column(TypeName = "char")]
        [Required]
        [StringLength(18)]
        public string IDNumber { get; set; }

        [Required]
        [StringLength(15)]
        public string Telephone { get; set; }

        public int JudgeID { get; set; }

        public int Weight { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<matchgroup> matchgroup { get; set; }
    }
}
