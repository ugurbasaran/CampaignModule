namespace CampaignModule.Command
{
    public class CreateCampaignRequest : CommandRequest
    {
        public string Name { get; set; }

        public string ProductCode { get; set; }

        /// <summary>
        /// Duration given in hours
        /// </summary>
        public int Duration { get; set; }

        /// <summary>
        /// Manipulation limit
        /// </summary>
        public int PriceManipulationLimit { get; set; }

        public decimal TargetSalesCount { get; set; }
    }
}