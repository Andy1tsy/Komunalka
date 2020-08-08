using System;
using System.Collections.Generic;

namespace Komunalka.DAL.Models
{
    public partial class PayingFixedSumma
    {
        public int Id { get; set; }
        public int PaymentId { get; set; }
        public int ServiceProviderId { get; set; }
        public string Account { get; set; }
        public decimal Summa { get; set; }

        public virtual Payment Payment { get; set; }
        public virtual ServiceProvider ServiceProvider { get; set; }
    }
}
