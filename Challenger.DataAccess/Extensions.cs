using System;
using System.Collections.Generic;
using System.Linq;
using Challenger.Models;

namespace Challenger.DataAccess
{
    public static class Extensions
    {
        private static Dictionary<DateTime, int> _sumByDayCache = new Dictionary<DateTime, int>();

        public static void UpdateTodayGoal(this ChallengeOverviewModel c)
        {
            if (c.Type == ChallengeType.AddOneMoreEachDay)
            {
                c.TodayGoal = DateTime.Now.DayOfYear;
                c.TodayTodo = DateTime.Now.DayOfYear - c.TodayCount;
                c.TargetTotal = GetSumFromBeginningOfTheYear(DateTime.Today);
                c.TargetTotalTodo = c.TargetTotal - c.CurrentTotal;
            }
        }

        private static int GetSumFromBeginningOfTheYear(DateTime untilDay)
        {
            if (!_sumByDayCache.ContainsKey(untilDay))
            {
                _sumByDayCache.Add(untilDay, CalculateSumTilDay(untilDay));
            }

            return _sumByDayCache[untilDay];
        }

        private static int CalculateSumTilDay(DateTime untilDay)
        {
            var result = 0;
            var i = 1;
            while (i <= untilDay.DayOfYear)
            {
                result += i;
                i++;
            }

            return result;
        }

        public static void UpdateCalculatedFields(this ChallengeDetailModel c)
        {
            if (c.Type == ChallengeType.AddOneMoreEachDay)
            {
                c.UpdateTodayGoal();
                c.SetsByDay.ForEach(thisSet =>
                {
                    thisSet.Target = thisSet.Date.DayOfYear;
                    thisSet.RunningTotal = c.SetsByDay.Where(s => s.Date <= thisSet.Date).SelectMany(s => s.Sets).Sum(s => s.Repetitions);
                    thisSet.RunningTotalTarget = GetSumFromBeginningOfTheYear(thisSet.Date);
                });
            }

            var allChallengeSets = c.SetsByDay.SelectMany(x => x.Sets).ToList();
            c.LastEntryCount = allChallengeSets.OrderByDescending(x => x.DateTimeCreated).FirstOrDefault()?.Repetitions ?? 0;
            c.LastEntry = allChallengeSets.OrderByDescending(x => x.DateTimeCreated).FirstOrDefault()?.DateTimeCreated;
            c.TodayCount = allChallengeSets.Where(x => x.Date.Date == DateTime.Today.Date)?.Sum(x => x.Repetitions) ?? 0;
        }

        private static Dictionary<DateTime, int> GetTargetsByDay()
        {
            var results = new Dictionary<DateTime, int>();

            for (var i = FirstDayOfThisYear().Date; i <= DateTime.Today.Date; i = i.AddDays(1))
            {
                results.Add(i, i.DayOfYear);
            }

            return results;
        }

        private static DateTime FirstDayOfThisYear()
        {
            return new DateTime(DateTime.Now.Year, 1, 1);
        }
    }
}