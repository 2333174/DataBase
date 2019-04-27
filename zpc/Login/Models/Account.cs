using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login.Models
{
    class Account
    {
        public string userName;
        public string password;
        public string accountRole;
        public string name;
        public Account(string _userName, string _password, string _accountRole, string _name)
        {
            userName = _userName;
            password = _password;
            accountRole = _accountRole;
            name = _name;
        }
    }
}
