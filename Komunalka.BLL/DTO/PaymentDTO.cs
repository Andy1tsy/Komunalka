using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Komunalka.BLL.DTO
{
    public class PaymentDTO
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public decimal TotalSumma { get; set; }
        public DateTime Timestamp { get; set; }

        public List<PayingComponentDTO> PayingComponentsDTO { get; set; }

        public PaymentDTO()
        {
            PayingComponentsDTO = new List<PayingComponentDTO>();
        }
    }
}