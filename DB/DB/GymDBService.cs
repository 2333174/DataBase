using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Data.Entity;

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
        public void Add(Login _loginAccount)
        {
            using (var db = new GymDB())
            {
                db.login.Add(_loginAccount);
                db.SaveChanges();
            }
        }
        
        // Add a team with teamname
        public void Add(Team _team)
        {
            using (var db = new GymDB())
            {
                db.team.Add(_team);
                db.SaveChanges();
            }
        }

        // Add a staff to the team
        public void Add(Staff _staff)
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
        public void Add(TeamResult _teamresult)
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
        public void Add(Athlete _athlete)
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
        public void Add(PersonalResult _personalResult)
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
        public void Add(MatchGroup _matchGroup)
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
        public void Add(Judge _judge)
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

        public List<Athlete> GetAthletesByName(string _AthleteName)
        {
            using (var db = new GymDB())
            {
                var athletes = db.athlete.Where(a => a.Name.Equals(_AthleteName));
                return athletes.ToList();
            }
        }

        public Athlete GetAthleteByID(string _AthleteID)
        {
            using (var db = new GymDB())
                return db.athlete.Find(_AthleteID);
        }

        public List<PersonalResult> GetPersonalResultsByAthleteID(string _AthleteID)
        {
            using (var db = new GymDB())
                return db.personalresult.Where(p => p.AthleteID.Equals(_AthleteID)).ToList();
        }

        public List<PersonalResult> GetPersonalResultsByGroupid(string _Groupid)
        {
            using (var db = new GymDB())
                return db.personalresult.Where(p => p.Groupid.Equals(_Groupid)).ToList();
        }

        public PersonalResult GetPersonalResultByAthleteIDAndGroupid(string _AthleteID, string _Groupid)
        {
            using (var db = new GymDB())
                return db.personalresult.Where(p => p.AthleteID.Equals(_AthleteID) && p.Groupid.Equals(_Groupid)).Single();
        }

        public List<Judge> GetJudgesByGroupid(string _Groupid)
        {
            using (var db = new GymDB())
            {
                var listofjudge = db.matchgroup.Where(mg => mg.GroupID.Equals(_Groupid));
                List<Judge> judges = new List<Judge>();
                foreach(var mgs in listofjudge)
                {
                    var tmp = db.judge.Where(j => j.JudgeID == mgs.JudgeID).Single();
                    judges.Add(tmp);
                }
                return judges;
            }
        }

        public List<MatchGroup> GetMatchGroupsJudgedByID(int _JudgeID)
        {
            using (var db = new GymDB())
                return db.matchgroup.Where(mg => mg.JudgeID == _JudgeID).ToList();
        }

        public Team GetTeamByTName(string _name)
        {
            using (var db = new GymDB())
                return db.team.Where(t => t.TName.Equals(_name)).Single();
        }

        public List<Team> GetAllTeams()
        {
            using (var db = new GymDB())
                return db.team.ToList();
        }

        public List<Athlete> GetAllAthletes()
        {
            using (var db = new GymDB())
                return db.athlete.ToList();
        }

        public List<Athlete> GetAthletesByTName(string _TName)
        {
            using (var db = new GymDB())
            {
                Team targetTeam = GetTeamByTName(_TName);
                return db.athlete.Where(a => a.TID == targetTeam.TID).ToList();
            }
        }

        public List<Staff> GetStaffsByTName(string _TName)
        {
            using (var db = new GymDB())
            {
                Team targetTeam = GetTeamByTName(_TName);
                return db.staff.Where(s => s.Tid == targetTeam.TID).ToList();
            }
        }

        public List<TeamResult> GetTeamResultsByTName(string _TName)
        {
            using (var db = new GymDB())
            {
                Team targetTeam = GetTeamByTName(_TName);
                return db.teamresult.Where(tr => tr.TID == targetTeam.TID).ToList();
            }
        }

        public void Delete(Login _login)
        {
            using (var db = new GymDB())
            {
                var target = db.login.Where(l => l.UID == _login.UID).Single();
                db.login.Remove(target);
                db.SaveChanges();
            }
        }

        public void Delete(Team _team)
        {
            using (var db = new GymDB())
            {
                var target = db.team.Where(t => t.TID == _team.TID).Single();
                db.team.Remove(target);
                db.SaveChanges();
            }
        }

        public void Delete(Judge _judge)
        {
            using (var db = new GymDB())
            {
                var target = db.judge.Where(j => j.JudgeID == _judge.JudgeID).Single();
                db.judge.Remove(target);
                db.SaveChanges();
            }
        }

        public void Delete(MatchGroup _matchgroup)
        {
            using (var db = new GymDB())
            {
                var target = db.matchgroup.Where(mg => mg.GroupID == _matchgroup.GroupID).Single();
                db.matchgroup.Remove(target);
                db.SaveChanges();
            }
        }

        public void Delete(PersonalResult _personalResult)
        {
            using (var db = new GymDB())
            {
                var target = db.personalresult.Where(pr => pr.PRid == _personalResult.PRid).Single();
                db.personalresult.Remove(target);
                db.SaveChanges();
            }
        }

        public void Delete(Athlete _athlete)
        {
            using (var db = new GymDB())
            {
                var target = db.athlete.Where(a => a.IDNumber == _athlete.IDNumber).Single();
                db.athlete.Remove(target);
                db.SaveChanges();
            }
        }

        public void Delete(Staff _staff)
        {
            using (var db = new GymDB())
            {
                var target = db.staff.Where(s => s.IDNumber == _staff.IDNumber).Single();
                db.staff.Remove(target);
                db.SaveChanges();
            }
        }

        public void Delete(TeamResult _teamResult)
        {
            using (var db = new GymDB())
            {
                var target = db.teamresult.Where(tr => tr.TRid == _teamResult.TRid).Single();
                db.teamresult.Remove(target);
                db.SaveChanges();
            }
        }

        public void Update(Login _login)
        {
            using (var db = new GymDB())
            {
                db.login.Attach(_login);
                db.Entry(_login).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void Update(Judge _judge)
        {
            using (var db = new GymDB())
            {
                db.judge.Attach(_judge);
                db.Entry(_judge).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void Update(MatchGroup _matchgroup)
        {
            using (var db = new GymDB())
            {
                db.matchgroup.Attach(_matchgroup);
                db.Entry(_matchgroup).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void Update(PersonalResult _personalresult)
        {
            using (var db = new GymDB())
            {
                db.personalresult.Attach(_personalresult);
                db.Entry(_personalresult).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void Update(Athlete _athlete)
        {
            using (var db = new GymDB())
            {
                db.athlete.Attach(_athlete);
                db.Entry(_athlete).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void Update(Team _team)
        {
            using (var db = new GymDB())
            {
                db.team.Attach(_team);
                db.Entry(_team).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void Update(Staff _staff)
        {
            using (var db = new GymDB())
            {
                db.staff.Attach(_staff);
                db.Entry(_staff).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void Update(TeamResult _teamresult)
        {
            using (var db = new GymDB())
            {
                db.teamresult.Attach(_teamresult);
                db.Entry(_teamresult).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        // Untested Code
        public void SetSuq(List<PersonalResult> _personalResults)
        {
            Random random = new Random();
            int length = _personalResults.Count();
            for (int i = 0; i < length; i++)
            {
                int rnum = random.Next(_personalResults.Count());
                var tmp = GetPersonalResultByAthleteIDAndGroupid(_personalResults[rnum].AthleteID, _personalResults[rnum].Groupid);
                tmp.Suq = (sbyte)(i + 1);
                Update(tmp);
                _personalResults.Remove(_personalResults[rnum]);
            }
        }

        // true means not null, false means null
        public bool isResultNotNull(List<PersonalResult> _personalResults)
        {
            bool result = true;
            foreach(var pr in _personalResults)
                result = result && (pr.Punishment != null) && (pr.Bouns != null) && (pr.Grade != null);
            return result;
        }

        public bool isResultNotNull(List<TeamResult> _teamResults)
        {
            bool result = true;
            foreach (var tr in _teamResults)
                result = result && (tr.Grade != null);
            return result;
        }

        public void Ranking(List<PersonalResult> _personalResults)
        {
            if (isResultNotNull(_personalResults))
            {
                var query = (from pr in _personalResults orderby pr.Grade descending select pr).ToList();
                using (var db = new GymDB())
                {
                    GymDBService dbs = new GymDBService();
                    foreach(var t in query)
                    {
                        t.Ranking = (short?)(query.IndexOf(t) + 1);
                        dbs.Update(t);
                    }
                }
            }
            else
                throw new Exception("There are empty items in the personalresult");
        }

        public void Ranking(List<TeamResult> _teamResults)
        {
            if (isResultNotNull(_teamResults))
            {
                var query = (from tr in _teamResults orderby tr.Grade descending select tr).ToList();
                using (var db = new GymDB())
                {
                    GymDBService dbs = new GymDBService();
                    foreach (var t in query)
                    {
                        t.Ranking = (short?)(query.IndexOf(t) + 1);
                        dbs.Update(t);
                    }
                }
            }
            else
                throw new Exception("There are empty items in the teamresult");
        }

        public bool isRankingNotNull(List<PersonalResult> _personalResults)
        {
            bool result = true;
            foreach (var prs in _personalResults)
                result = result && (prs.Ranking != null);
            return result;
        }

        public void Promote(List<PersonalResult> _personalResults, int NumofPromoted, int GroupSize)
        {
            if (isRankingNotNull(_personalResults))
            {
                var query = (from prs in _personalResults where prs.Ranking <= NumofPromoted select prs).ToList();
                using (var db = new GymDB())
                {
                    GymDBService dbs = new GymDBService();
                    // Create a new match group
                    foreach(var q in query)
                    {
                        // the Groupid of prs should be reset
                        //var prs = new PersonalResult(q.AthleteID, q.SportsEvent, q.Groupid, 2);
                        //dbs.Add(prs);
                    }
                }
            }
            else
                throw new Exception("There are empty rankings in the personalresult");
        }
    }
}
