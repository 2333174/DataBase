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
        public string name { get; set; }

        [Column(TypeName = "char")]
        [Required]
        [StringLength(18)]
        public string idNumber { get; set; }

        [Required]
        [StringLength(15)]
        public string telephone { get; set; }

        public int judgeID { get; set; }

        public int weight { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<matchgroup> matchgroup { get; set; }
    }
}
