using System;

namespace Challenger.Models
{
    public class TrackSkipModel
    {
        public int ChallengeId { get; set; }
        public DateTime Date { get; set; }
        public bool Skip { get; set; }
        public SkipReason Reason { get; set; }
        public string Comment{ get; set; }
    }
}
