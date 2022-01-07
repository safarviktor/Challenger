using System;
using System.Collections.Generic;
using System.Linq;

namespace Challenger.Web.Models
{
    public class SetByDateViewModel
    {
        public DateTime Date { get; set; }

        public List<SetViewModel> Sets { get; set; }

        public int Target { get; set; }

        public int Total => Sets.Sum(x => x.Count);

        public int RunningTotalTarget { get; set; }
        public int RunningTotal { get; set; }
    }
}