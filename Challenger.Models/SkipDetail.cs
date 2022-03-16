using System;

namespace Challenger.Models
{
    public class SkipDetail
    {
        public bool Skipped { get; set; }
        public SkipReason Reason { get; set; }
        public string Comment { get; set; }
    }
}
