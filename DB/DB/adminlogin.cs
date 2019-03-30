namespace DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("gymdb.adminlogin")]
    public partial class adminlogin
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(20)]
        public string Admin { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(20)]
        public string Password { get; set; }
    }
}
