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
        public int UID { get; set; }

        [Required]
        [StringLength(18)]
        public string UName { get; set; }

        public int Role { get; set; }

        [Required]
        [StringLength(18)]
        public string Password { get; set; }

        public sbyte? Weight { get; set; }
    }
}
