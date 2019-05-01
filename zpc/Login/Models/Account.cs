using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login.Models
{
    public class Account
    {
        public string userName { get; set; }
        public string password { get; set; }
        public string accountRole { get; set; }
        public string name { get; set; }
        public Account(string _userName, string _password, string _accountRole, string _name)
        {
            userName = _userName;
            password = _password;
            accountRole = _accountRole;
            name = _name;
        }
    }
}
