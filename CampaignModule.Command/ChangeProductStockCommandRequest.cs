using CampaignModule.Domain;

namespace CampaignModule.Command
{
    public class ChangeProductStockCommandRequest : CommandRequest
    {
        public string ProductCode { get; set; }

        public decimal Quantity { get; set; }
    }
}