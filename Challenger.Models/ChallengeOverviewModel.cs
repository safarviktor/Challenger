using System;

namespace Challenger.Models
{
    public class ChallengeOverviewModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public ChallengeType Type { get; set; }

        // calculated fields
        public int CurrentTotal { get; set; }
        public DateTime? LastEntry { get; set; }
        public int LastEntryCount { get; set; }
        public int TodayCount { get; set; }
        public int TodayGoal { get; set; }
        public int TodayTodo { get; set; }
        public int TargetTotal { get; set; }
        public int TargetTotalTodo { get; set; }
    }
}