using System;
using System.Collections.Generic;

namespace Challenger.Web.Models
{
    public class ChallengeViewModel
    {
        public string Name { get; set; }
        public int CurrentTotal { get; set; }
        public int TodayCount { get; set; }
        public int TodayGoal { get; set; }
        public int TodayTodo { get; set; }
        public DateTime LastEntry { get; set; }
        public int LastEntryCount { get; set; }
        public int TargetTotal { get; set; }
        public int TargetTotalTodo { get; set; }
        public AddSetViewModel AddSetViewModel { get; set; }

        public List<SetByDateViewModel> SetsByDate { get; set; }
        public int Id { get; set; }
    }
}