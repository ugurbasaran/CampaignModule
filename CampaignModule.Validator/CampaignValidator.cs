using CampaignModule.Domain;
using FluentValidation;

namespace CampaignModule.Validator
{
    public class CampaignValidator : AbstractValidator<Campaign>
    {
        private const int minCampaignNameLength = 2;
        private const int maxCampaignNameLength = 50;
        private const int minDuration = 1;
        
        public CampaignValidator()
        {
            RuleFor(c => c.Name.Length)
                .GreaterThanOrEqualTo(minCampaignNameLength)
                .LessThanOrEqualTo(maxCampaignNameLength)
                .WithMessage($"Campaign name length should be between {minCampaignNameLength} and {maxCampaignNameLength}.");
            
            RuleFor(c => c.Duration)
                .GreaterThanOrEqualTo(minDuration)
                .WithMessage($"Campaign duration should be greater than {minDuration}.");

            RuleFor(c => c.PriceManipulationLimit)
                .GreaterThanOrEqualTo(0)
                .LessThanOrEqualTo(100)
                .WithMessage("Prica Manipulation Limit percentage should be between 0-100.");

            RuleFor(c => c.TargetSalesCount)
                .LessThanOrEqualTo(c => c.Product.Stock)
                .WithMessage("Target Sales Count can not be greater than product stock!");
        }
    }
}