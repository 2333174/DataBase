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
        [Required]
        [StringLength(20)]
        public string name { get; set; }

        [Key]
        [Column(TypeName = "char")]
        [StringLength(18)]
        public string idNumber { get; set; }

        public sbyte age { get; set; }

        [Required]
        [StringLength(20)]
        public string group { get; set; }

        [StringLength(20)]
        public string sportsEvent { get; set; }

        public sbyte? order { get; set; }
    }
}
