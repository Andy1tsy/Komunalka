using System;
using System.Collections.Generic;

namespace Komunalka.DAL.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Payment = new HashSet<Payment>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public virtual ICollection<Payment> Payment { get; set; }
    }
}
