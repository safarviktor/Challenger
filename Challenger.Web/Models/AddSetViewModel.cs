using System;

namespace Challenger.Web.Models
{
    public class AddSetViewModel
    {
        public string Message { get; set; }
        public int Count { get; set; }

        public int ChallengeId { get; set; }

        public DateTime Date { get; set; }
    }
}