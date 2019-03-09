namespace DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("gymdb.judgelogin")]
    public partial class judgelogin
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(20)]
        public string judgeName { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(20)]
        public string password { get; set; }
    }
}
