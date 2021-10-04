using CampaignModule.Domain;
using CampaignModule.Validator;
using FluentValidation.Results;
using NUnit.Framework;

namespace CampaignModule.Test
{
    public class ProductValidatorTest
    {
        [TestCase("P1")]
        [TestCase("P3")]
        [TestCase("P5")]
        public void Validate_ProductCodeLength_ReturnsValid(string productCode)
        {
            // Arrange
            ProductValidator productValidator = new ProductValidator();
            Product product = new Product()
            {
                ProductCode = productCode,
                Price = 100,
                Stock = 100
            };

            // Act
            ValidationResult validationResult = productValidator.Validate(product);
            
            // Assert
            Assert.AreEqual(validationResult.IsValid, true);
        }
        
        [TestCase("P")]
        [TestCase("P3453635dsfgf345")]
        [TestCase("LoremIpsumIpsumLorem")]
        public void Validate_ProductCodeLength_ReturnsInvalid(string productCode)
        {
            // Arrange
            ProductValidator productValidator = new ProductValidator();
            Product product = new Product()
            {
                ProductCode = productCode,
                Price = 100,
                Stock = 100
            };

            // Act
            ValidationResult validationResult = productValidator.Validate(product);
            
            // Assert
            Assert.AreEqual(validationResult.IsValid, false);
        }
        
        [TestCase(3)]
        [TestCase(87)]
        [TestCase(9765)]
        [TestCase(3.5)]
        public void Validate_Price_ReturnsValid(decimal price)
        {
            // Arrange
            ProductValidator productValidator = new ProductValidator();
            Product product = new Product()
            {
                ProductCode = "P1",
                Price = price,
                Stock = 100
            };

            // Act
            ValidationResult validationResult = productValidator.Validate(product);
            
            // Assert
            Assert.AreEqual(validationResult.IsValid, true);
        }
        
        [TestCase(0)]
        [TestCase(20000)]
        [TestCase(-5)]
        [TestCase(-5000)]
        public void Validate_Price_ReturnsInvalid(decimal price)
        {
            // Arrange
            ProductValidator productValidator = new ProductValidator();
            Product product = new Product()
            {
                ProductCode = "P1",
                Price = price,
                Stock = 100
            };

            // Act
            ValidationResult validationResult = productValidator.Validate(product);
            
            // Assert
            Assert.AreEqual(validationResult.IsValid, false);
        }
        
        [TestCase(33)]
        [TestCase(1717)]
        [TestCase(1756)]
        [TestCase(4.7)]
        public void Validate_Stock_ReturnsValid(decimal stock)
        {
            // Arrange
            ProductValidator productValidator = new ProductValidator();
            Product product = new Product()
            {
                ProductCode = "P1",
                Price = 100,
                Stock = stock
            };

            // Act
            ValidationResult validationResult = productValidator.Validate(product);
            
            // Assert
            Assert.AreEqual(validationResult.IsValid, true);
        }
        
        [TestCase(-5)]
        [TestCase(171717)]
        [TestCase(17561616)]
        [TestCase(-3.5)]
        public void Validate_Stock_ReturnsInvalid(decimal stock)
        {
            // Arrange
            ProductValidator productValidator = new ProductValidator();
            Product product = new Product()
            {
                ProductCode = "P1",
                Price = 100,
                Stock = stock
            };

            // Act
            ValidationResult validationResult = productValidator.Validate(product);
            
            // Assert
            Assert.AreEqual(validationResult.IsValid, false);
        }
    }
}