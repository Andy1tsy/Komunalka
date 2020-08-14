namespace Komunalka.API.DTO
{
    public class PayingByCounterDTO
    {
        public string ServiceProviderName { get; set; }
        public string Account { get; set; }
        public int CounterIndicationsCurrent { get; set; }
        public int CurrentIndicationsPrevious { get; set; }
        public int IndicationsDifference { get; set; }
        public double RateCommon { get; set; }
        public double? RateDiscount { get; set; }
        public int? DiscountIndicationsAmount { get; set; }
        public decimal Summa { get; set; }
        public PayingByCounterDTO()
        {

        }
    }
}