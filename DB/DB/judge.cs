namespace DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("gymdb.judge")]
    public partial class Judge
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Judge()
        {
            matchgroup = new HashSet<MatchGroup>();
            refereescore = new HashSet<RefereeScore>();
        }
        public Judge(string judgeName, string judgeIDnum, string judgeTel)
        {
            Name = judgeName;
            IDNumber = judgeIDnum;
            Telephone = judgeTel;
            matchgroup = new HashSet<MatchGroup>();
            refereescore = new HashSet<RefereeScore>();
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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MatchGroup> matchgroup { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RefereeScore> refereescore { get; set; }
    }
}
