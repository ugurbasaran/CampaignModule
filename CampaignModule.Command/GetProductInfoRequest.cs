namespace CampaignModule.Command
{
    public class GetProductInfoRequest : CommandRequest
    {
        public string ProductCode { get; set; }
    }
}