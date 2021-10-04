using System;
using System.Linq;
using CampaignModule.Domain;

namespace CampaignModule.Command
{
    public class GetCampaignInfoCommandQuery : CommandQuery<GetCampaignInfoRequest>
    {
        protected override void ExecuteTemplate(GetCampaignInfoRequest commandRequest)
        {
            Campaign campaign =
                DataContext.Instance.Campaigns.FirstOrDefault(c => c.Name.Equals(commandRequest.CampaignCode));
            if (campaign == null)
                throw new Exception($"Campaign could not be found! Campaign Name: {commandRequest.CampaignCode}");

            string status = campaign.IsActive ? "Active" : "Ended";
            string averageItemPrice = campaign.AverageItemPrice == 0 ? "-" : campaign.AverageItemPrice.ToString("F");
            Console.WriteLine($"Campaign {campaign.Name} info; Status {status}, Target Sales {campaign.TargetSalesCount}, Total Sales {campaign.TotalSalesCount}, Turnover {(campaign.TotalSalesCount * campaign.AverageItemPrice):F}, Average Item Price {averageItemPrice}");
        }

        protected override bool CommandQueryIsValid(string[] splittedCommandQuery)
        {
            if (splittedCommandQuery[0].Equals("get_campaign_info") == false)
                throw new Exception("Get Campaign Info command query should be start with \"get_campaign_info\"!");
            if (splittedCommandQuery.Length != 2)
                throw new Exception(
                    "Wrong Get Campaign Info command format! Format should be as: \"get_campaign_info NAME\"!");
            return true;
        }

        protected override GetCampaignInfoRequest GetCommandRequestByQuery(string[] splittedCommandQuery)
        {
            return new GetCampaignInfoRequest()
            {
                CampaignCode = splittedCommandQuery[1]
            };
        }
    }
}