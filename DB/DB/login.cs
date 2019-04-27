namespace DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("gymdb.login")]
    public partial class Login
    {
        public Login() { }
        public Login(string userName, string userPwd, int userRole)
        {
            UName = userName;
            Password = userPwd;
            Role = userRole;
        }

        public Login(string userName, string userPwd, int userRole, int _judgeID)
        {
            UName = userName;
            Password = userPwd;
            Role = userRole;
            JudgeID = _judgeID;
        }

        public Login(string userName, string userPwd, int userRole, string _TName)
        {
            UName = userName;
            Password = userPwd;
            Role = userRole;
            TName = _TName;
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

        public int? JudgeID { get; set; }

        [StringLength(20)]
        public string TName { get; set; }
    }
}
