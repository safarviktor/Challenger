using System;

namespace Challenger.Models
{
    public class ChallengeSetModel
    {
        public int Id { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime Date { get; set; }
        public int Repetitions { get; set; }
        public int ChallengeId { get; set; }
    }
}