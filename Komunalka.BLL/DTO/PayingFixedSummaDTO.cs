namespace Komunalka.API.DTO
{
    public class PayingFixedSummaDTO
    {
        public string ServiceProviderNane { get; set; }
        public string Account { get; set; }
        public decimal Summa { get; set; }

        public PayingFixedSummaDTO()
        {

        }
    }
}