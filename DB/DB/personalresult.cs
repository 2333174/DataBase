namespace DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("gymdb.personalresult")]
    public partial class personalresult
    {
        [Required]
        [StringLength(18)]
        public string AthleteID { get; set; }

        [Required]
        [StringLength(20)]
        public string SportsEvent { get; set; }

        [Required]
        [StringLength(18)]
        public string Gounpid { get; set; }

        public sbyte? Role { get; set; }

        public short? Bouns { get; set; }

        public short? Punishment { get; set; }

        public short? Grade { get; set; }

        public short? Ranking { get; set; }

        public bool? Suq { get; set; }

        [Key]
        public int PRid { get; set; }

        public virtual athlete athlete { get; set; }

        public virtual matchgroup matchgroup { get; set; }
    }
}
