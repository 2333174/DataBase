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
            personalresult = new HashSet<personalresult>();
        }

        public matchgroup(string groupID, int judgeID, int weight)
        {
            GroupID = groupID;
            JudgeID = judgeID;
            Weight = (sbyte)weight;
            personalresult = new HashSet<personalresult>();
        }

        [Key]
        [StringLength(18)]
        public string GroupID { get; set; }

        public int JudgeID { get; set; }

        public sbyte Weight { get; set; }

        public virtual judge judge { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<personalresult> personalresult { get; set; }
    }
}
