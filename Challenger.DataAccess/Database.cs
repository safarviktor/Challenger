using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Challenger.Models;
using Newtonsoft.Json;

namespace Challenger.DataAccess
{
    public sealed class Database
    {
        private static readonly Database _instance = new Database();

        private Database()
        {
            Load();
        }

        public static Database Instance
        {
            get
            {
                return _instance;
            }
        }

        public IList<ChallengeOverviewModel> ChallengeOverviews { get; set; }

        public IList<ChallengeSetModel> ChallengeSets { get; set; }

        private const string ChallengeOverviewsFile = @"C:\temp\challengerDatabase\ChallengeOverviews_dev.txt";
        private const string ChallengeSetsFile = @"C:\temp\challengerDatabase\ChallengeSets_dev.txt";

        private void Load()
        {
            var challengeSetsRaw = File.ReadAllText(ChallengeSetsFile);
            ChallengeSets = JsonConvert.DeserializeObject<List<ChallengeSetModel>>(challengeSetsRaw);

            if (ChallengeSets == null)
            {
                ChallengeSets = new List<ChallengeSetModel>();
            }

            var challengeOverviewsRaw = File.ReadAllText(ChallengeOverviewsFile);
            ChallengeOverviews = JsonConvert.DeserializeObject<List<ChallengeOverviewModel>>(challengeOverviewsRaw);
            if (ChallengeOverviews == null)
            {
                ChallengeOverviews = new List<ChallengeOverviewModel>();
            }

            var today = DateTime.Today.Date;

            foreach (var c in ChallengeOverviews)
            {
                c.CurrentTotal = ChallengeSets.Where(set => set.ChallengeId == c.Id)?.Sum(set => set.Repetitions) ?? 0;
                c.LastEntry = ChallengeSets.Where(set => set.ChallengeId == c.Id).OrderBy(x => x.DateTimeCreated).LastOrDefault()?.DateTimeCreated ?? null;
                c.LastEntryCount = ChallengeSets.Where(set => set.ChallengeId == c.Id).OrderBy(x => x.DateTimeCreated).LastOrDefault()?.Repetitions ?? 0;
                c.TodayCount = ChallengeSets.Where(set => set.ChallengeId == c.Id && set.Date.Date == today)?.Sum(x => x.Repetitions) ?? 0;
                c.UpdateTodayGoal();
            }
        }

        public void Save()
        {
            File.WriteAllText(ChallengeSetsFile, JsonConvert.SerializeObject(ChallengeSets));
            File.WriteAllText(ChallengeOverviewsFile, JsonConvert.SerializeObject(ChallengeOverviews.Select(x => new { x.Id, x.UserId, x.Name, x.Type })));
            foreach (var c in ChallengeOverviews)
            {
                c.UpdateTodayGoal();
            }
        }
    }
}