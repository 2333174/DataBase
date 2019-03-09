namespace DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("gymdb.teamresult")]
    public partial class teamresult
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(18)]
        public string teamName { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(20)]
        public string Event { get; set; }

        public short? Grade { get; set; }

        public short? Ranking { get; set; }
    }
}
