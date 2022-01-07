using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Challenger.Models;

namespace Challenger.DataAccess
{
    public class DataContext
    {
        public ChallengeOverviewModel AddNewChallenge(string name, ChallengeType type)
        {
            var nextId = Database.Instance.ChallengeOverviews.Any()
                ? Database.Instance.ChallengeOverviews.Max(x => x.Id) + 1
                : 1;

            var model = new ChallengeOverviewModel()
            {
                Id = nextId,
                Name = name,
                Type = type
            };

            Database.Instance.ChallengeOverviews.Add(model);

            Database.Instance.Save();

            return model;
        }

        public ChallengeSetModel AddNewSet(TrackSetModel model)
        {
            var nextId = Database.Instance.ChallengeSets.Any()
                ? Database.Instance.ChallengeSets.Max(x => x.Id) + 1
                : 1;

            var newModel = new ChallengeSetModel()
            {
                ChallengeId = model.ChallengeId,
                Repetitions = model.Count,
                DateTimeCreated = DateTime.Now,
                Date = model.Date == new DateTime() ? DateTime.Now.Date : model.Date.Date,
                Id = nextId
            };

            Database.Instance.ChallengeSets.Add(newModel);

            Database.Instance.Save();

            return newModel;
        }

        public TrackSetModel GetSetForNewTracking(int challengeId)
        {
            var challenge = Database.Instance.ChallengeOverviews.FirstOrDefault(x => x.Id == challengeId);

            var model = new TrackSetModel()
            {
                ChallengeId = challengeId,
                ChallengeName = challenge?.Name,
                Date = DateTime.Today.Date
            };

            return model;
        }

        public IEnumerable<ChallengeOverviewModel> GetChallengeOverviews()
        {
            var today = DateTime.Today.Date;
            var challenges = Database.Instance.ChallengeOverviews.Select(c => new ChallengeOverviewModel()
            {
                Id = c.Id,
                UserId = c.UserId,
                Name = c.Name,
                CurrentTotal = Database.Instance.ChallengeSets.Where(set => set.ChallengeId == c.Id)?.Sum(set => set.Repetitions) ?? 0,
                LastEntry = Database.Instance.ChallengeSets.Where(set => set.ChallengeId == c.Id).OrderBy(x => x.DateTimeCreated).LastOrDefault()?.DateTimeCreated ?? null,
                LastEntryCount = Database.Instance.ChallengeSets.Where(set => set.ChallengeId == c.Id).OrderBy(x => x.DateTimeCreated).LastOrDefault()?.Repetitions ?? 0,
                TodayCount = Database.Instance.ChallengeSets.Where(set => set.ChallengeId == c.Id && set.Date.Date == today)?.Sum(x => x.Repetitions) ?? 0,
                Type = c.Type
            }).ToList();

            challenges.ForEach(x => x.UpdateTodayGoal());
            
            return challenges;
        }
        
        public ChallengeDetailModel GetChallengeDetails(int id)
        {
            var challenge = Database.Instance.ChallengeOverviews.FirstOrDefault(x => x.Id == id);

            if (challenge == null)
            {
                return null;
            }

            var sets = Database.Instance.ChallengeSets.Where(x => x.ChallengeId == id).ToList();

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

            // model.SetsByDay.RemoveAll(x => x.Date < DateTime.Now.Date.AddDays(-7));
            model.SetsByDay = model.SetsByDay.OrderByDescending(x => x.Date).Take(7).ToList();

            return model; 
        }
    }
}