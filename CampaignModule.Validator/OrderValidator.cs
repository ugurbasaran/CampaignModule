using System.Data;
using CampaignModule.Domain;
using FluentValidation;

namespace CampaignModule.Validator
{
    public class OrderValidator : AbstractValidator<Order>
    {
        private const int minOrderQuantity = 1;
        private const int maxOrderQuantity = 100;
        private const decimal minCalculatedPrice = (decimal) 0.1;

        public OrderValidator()
        {
            RuleFor(c => c.Quantity)
                .GreaterThanOrEqualTo(minOrderQuantity)
                .LessThan(maxOrderQuantity)
                .WithMessage($"Order quantity should be between {minOrderQuantity} and {maxOrderQuantity}");
            
            RuleFor(c => c.CalculatedPrice)
                .GreaterThanOrEqualTo(minOrderQuantity)
                .LessThanOrEqualTo(c => c.Product.Price)
                .WithMessage($"Order Calculated Price should be between {minCalculatedPrice} and Product Price!");
        }
    }
}