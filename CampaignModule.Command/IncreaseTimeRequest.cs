namespace CampaignModule.Command
{
    public class IncreaseTimeRequest : CommandRequest
    {
        public int HoursToIncrease { get; set; }
    }
}