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
        public login(string userName, string userPwd, int userRole)
        {
            UName = userName;
            Password = userPwd;
            Role = userRole;
        }

        public login(string userName, string userPwd, int userRole, int userWeight)
        {
            UName = userName;
            Password = userPwd;
            Role = userRole;
            Weight = (sbyte?)userWeight;
        }

        [Key]
        public int UID { get; set; }

        [Required]
        [StringLength(18)]
        public string UName { get; set; }

        [Required]
        [StringLength(18)]
        public string Password { get; set; }

        public int Role { get; set; }

        public sbyte? Weight { get; set; }
    }
}
