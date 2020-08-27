namespace Komunalka.BLL.DTO
{
    public class PayingFixedSummaDTO
    {
        public string ServiceProviderName { get; set; }
        public string Account { get; set; }
        public decimal Summa { get; set; }

        public PayingFixedSummaDTO()
        {

        }
    }
}