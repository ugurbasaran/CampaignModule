using System;

namespace CampaignModule.Command
{
    /// <summary>
    /// Command finder by command query
    /// </summary>
    public class CommandQueryFinder
    {
        public ICommandQuery FindRelevantCommandQuery(string commandQuery)
        {
            if (commandQuery.StartsWith("create_product"))
                return new CreateProductCommandQuery();
            else if (commandQuery.StartsWith("get_product_info"))
                return new GetProductInfoCommandQuery();
            else if (commandQuery.StartsWith("create_order"))
                return new CreateOrderCommandQuery();
            else if (commandQuery.StartsWith("create_campaign"))
                return new CreateCampaignCommandQuery();
            else if (commandQuery.StartsWith("get_campaign_info"))
                return new GetCampaignInfoCommandQuery();
            else if (commandQuery.StartsWith("increase_time"))
                return new IncreaseTimeCommandQuery();
            
            throw new Exception("Relevant command could not be found by command query!");
        }
    }
}