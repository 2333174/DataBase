namespace DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("gymdb.staff")]
    public partial class staff
    {
        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        [Key]
        [StringLength(18)]
        public string IDNumber { get; set; }

        [Required]
        [StringLength(2)]
        public string Gender { get; set; }

        [Required]
        [StringLength(15)]
        public string Telephone { get; set; }

        [Required]
        [StringLength(5)]
        public string Role { get; set; }

        public int Tid { get; set; }

        public virtual team team { get; set; }
    }
}
