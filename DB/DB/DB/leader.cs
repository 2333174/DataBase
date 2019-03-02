namespace DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("gymdb.leader")]
    public partial class leader
    {
        [Required]
        [StringLength(20)]
        public string name { get; set; }

        [Key]
        [Column(TypeName = "char")]
        [StringLength(18)]
        public string idNumber { get; set; }

        [Required]
        [StringLength(15)]
        public string telephone { get; set; }
    }
}
