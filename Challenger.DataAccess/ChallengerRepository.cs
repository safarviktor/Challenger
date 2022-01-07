using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Challenger.Models;
using Dapper;

namespace Challenger.DataAccess
{
    public class ChallengerRepository : BaseRepository
    {
        public async Task<int> AddNewChallenge(string name, ChallengeType type, string userId)
        {
            var query = "INSERT INTO clg.Challenge " +
                        "(UserId, [Name], ChallengeTypeId) " +
                        "SELECT @userId, @name, @type " +
                        "" +
                        "SELECT SCOPE_IDENTITY()";

            var p = new DynamicParameters();
            p.Add("@userId", userId, DbType.String);
            p.Add("@name", name, DbType.String);
            p.Add("@type", (int)type, DbType.Int32);

            return await WithConnection(async c =>
            {
                return (await c.QueryAsync<int>(sql: query, param: p)).FirstOrDefault();
            });
        }

        public async Task<IEnumerable<ChallengeOverviewModel>> GetChallengeOverviews(string userId)
        {
            var today = DateTime.Today.Date;

            var query = "SELECT " +
                        "Id, UserId, [Name], ChallengeTypeId AS [Type] " +
                        "FROM clg.Challenge " +
                        "WHERE UserId = @userId";

            var p = new DynamicParameters();
            p.Add("@userId", userId, DbType.String);

            var challenges = await WithConnection(async c =>
            {
                return (await c.QueryAsync<ChallengeOverviewModel>(sql: query, param: p)).ToList();
            });

            query = "SELECT " +
                    "S.ID, S.ChallengeId, S.Repetitions, S.[Date], S.DateTimeCreated " +
                    "FROM clg.[Set] S " +
                    "INNER JOIN clg.Challenge C ON C.Id = S.ChallengeId " +
                    "WHERE C.UserId = @userId";

            var allSets = await WithConnection(async c =>
            {
                return (await c.QueryAsync<ChallengeSetModel>(sql: query, param: p)).ToList();
            });

            foreach (var clg in challenges)
            {
                clg.CurrentTotal = allSets.Where(set => set.ChallengeId == clg.Id)?.Sum(set => set.Repetitions) ?? 0;
                clg.LastEntry = allSets.Where(set => set.ChallengeId == clg.Id).OrderBy(x => x.DateTimeCreated).LastOrDefault()?.DateTimeCreated ?? null;
                clg.LastEntryCount = allSets.Where(set => set.ChallengeId == clg.Id).OrderBy(x => x.DateTimeCreated).LastOrDefault()?.Repetitions ?? 0;
                clg.TodayCount = allSets.Where(set => set.ChallengeId == clg.Id && set.Date.Date == today)?.Sum(x => x.Repetitions) ?? 0;
            }
            
            challenges.ForEach(x => x.UpdateTodayGoal());

            return challenges;
        }


        public async Task<ChallengeSetModel> AddNewSet(TrackSetModel model)
        {
            var query = "INSERT INTO clg.[Set] " +
                        "(ChallengeId, Repetitions, [Date], DateTimeCreated) " +
                        "SELECT @challengeId, @repetitions, @date, GETDATE() ";

            model.Date = model.Date == new DateTime() ? DateTime.Now.Date : model.Date.Date;

            var p = new DynamicParameters();
            p.Add("@challengeId", model.ChallengeId, DbType.Int32);
            p.Add("@repetitions", model.Count, DbType.Int32);
            p.Add("@date", model.Date, DbType.Date);

            await WithConnection(async c =>
            {
                await c.ExecuteAsync(sql: query, param: p);
            });

            return new ChallengeSetModel()
            {
                ChallengeId = model.ChallengeId,
                Repetitions = model.Count,
                DateTimeCreated = DateTime.Now,
                Date = model.Date
            };
        }

        public async Task<bool> DeleteSet(int setId, int challengeId)
        {
            const string query = "DELETE clg.[Set] " +
                                 "WHERE Id = @setId AND ChallengeId = @challengeId";

            var p = new DynamicParameters();
            p.Add("@setId", setId, DbType.Int32);
            p.Add("@challengeId", challengeId, DbType.Int32);
            
            var result = await WithConnection(async c =>
            {
                return await c.ExecuteAsync(sql: query, param: p);
            });

            return result == 1;
        }


        public async Task<ChallengeDetailModel> GetChallengeDetails(int id, string userId)
        {
            var query = "SELECT " +
                        "Id, UserId, [Name], ChallengeTypeId AS [Type] " +
                        "FROM clg.Challenge " +
                        "WHERE ID = @id " +
                        "    AND UserId = @userId";

            var challenge = await WithConnection(async c =>
            {
                var p = new DynamicParameters();
                p.Add("Id", id, DbType.Int32);
                p.Add("userId", userId, DbType.String);
                return (await c.QueryAsync<ChallengeOverviewModel>(sql: query, param: p)).FirstOrDefault(); 
            });

            if (challenge == null)
            {
                return null;
            }

            query = "SELECT " +
                    "ID, ChallengeId, Repetitions, [Date], DateTimeCreated " +
                    "FROM clg.[Set] " +
                    "WHERE ChallengeId = @id";

            var sets = await WithConnection(async c =>
            {
                var p = new DynamicParameters();
                p.Add("Id", id, DbType.Int32);
                return (await c.QueryAsync<ChallengeSetModel>(sql: query, param: p)).ToList();
            });

            var model = new ChallengeDetailModel()
            {
                CurrentTotal = sets.Sum(x => x.Repetitions),
                Id = id,
                UserId = challenge.UserId,
                Name = challenge.Name,
                Type = challenge.Type,
                SetsByDay = sets
                    .GroupBy(x => x.Date.Date)
                    .Select(g => new DaySets()
                    {
                        Date = g.Key,
                        Sets = g.Select(s => new ChallengeSetModel()
                        {
                            Date = s.Date,
                            Repetitions = s.Repetitions,
                            DateTimeCreated = s.DateTimeCreated,
                            Id = s.Id,
                            ChallengeId = id
                        }).ToList()
                    }).ToList(),
                LastEntryCount = sets.OrderByDescending(x => x.DateTimeCreated).FirstOrDefault()?.Repetitions ?? 0,
                LastEntry = sets.OrderByDescending(x => x.DateTimeCreated).FirstOrDefault()?.DateTimeCreated,
                TodayCount = sets.Where(x => x.Date.Date == DateTime.Today.Date)?.Sum(x => x.Repetitions) ?? 0,
            };

            model.UpdateCalculatedFields();

            model.SetsByDay = model.SetsByDay.OrderByDescending(x => x.Date).Take(7).ToList();

            return model;
        }
    }
}