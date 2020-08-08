using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Komunalka.API.DTO
{
    public class PaymentDTO
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public DateTime Timestamp { get; set; }

        public List<PayingByCounterDTO> PayingsByCounterDTO { get; set; }
        public List<PayingFixedSummaDTO> PayingsFixedSummaDTO { get; set; }
    }
}