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
        public void AddLoginAccount(Login _loginAccount)
        {
            using (var db = new GymDB())
            {
                db.login.Add(_loginAccount);
                db.SaveChanges();
            }
        }
        
        // Add a team with teamname
        public void AddTeam(Team _team)
        {
            using (var db = new GymDB())
            {
                db.team.Add(_team);
                db.SaveChanges();
            }
        }

        // Add a staff to the team
        public void AddStaff(Staff _staff)
        {
            using (var db = new GymDB())
            {
                var targetTeam = db.team.Find(_staff.Tid);
                targetTeam.staff.Add(_staff);
                db.staff.Add(_staff);
                db.SaveChanges();
            }
        }

        // Add a teamresult to the team
        public void AddTeamResult(TeamResult _teamresult)
        {
            using (var db = new GymDB())
            {
                var targetTeam = db.team.Find(_teamresult.TID);
                targetTeam.teamresult.Add(_teamresult);
                db.teamresult.Add(_teamresult);
                db.SaveChanges();
            }
        }

        // Add an athlete to the team
        public void AddAthlete(Athlete _athlete)
        {
            using (var db = new GymDB())
            {
                var targetTeam = db.team.Find(_athlete.TID);
                targetTeam.athlete.Add(_athlete);
                db.athlete.Add(_athlete);
                db.SaveChanges();
            }
        }

        // Add personalresult to the athlete
        public void AddPersonalResult(PersonalResult _personalResult)
        {
            using (var db = new GymDB())
            {
                var targetAthlete = db.athlete.Find(_personalResult.AthleteID);
                var targetGroup = db.matchgroup.Find(_personalResult.Groupid);
                targetAthlete.personalresult.Add(_personalResult);
                targetGroup.personalresult.Add(_personalResult);
                db.personalresult.Add(_personalResult);
                db.SaveChanges();
            }
        }

        // Add match group to the judge
        public void AddMatchGroup(MatchGroup _matchGroup)
        {
            using (var db = new GymDB())
            {
                var targetJudge = db.judge.Find(_matchGroup.JudgeID);
                targetJudge.matchgroup.Add(_matchGroup);
                db.matchgroup.Add(_matchGroup);
                db.SaveChanges();
            }
        }

        // Add a judge
        public void AddJudge(Judge _judge)
        {
            using (var db = new GymDB())
            {
                db.judge.Add(_judge);
                db.SaveChanges();
            }
        }

        // Get the password by comparing username and role
        public string GetPassword(string _username, int _role)
        {
            using (var db = new GymDB())
            {
                var account = db.login.Where(l => l.UName.Equals(_username) && l.Role == _role).FirstOrDefault();
                return account.Password;
            }
        }

        public List<Athlete> GetAthlete(string _AthleteName)
        {
            using (var db = new GymDB())
            {
                var athletes = db.athlete.Where(a => a.Name.Equals(_AthleteName));
                return athletes.ToList();
            }
        }
    }
}
