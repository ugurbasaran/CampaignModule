namespace CampaignModule.Domain
{
    public class Product : DomainModel
    {
        public string ProductCode { get; set; }

        public decimal Price { get; set; }

        public decimal Stock { get; set; }
    }
}