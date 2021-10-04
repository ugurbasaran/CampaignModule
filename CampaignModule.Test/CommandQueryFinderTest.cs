using System;
using CampaignModule.Command;
using NUnit.Framework;

namespace CampaignModule.Test
{
    public class CommandQueryFinderTest
    {
        [TestCase("create_product", typeof(CreateProductCommandQuery))]
        [TestCase("get_product_info", typeof(GetProductInfoCommandQuery))]
        [TestCase("create_order", typeof(CreateOrderCommandQuery))]
        [TestCase("create_campaign", typeof(CreateCampaignCommandQuery))]
        [TestCase("get_campaign_info", typeof(GetCampaignInfoCommandQuery))]
        [TestCase("increase_time", typeof(IncreaseTimeCommandQuery))]
        public void FindRelevantCommandQuery_MatchingCommandQuery_TypeMatch(string commandQuery, Type commandQueryType)
        {
            // Arrange
            CommandQueryFinder commandQueryFinder = new CommandQueryFinder();

            // Act
            ICommandQuery commandQueryInstance = commandQueryFinder.FindRelevantCommandQuery(commandQuery);
            
            // Assert
            Assert.AreEqual(commandQueryType, commandQueryInstance.GetType());
        }
        
        [TestCase("create_product", typeof(GetProductInfoCommandQuery))]
        [TestCase("get_product_info", typeof(GetCampaignInfoCommandQuery))]
        [TestCase("create_order", typeof(CreateCampaignCommandQuery))]
        [TestCase("create_campaign", typeof(GetProductInfoCommandQuery))]
        [TestCase("get_campaign_info", typeof(IncreaseTimeCommandQuery))]
        [TestCase("increase_time", typeof(CreateCampaignCommandQuery))]
        public void FindRelevantCommandQuery_MatchingCommandQuery_TypeUnmatch(string commandQuery, Type commandQueryType)
        {
            // Arrange
            CommandQueryFinder commandQueryFinder = new CommandQueryFinder();

            // Act
            ICommandQuery commandQueryInstance = commandQueryFinder.FindRelevantCommandQuery(commandQuery);
            
            // Assert
            Assert.AreNotEqual(commandQueryType, commandQueryInstance.GetType());
        }
        
        [TestCase("lorem_ipsum")]
        [TestCase("123_456")]
        [TestCase("lorem_123 P1 100 2")]
        public void FindRelevantCommandQuery_MatchingCommandQuery_ThrowsException(string commandQuery)
        {
            // Arrange
            CommandQueryFinder commandQueryFinder = new CommandQueryFinder();

            // Act & Assert
            Assert.Throws<Exception>(() =>
            {
                ICommandQuery commandQueryInstance = commandQueryFinder.FindRelevantCommandQuery(commandQuery);
            });
        }
    }
}