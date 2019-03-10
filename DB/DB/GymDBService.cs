using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace DB
{
    // Database related operations
    class GymDBService
    {
        // character set for generating random password
        private string[] CharacterSet = { "ABCDEFGHIJKLMNOPQRSTUVWXYZ",
            "abcdefghijklmnopqrstuvwxyz", "1234567890", ",./;:[]{}!@#$%&*()~" };

        // operation for generating random password
        public string GetRandomPassword(int passwordLength)
        {
            string tmpPwd = string.Empty;
            int randomNum1, randomNum2;
            Random random = new Random();
            for(int i = 0; i < passwordLength; i++)
            {
                randomNum1 = random.Next(CharacterSet.Length);
                randomNum2 = random.Next(CharacterSet[randomNum1].Length);
                tmpPwd += CharacterSet[randomNum1][randomNum2];
            }
            Thread.Sleep(20);
            return tmpPwd;
        }

        // Add an login account to the database
        public void AddLoginAccount(login loginAccount)
        {
            using (var db = new GymDB())
            {
                db.login.Add(loginAccount);
                db.SaveChanges();
            }
        }
        
    }
}
