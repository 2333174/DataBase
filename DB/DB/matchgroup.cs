namespace DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("gymdb.matchgroup")]
    public partial class MatchGroup
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MatchGroup()
        {
            personalresult = new HashSet<PersonalResult>();
        }

        public MatchGroup(string groupID, int judgeID, int weight)
        {
            GroupID = groupID;
            JudgeID = judgeID;
            Weight = (sbyte)weight;
            personalresult = new HashSet<PersonalResult>();
        }

        [Key]
        [StringLength(18)]
        public string GroupID { get; set; }

        public int JudgeID { get; set; }

        public sbyte Weight { get; set; }

        public virtual Judge judge { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonalResult> personalresult { get; set; }
    }
}
