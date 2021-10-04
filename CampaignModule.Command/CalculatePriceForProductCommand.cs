using System;
using System.Linq;
using CampaignModule.Domain;

namespace CampaignModule.Command
{
    public class CalculatePriceForProductCommand : Command<CalculatePriceForProductRequest>
    {
        public const int DecreasePercentagePerHour = 5;
        public decimal CalculatedPrice { get; set; }
        
        protected override void ExecuteTemplate(CalculatePriceForProductRequest commandRequest)
        {
            Product product = DataContext.Instance.Products.FirstOrDefault(p => p.ProductCode.Equals(commandRequest.ProductCode));
            if (product == null)
                throw new Exception($"Product can not be found! Product code: {commandRequest.ProductCode}");
            
            Campaign campaign = DataContext.Instance.Campaigns.FirstOrDefault(c => c.Product.ProductCode.Equals(commandRequest.ProductCode) && c.IsActive == true);
            if (campaign == null)
            {
                // If there aren't active campaign for product, returns net price
                CalculatedPrice = product.Price;
                return;
            }

            if (campaign.TotalSalesCount >= campaign.TargetSalesCount)
            {
                //If campaign has reached to Target Sales Count limit, returns net price
                CalculatedPrice = product.Price;
                return;
            }
            
            // Calculates decrease percentage per hour
            int decreasePercentage = DecreasePercentagePerHour * campaign.ActiveDuration;
            if (decreasePercentage > campaign.PriceManipulationLimit)
            {
                // Decrease percentage can not be greater than Price Manipulation Limit of campaign
                decreasePercentage = campaign.PriceManipulationLimit;
            }
            
            CalculatedPrice = product.Price - (product.Price / 100 * decreasePercentage);
        }
    }
}