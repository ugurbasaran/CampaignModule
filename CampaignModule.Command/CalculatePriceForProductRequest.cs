namespace CampaignModule.Command
{
    public class CalculatePriceForProductRequest : CommandRequest
    {
        public string ProductCode { get; set; }
    }
}