using System;
using System.Linq;
using CampaignModule.Domain;

namespace CampaignModule.Command
{
    public class CreateCampaignCommandQuery : CommandQuery<CreateCampaignRequest>
    {
        protected override void ExecuteTemplate(CreateCampaignRequest commandRequest)
        {
            if (DataContext.Instance.Campaigns.Any(c => c.Name.Equals(commandRequest.Name)))
                throw new Exception("Campaign can not be added with same name!");
            
            Product product = DataContext.Instance.Products.FirstOrDefault(p => p.ProductCode.Equals(commandRequest.ProductCode));
            if (product == null)
                throw new Exception($"Product can not be found! Product code: {commandRequest.ProductCode}");

            Campaign existsCampaign = DataContext.Instance.Campaigns.FirstOrDefault(c => c.Product.ProductCode.Equals(commandRequest.ProductCode));
            if (existsCampaign != null)
                throw new Exception("Multiple campaign can not be define for one product!");
            
            Campaign campaign = new Campaign()
            {
                Name = commandRequest.Name,
                Product = product,
                Duration = commandRequest.Duration,
                PriceManipulationLimit = commandRequest.PriceManipulationLimit,
                TargetSalesCount = commandRequest.TargetSalesCount,
                CreationTime = CampaignSystemTime.Instance.SystemTime
            };
            
            DataContext.Instance.Campaigns.Add(campaign);
            Console.WriteLine($"Campaign created; name {campaign.Name}, product {campaign.Product.ProductCode}, duration {campaign.Duration},limit {campaign.PriceManipulationLimit}, target sales count {campaign.TargetSalesCount}");
        }

        protected override bool CommandQueryIsValid(string[] splittedCommandQuery)
        {
            if (splittedCommandQuery[0].Equals("create_campaign") == false)
                throw new Exception("Create Campaign command query should be start with \"create_campaign\"!");

            if (splittedCommandQuery.Length != 6)
                throw new Exception(
                    "Wrong Create Campaign command format! Format should be as: \"create_campaign NAME PRODUCTCODE DURATION PMLIMIT TARGETSALESCOUNT\"!");

            if (int.TryParse(splittedCommandQuery[3], out _) == false)
                throw new Exception("Duration parameter should be integer!");

            if (int.TryParse(splittedCommandQuery[4], out _) == false)
                throw new Exception("PM Limit parameter should be integer!");
            
            if (int.TryParse(splittedCommandQuery[5], out _) == false)
                throw new Exception("Target Sales Count parameter should be integer!");

            return true;
        }

        protected override CreateCampaignRequest GetCommandRequestByQuery(string[] splittedCommandQuery)
        {
            return new CreateCampaignRequest()
            { 
                Name = splittedCommandQuery[1],
                ProductCode = splittedCommandQuery[2],
                Duration = Convert.ToInt32(splittedCommandQuery[3]),
                PriceManipulationLimit = Convert.ToInt32(splittedCommandQuery[4]),
                TargetSalesCount = Convert.ToInt32(splittedCommandQuery[5])
            };
        }
    }
}