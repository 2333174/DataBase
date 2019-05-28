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
        [Key]
        [Column(Order = 0)]
        [StringLength(20)]
        public string name { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(18)]
        public string idNumber { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(15)]
        public string telephone { get; set; }
    }
}
