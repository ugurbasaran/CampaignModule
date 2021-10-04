namespace CampaignModule.Command
{
    public class GetCampaignInfoRequest : CommandRequest
    {
        public string CampaignCode { get; set; }
    }
}