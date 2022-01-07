using Challenger.Models;

namespace Challenger.Web.Models
{
    public class AddChallengeViewModel
    {
        public string Name { get; set; }

        public ChallengeType Type { get; set; }

        public string Message { get; set; }
    }
}