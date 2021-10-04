using CampaignModule.Domain;
using FluentValidation;

namespace CampaignModule.Validator
{
    public class ProductValidator : AbstractValidator<Product>
    {
        private const int minProductCodeLength = 2;
        private const int maxProductCodeLength = 13;
        private const int minProductPrice = 1;
        private const int maxProducPrice = 10000;
        private const int minProductStock = 0;
        private const int maxProducStock = 100000;
        
        public ProductValidator()
        {
            RuleFor(p => p.ProductCode.Length)
                .GreaterThanOrEqualTo(minProductCodeLength)
                .LessThanOrEqualTo(maxProductCodeLength)
                .WithMessage($"Product code length should be between {minProductCodeLength} and {maxProductCodeLength}.");

            RuleFor(p => p.Price)
                .GreaterThanOrEqualTo(minProductPrice)
                .LessThanOrEqualTo(maxProducPrice)
                .WithMessage($"Product price should be between {minProductPrice} and {maxProducPrice}.");
            
            RuleFor(p => p.Stock)
                .GreaterThanOrEqualTo(minProductStock)
                .LessThanOrEqualTo(maxProducStock)
                .WithMessage($"Product stock should be between {minProductStock} and {maxProducStock}.");
        }
    }
}