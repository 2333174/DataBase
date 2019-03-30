namespace DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("gymdb.teamlogin")]
    public partial class teamlogin
    {
        [Required]
        [StringLength(20)]
        public string name { get; set; }

        [Key]
        [StringLength(20)]
        public string account { get; set; }

        [Required]
        [StringLength(20)]
        public string password { get; set; }
    }
}
