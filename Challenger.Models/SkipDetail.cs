using System;

namespace Challenger.Models
{
    public class SkipDetail
    {
        public DateTime DateTimeCreated { get; set; }
        public int ChallengeId { get; set; }
        public int Id { get; set; }
        public bool Skipped { get; set; }
        public DateTime Date { get; set; }
        public SkipReason Reason { get; set; }
        public string Comment { get; set; }
    }
}
