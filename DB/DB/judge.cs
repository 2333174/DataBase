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

        [StringLength(18)]
        public string judgeID { get; set; }

        public sbyte weight { get; set; }
    }
}
