using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace CampaignModule.Domain
{
    public class Campaign : DomainModel
    {
        public string Name { get; set; }

        public Product Product { get; set; }

        /// <summary>
        /// Duration given in hours
        /// </summary>
        public int Duration { get; set; }

        /// <summary>
        /// Manipulation limit
        /// </summary>
        public int PriceManipulationLimit { get; set; }

        public decimal TargetSalesCount { get; set; }

        public TimeSpan CreationTime { get; set; }

        public decimal TotalSalesCount => GivenOrders.Any() == false ? 0 : GivenOrders.Sum(o => o.Quantity);

        public decimal AverageItemPrice => GivenOrders.Any() == false ? 0 : (GivenOrders.Sum(o => o.Quantity * o.CalculatedPrice) / GivenOrders.Sum(o => o.Quantity));

        public List<Order> GivenOrders { get; set; } = new List<Order>();

        public bool IsActive => CampaignSystemTime.Instance.SystemTime.Subtract(CreationTime).Hours < Duration;

        public int ActiveDuration => CampaignSystemTime.Instance.SystemTime.Subtract(CreationTime).Hours;
    }
}