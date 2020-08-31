using System;
using System.Collections.Generic;

namespace Komunalka.DAL.Models
{
    public partial class ServiceProvider
    {
        public ServiceProvider()
        {
            PayingComponent = new HashSet<PayingComponent>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public ServiceType ServiceType { get; set; }

        public virtual ICollection<PayingComponent> PayingComponent { get; set; }
    }
}
