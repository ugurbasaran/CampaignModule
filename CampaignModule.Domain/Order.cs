namespace CampaignModule.Domain
{
    public class Order : DomainModel
    {
        public Product Product { get; set; }

        public decimal Quantity { get; set; }

        public decimal CalculatedPrice { get; set; }
    }
}