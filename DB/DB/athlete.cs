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
            personalresult = new HashSet<personalresult>();
        }

        public athlete(string aname, string aidnum, int age, string gender)
        {
            Name = aname;
            IDNumber = aidnum;
            Age = age;
            Gender = gender;
            personalresult = new HashSet<personalresult>();
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

        public virtual team team { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<personalresult> personalresult { get; set; }
    }
}
