namespace DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("gymdb.matchgroup")]
    public partial class matchgroup
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public matchgroup()
        {
            peasonalresult = new HashSet<peasonalresult>();
        }

        [Key]
        [StringLength(18)]
        public string gounpID { get; set; }

        public int judge { get; set; }

        public sbyte Weight { get; set; }

        public virtual judge judge1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<peasonalresult> peasonalresult { get; set; }
    }
}
