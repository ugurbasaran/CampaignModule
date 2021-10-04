using System;
using CampaignModule.Domain;

namespace CampaignModule.Command
{
    public class IncreaseTimeCommandQuery : CommandQuery<IncreaseTimeRequest>
    {
        protected override void ExecuteTemplate(IncreaseTimeRequest commandRequest)
        {
            // Increase hours
            TimeSpan hoursToAdd = new TimeSpan(commandRequest.HoursToIncrease, 0, 0);
            CampaignSystemTime.Instance.SystemTime = CampaignSystemTime.Instance.SystemTime.Add(hoursToAdd);
            Console.WriteLine("Time is {0:00}:{1:00}", CampaignSystemTime.Instance.SystemTime.Hours,
                CampaignSystemTime.Instance.SystemTime.Minutes);
        }

        protected override bool CommandQueryIsValid(string[] splittedCommandQuery)
        {
            if (splittedCommandQuery[0].Equals("increase_time") == false)
                throw new Exception("Increase Time command query should be start with \"increase_time\"!");

            if (splittedCommandQuery.Length != 2)
                throw new Exception("Wrong Increase Time command format! Format should be as: \"increase_time HOUR\"!");

            return true;
        }

        protected override IncreaseTimeRequest GetCommandRequestByQuery(string[] splittedCommandQuery)
        {
            return new IncreaseTimeRequest()
            {
                HoursToIncrease = Convert.ToInt32(splittedCommandQuery[1])
            };
        }
    }
}