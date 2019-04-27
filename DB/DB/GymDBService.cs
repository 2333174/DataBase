﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Data.Entity;

namespace DB
{
    // Database related operations
    public class GymDBService
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
            for (int i = 0; i < passwordLength; i++)
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
                //var targetGroup = db.matchgroup.Find(_personalResult.GID);
                targetAthlete.personalresult.Add(_personalResult);
                //targetGroup.personalresult.Add(_personalResult);
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
                //targetJudge.matchgroup.Add(_matchGroup);
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

        private bool CheckDuplicateRefereeScore(RefereeScore refereeScore)
        {
            using (var db = new GymDB())
            {
                var rss = db.refereescore.Where(rs => rs.PRid == refereeScore.PRid && rs.JudgeID == refereeScore.JudgeID);
                if (rss.Count() == 0) return true;
                else return false;
            }
        }

        public bool Add(RefereeScore _refereeScore)
        {
            using (var db = new GymDB())
            {
                if (!CheckDuplicateRefereeScore(_refereeScore)) return false;
                var targetPR = db.personalresult.Find(_refereeScore.PRid);
                var targetJudge = db.judge.Find(_refereeScore.JudgeID);
                db.refereescore.Add(_refereeScore);
                targetPR.refereescore.Add(_refereeScore);
                targetJudge.refereescore.Add(_refereeScore);
                db.SaveChanges();
                return true;
            }
        }

        public void Add(Setting _setting)
        {
            using (var db = new GymDB())
            {
                db.setting.Add(_setting);
                db.SaveChanges();
            }
        }

        public string GetMatchType(PersonalResult _personalResult)
        {
            string tmpType = null;
            switch (_personalResult.GroupID.Substring(3,1))
            {
                case "0":
                    tmpType = "预赛";
                    break;
                case "1":
                    tmpType = "决赛";
                    break;
            }
            return tmpType;
        }

        public string GetRealSportName(PersonalResult _personalResult)
        {
            string tmpSport = null;
            switch (_personalResult.SportsEvent.Substring(1, 2))
            {
                case "01":
                    tmpSport = "男子单杠";
                    break;
                case "02":
                    tmpSport = "男子双杠";
                    break;
                case "03":
                    tmpSport = "男子吊环";
                    break;
                case "04":
                    tmpSport = "男子跳马";
                    break;
                case "05":
                    tmpSport = "男子自由体操";
                    break;
                case "06":
                    tmpSport = "男子鞍马";
                    break;
                case "07":
                    tmpSport = "男子蹦床";
                    break;
                case "08":
                    tmpSport = "女子跳马";
                    break;
                case "09":
                    tmpSport = "女子高低杠";
                    break;
                case "10":
                    tmpSport = "女子平衡木";
                    break;
                case "11":
                    tmpSport = "女子自由体操";
                    break;
                case "12":
                    tmpSport = "女子蹦床";
                    break;
                default:
                    throw new Exception("未找到相关项目。");
            }
            return tmpSport;
        }

        // Get the judgeid by comparing username and role
        public int GetJudgeID(string _username, string password)
        {
            using (var db = new GymDB())
            {
                var account = db.login.Where(l => l.UName.Equals(_username) && l.Password.Equals(password)).FirstOrDefault();
                return (int)account.JudgeID;
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

        public RefereeScore GetRefereeScoreByPridAndJudgeID(int _Prid, int _judgeID)
        {
            using (var db = new GymDB())
                return db.refereescore.Where(rs => rs.PRid == _Prid && rs.JudgeID == _judgeID).Single();
        }

        public Athlete GetAthleteByID(string _AthleteIDNum)
        {
            using (var db = new GymDB())
                return db.athlete.Find(_AthleteIDNum);
        }

        public List<PersonalResult> GetPersonalResultsByAthleteID(string _AthleteID)
        {
            using (var db = new GymDB())
                return db.personalresult.Where(p => p.AthleteID.Equals(_AthleteID)).ToList();
        }

        public List<PersonalResult> GetPersonalResultsBySportEvent(string _sportEvent)
        {
            using (var db = new GymDB())
                return db.personalresult.Where(p => p.SportsEvent.Equals(_sportEvent)).ToList();
        }

        public PersonalResult GetPersonalResultByAthleteIDAndGroupid(string _AthleteID, string _Groupid)
        {
            using (var db = new GymDB())
                return db.personalresult.Where(p => p.AthleteID.Equals(_AthleteID) && p.GroupID.Equals(_Groupid)).Single();
        }

        public List<Judge> GetJudgesByGroupid(string _Groupid)
        {
            using (var db = new GymDB())
            {
                var listofjudge = db.matchgroup.Where(mg => mg.GroupID.Equals(_Groupid));
                List<Judge> judges = new List<Judge>();
                foreach (var mgs in listofjudge)
                {
                    var tmp = db.judge.Where(j => j.JudgeID == mgs.JudgeID).Single();
                    judges.Add(tmp);
                }
                return judges;
            }
        }

        public List<RefereeScore> GetRefereeScoresByPRid(int PRid)
        {
            using (var db = new GymDB())
                return db.refereescore.Where(rs => rs.PRid == PRid).ToList();
        }

        public List<Athlete> GetAthletesByTID(int _TID)
        {
            using (var db = new GymDB())
                return db.athlete.Where(a => a.TID == _TID).ToList();
        }

        public List<MatchGroup> GetMatchGroupsJudgedByID(int _JudgeID)
        {
            using (var db = new GymDB())
                return db.matchgroup.Where(mg => mg.JudgeID == _JudgeID).ToList();
        }

        public List<MatchGroup> GetMatchGroups()
        {
            using (var db = new GymDB())
                return db.matchgroup.ToList();
        }

        public List<PersonalResult> GetPersonalResultsByGroupID(string _Groupid)
        {
            using (var db = new GymDB())
                return db.personalresult.Where(p => p.GroupID == _Groupid).ToList();
        }

        public PersonalResult GetPersonalResultByPRid(int _PRid)
        {
            using (var db = new GymDB())
                return db.personalresult.Find(_PRid);
        }

        public Team GetTeamByTName(string _name)
        {
            using (var db = new GymDB())
                return db.team.Where(t => t.TName == _name).Single();
        }

        public Team GetTeamByTID(int _TID)
        {
            using (var db = new GymDB())
                return db.team.Where(t => t.TID == _TID).Single();
        }

        public string GetTeamNameByTID(int? _TID)
        {
            using (var db = new GymDB())
                return db.team.Find(_TID).TName;
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

        public MatchGroup GetMatchGroupByKey(int _key)
        {
            using (var db = new GymDB())
                return db.matchgroup.Find(_key);
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

        public Judge GetJudgeByJudgeID(int _judgeID)
        {
            using (var db = new GymDB())
                return db.judge.Find(_judgeID);
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

        public void Delete(RefereeScore _refereeScore)
        {
            using (var db = new GymDB())
            {
                var target = db.refereescore.Where(rs => rs.Rsid == _refereeScore.Rsid).Single();
                db.refereescore.Remove(target);
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

        public void Update(RefereeScore _refereeScore)
        {
            using (var db = new GymDB())
            {
                db.refereescore.Attach(_refereeScore);
                db.Entry(_refereeScore).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void Update(Setting _setting)
        {
            using (var db = new GymDB())
            {
                db.setting.Attach(_setting);
                db.Entry(_setting).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void SetSuq(List<PersonalResult> _personalResults)
        {
            Random random = new Random();
            int length = _personalResults.Count();
            for (int i = 0; i < length; i++)
            {
                int rnum = random.Next(_personalResults.Count());
                var tmp = GetPersonalResultByAthleteIDAndGroupid(_personalResults[rnum].AthleteID, _personalResults[rnum].GroupID);
                tmp.Suq = (sbyte)(i + 1);
                Update(tmp);
                _personalResults.Remove(_personalResults[rnum]);
            }
        }

        // true means not null, false means null
        public bool IsResultNotNull(List<PersonalResult> _personalResults)
        {
            bool result = true;
            foreach(var pr in _personalResults)
                result = result && (pr.Punishment != null) && (pr.Bouns != null) && (pr.Grade != null);
            return result;
        }

        public bool IsResultNotNull(List<TeamResult> _teamResults)
        {
            bool result = true;
            foreach (var tr in _teamResults)
                result = result && (tr.Grade != null);
            return result;
        }

        public void Ranking(List<PersonalResult> _personalResults)
        {
            if (IsResultNotNull(_personalResults))
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

        public void Grouping(int n)
        {
            using (var db = new GymDB())
            {
                var list1 = db.personalresult.ToList();
                var query = list1.GroupBy(a => a.SportsEvent).Select(g => new { id = g.Key, count = g.Count() });
                int i;
                foreach (var l in query)
                {
                    i = 0;
                    var list2 = GetPersonalResultsBySportEvent(l.id);
                    foreach (var ll in list2)
                    {
                        MatchGroup m = new MatchGroup(l.id + (i / n).ToString());//sportevent+小组
                        int tot = db.matchgroup.ToList().Count;
                        Add(m);
                        ll.GroupID = m.GroupID;
                        Update(ll);
                        i++;
                    }
                }
            }
        }
        public void Ranking(List<TeamResult> _teamResults)
        {
            if (IsResultNotNull(_teamResults))
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

        public bool IsRankingNotNull(List<PersonalResult> _personalResults)
        {
            bool result = true;
            foreach (var prs in _personalResults)
                result = result && (prs.Ranking != null);
            return result;
        }

        public void Promote(List<PersonalResult> _personalResults, int NumofPromoted, int GroupSize)
        {
            if (IsRankingNotNull(_personalResults))
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
        
        //登录，返回-1为密码错误，返回0为验证成功,返回1为裁判，返回2为主裁判
        public int Loginf(string username,string password, int role)
        {
            using (var db=new GymDB())
            {
                var account = db.login.Where(l => l.UName.Equals(username) && l.Role == role && l.Password.Equals(password)).SingleOrDefault();
                if (account == null)
                {
                    return -1;
                }
                else
                {
                    if (role == 2)
                    {
                        if (account.Weight == 0) return 1;
                        else return 2;
                    }
                    else return 0;
                }
            }
        }

        //返回Tname
        public string GetTName(string username, string password, int role)
        {
            using (var db = new GymDB())
            {
                var account = db.login.Where(l => l.UName.Equals(username) && l.Role == role && l.Password.Equals(password)).SingleOrDefault();
                return account.TName;
            }
        }

        public int GetTIDByTName(string _TName)
        {
            using (var db = new GymDB())
            {
                return db.team.Where(a => a.TName.Equals(_TName)).First().TID;
            }

        }
        
        //设置三个参数:代表队男/女各年龄组最大报名人数
        public void Set(string POne, string PTwo, string PThree)
        {
            using (var db = new GymDB())
            {
                if (db.setting.ToList().Count == 0)
                {
                    Setting setting = new Setting(int.Parse(POne), int.Parse(PTwo), int.Parse(PThree));
                    db.setting.Add(setting);
                }
                else
                {
                    foreach (var a in db.setting)
                    {
                        db.setting.Remove(a);
                        Setting setting = new Setting(int.Parse(POne), int.Parse(PTwo), int.Parse(PThree));
                        db.setting.Add(setting);
                    }
                }
                db.SaveChanges();
            }
        }

    }
}
