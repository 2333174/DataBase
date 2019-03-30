namespace DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("gymdb.setting")]
    public partial class Setting
    {
        public Setting(int pone,int ptwo,int pthree)
        {
            Pone = pone;
            Ptwo = ptwo;
            Pthree = pthree;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Pone { get; set; }

        public int? Ptwo { get; set; }

        public int? Pthree { get; set; }
    }
}
