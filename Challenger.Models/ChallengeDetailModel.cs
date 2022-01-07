using System;
using System.Collections.Generic;

namespace Challenger.Models
{
    public class ChallengeDetailModel : ChallengeOverviewModel
    {
        public List<DaySets> SetsByDay { get; set; }
    }
}