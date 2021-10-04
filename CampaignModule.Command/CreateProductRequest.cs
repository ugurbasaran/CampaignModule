namespace CampaignModule.Command
{
    public class CreateProductRequest : CommandRequest
    {
        public string ProductCode { get; set; }

        public decimal Price { get; set; }

        public decimal Stock { get; set; }
    }
}