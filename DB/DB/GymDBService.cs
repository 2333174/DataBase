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
        public string GenerateRandomPassword(int passwordLength)
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
        public void AddLoginAccount(Login loginAccount)
        {
            using (var db = new GymDB())
            {
                db.login.Add(loginAccount);
                db.SaveChanges();
            }
        }
        
        // Add a team with teamname
        public void AddTeam(Team team)
        {
            using (var db = new GymDB())
            {
                db.team.Add(team);
                db.SaveChanges();
            }
        }

        // Add a staff to the team
        public void AddStaff(Staff staff)
        {
            using (var db = new GymDB())
            {
                var targetTeam = db.team.Find(staff.Tid);
                targetTeam.staff.Add(staff);
                db.staff.Add(staff);
                db.SaveChanges();
            }
        }

        // Add a teamresult to the team
        public void AddTeamResult(TeamResult teamresult)
        {
            using (var db = new GymDB())
            {
                var targetTeam = db.team.Find(teamresult.TID);
                targetTeam.teamresult.Add(teamresult);
                db.teamresult.Add(teamresult);
                db.SaveChanges();
            }
        }

        // Add an athlete to the team
        public void AddAthlete(Athlete athlete)
        {
            using (var db = new GymDB())
            {
                var targetTeam = db.team.Find(athlete.TID);
                targetTeam.athlete.Add(athlete);
                db.athlete.Add(athlete);
                db.SaveChanges();
            }
        }

        // Add personalresult to the athlete
        public void AddPersonalResult(PersonalResult personalResult)
        {
            using (var db = new GymDB())
            {
                var targetAthlete = db.athlete.Find(personalResult.AthleteID);
                var targetGroup = db.matchgroup.Find(personalResult.Groupid);
                targetAthlete.personalresult.Add(personalResult);
                targetGroup.personalresult.Add(personalResult);
                db.personalresult.Add(personalResult);
                db.SaveChanges();
            }
        }

        // Add match group to the judge
        public void AddMatchGroup(MatchGroup matchGroup)
        {
            using (var db = new GymDB())
            {
                var targetJudge = db.judge.Find(matchGroup.JudgeID);
                targetJudge.matchgroup.Add(matchGroup);
                db.matchgroup.Add(matchGroup);
                db.SaveChanges();
            }
        }

        // Add a judge
        public void AddJudge(Judge judge)
        {
            using (var db = new GymDB())
            {
                db.judge.Add(judge);
                db.SaveChanges();
            }
        }

        // Get the password by comparing username and role
        public string GetPassword(string username, int role)
        {
            using (var db = new GymDB())
            {
                var account = db.login.Where(l => l.UName.Equals(username) && l.Role == role).FirstOrDefault();
                return account.Password;
            }
        }
    }
}
