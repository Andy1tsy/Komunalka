using System;
using System.Collections.Generic;

namespace Komunalka.DAL.Models
{
    public partial class Payment
    {
        public Payment()
        {
            PayingComponent = new HashSet<PayingComponent>();
        }

        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public decimal TotalSumma { get; set; }
        public DateTime Timestamp { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual ICollection<PayingComponent> PayingComponent { get; set; }
    }
}
