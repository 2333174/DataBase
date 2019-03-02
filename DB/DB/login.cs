namespace DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("gymdb.login")]
    public partial class login
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(20)]
        public string User { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(20)]
        public string Password { get; set; }
    }
}
