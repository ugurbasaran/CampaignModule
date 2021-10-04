using System;
using CampaignModule.Command;
using CampaignModule.Domain;
using NUnit.Framework;

namespace CampaignModule.Test
{
    public class CalculatePriceForProductTest
    {
        [TestCase]
        public void CalculatePriceForProduct_NoCampaign_ReturnsProductPrice()
        {
            // Arrange
            Product product = new Product()
            {
                ProductCode = "P1",
                Price = 100,
                Stock = 100
            };
            
            DataContext.Instance.Products.Add(product);
            CalculatePriceForProductCommand calculatePriceForProductCommand = new CalculatePriceForProductCommand();
            CalculatePriceForProductRequest calculatePriceForProductRequest = new CalculatePriceForProductRequest()
            {
                ProductCode = product.ProductCode
            };

            // Act
            calculatePriceForProductCommand.Execute(calculatePriceForProductRequest);
            
            // Assert
            Assert.AreEqual(product.Price, calculatePriceForProductCommand.CalculatedPrice);
        }
        
        [TestCase]
        public void CalculatePriceForProduct_CampaignExistsWithoutDecreasedPrice_ReturnsProductPrice()
        {
            // Arrange
            Product product = new Product()
            {
                ProductCode = "P1",
                Price = 100,
                Stock = 100
            };

            Campaign campaign = new Campaign()
            {
                Duration = 5,
                Name = "C1",
                TargetSalesCount = 100,
                Product = product,
                CreationTime = TimeSpan.FromHours(0),
                PriceManipulationLimit = 60
            };
            
            DataContext.Instance.Products.Add(product);
            DataContext.Instance.Campaigns.Add(campaign);
            CalculatePriceForProductCommand calculatePriceForProductCommand = new CalculatePriceForProductCommand();
            CalculatePriceForProductRequest calculatePriceForProductRequest = new CalculatePriceForProductRequest()
            {
                ProductCode = product.ProductCode
            };

            // Act
            calculatePriceForProductCommand.Execute(calculatePriceForProductRequest);
            
            // Assert
            Assert.AreEqual(product.Price, calculatePriceForProductCommand.CalculatedPrice);
        }
        
        [TestCase]
        public void CalculatePriceForProduct_CampaignExistsWithDecreasedPrice_ReturnsDecreasedPrice()
        {
            // Arrange
            Product product = new Product()
            {
                ProductCode = "P1",
                Price = 100,
                Stock = 100
            };

            Campaign campaign = new Campaign()
            {
                Duration = 5,
                Name = "C1",
                TargetSalesCount = 100,
                Product = product,
                CreationTime = TimeSpan.FromHours(0),
                PriceManipulationLimit = 60
            };

            int decreasePercentagePerHour = 5;
            CampaignSystemTime.Instance.SystemTime = CampaignSystemTime.Instance.SystemTime.Add(TimeSpan.FromHours(1));
            DataContext.Instance.Products.Add(product);
            DataContext.Instance.Campaigns.Add(campaign);
            CalculatePriceForProductCommand calculatePriceForProductCommand = new CalculatePriceForProductCommand();
            CalculatePriceForProductRequest calculatePriceForProductRequest = new CalculatePriceForProductRequest()
            {
                ProductCode = product.ProductCode
            };

            // Act
            calculatePriceForProductCommand.Execute(calculatePriceForProductRequest);
            
            // Assert
            Assert.AreEqual(product.Price - (product.Price / 100 * decreasePercentagePerHour ), calculatePriceForProductCommand.CalculatedPrice);
        }
    }
}