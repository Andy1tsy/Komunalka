using System;
using System.Collections.Generic;

namespace Komunalka.DAL.Models
{
    public partial class Payment
    {
        public Payment()
        {
            PayingByCounter = new HashSet<PayingByCounter>();
            PayingFixedSumma = new HashSet<PayingFixedSumma>();
        }

        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public DateTime Timestamp { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual ICollection<PayingByCounter> PayingByCounter { get; set; }
        public virtual ICollection<PayingFixedSumma> PayingFixedSumma { get; set; }
    }
}
