using System;
using System.Collections.Generic;

namespace Komunalka.DAL.Models
{
    public partial class ServiceProvider
    {
        public ServiceProvider()
        {
            PayingByCounter = new HashSet<PayingByCounter>();
            PayingFixedSumma = new HashSet<PayingFixedSumma>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<PayingByCounter> PayingByCounter { get; set; }
        public virtual ICollection<PayingFixedSumma> PayingFixedSumma { get; set; }
    }
}
