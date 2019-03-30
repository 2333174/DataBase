namespace DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("gymdb.peasonalresult")]
    public partial class peasonalresult
    {
        [Key]
        [Column(Order = 0, TypeName = "char")]
        [StringLength(18)]
        public string playID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(20)]
        public string SportsEvent { get; set; }

        [Key]
        [Column(Order = 2, TypeName = "char")]
        [StringLength(18)]
        public string Gounpid { get; set; }

        public short? Bouns { get; set; }

        public short? Punishment { get; set; }

        public short? Grade { get; set; }

        public short? Ranking { get; set; }

        public virtual athlete athlete { get; set; }

        public virtual matchgroup matchgroup { get; set; }
    }
}
