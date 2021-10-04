using System;

namespace CampaignModule.Domain
{
    public sealed class CampaignSystemTime
    {
        private static readonly Lazy<CampaignSystemTime> Lazy = new Lazy<CampaignSystemTime>(() => new CampaignSystemTime());

        public CampaignSystemTime()
        {
            SystemTime = new TimeSpan(0, 0, 0);
        }

        public static CampaignSystemTime Instance => Lazy.Value;

        public TimeSpan SystemTime { get; set; }
    }
}