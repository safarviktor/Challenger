using System;
using System.Collections.Generic;
using System.Linq;

namespace Challenger.Models
{
    public class DaySets
    {
        public DateTime Date { get; set; }

        public List<ChallengeSetModel> Sets { get; set; }

        public int Target { get; set; }

        public int Total => Sets.Sum(x => x.Repetitions);

        public int RunningTotalTarget { get; set; }
        public int RunningTotal { get; set; }
    }
}