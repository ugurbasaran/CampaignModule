namespace CampaignModule.Command
{
    public class CreateOrderRequest : CommandRequest
    {
        public string ProductCode { get; set; }

        public decimal Quantity { get; set; }
    }
}