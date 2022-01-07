using System;

namespace Challenger.Models
{
    public class TrackSetModel
    {
        public int ChallengeId { get; set; }
        public string ChallengeName { get; set; }
        public int Count { get; set; }
        public DateTime Date { get; set; }
    }
}