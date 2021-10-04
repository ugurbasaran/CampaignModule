using System;
using System.Collections.Generic;

namespace CampaignModule.Domain
{
    public sealed class DataContext
    {
        private static readonly Lazy<DataContext> Lazy = new Lazy<DataContext>(() => new DataContext());
        public static DataContext Instance => Lazy.Value;

        public List<Product> Products { get; set; } = new List<Product>();

        public List<Order> Orders { get; set; } = new List<Order>();

        public List<Campaign> Campaigns { get; set; } = new List<Campaign>();
        
    }
}