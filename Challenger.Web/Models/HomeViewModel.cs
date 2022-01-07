using System.Collections.Generic;
using Challenger.Models;

namespace Challenger.Web.Models
{
    public class HomeViewModel
    {
        public AddChallengeViewModel NewChallenge { get; set; }

        public IEnumerable<ChallengeOverviewModel> Challenges { get; set; }

        public string Name { get; set; }
    }
}