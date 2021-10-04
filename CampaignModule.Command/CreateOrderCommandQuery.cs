using System;
using System.Linq;
using CampaignModule.Domain;
using CampaignModule.Validator;
using FluentValidation.Results;

namespace CampaignModule.Command
{
    public class CreateOrderCommandQuery : CommandQuery<CreateOrderRequest>
    {
        protected override void ExecuteTemplate(CreateOrderRequest commandRequest)
        {
            // Change product stock
            ChangeProductStockCommandRequest changeProductStockCommandRequest = new ChangeProductStockCommandRequest()
            {
                ProductCode = commandRequest.ProductCode,
                Quantity = commandRequest.Quantity
            };

            ChangeProductStockCommand changeProductStockCommand = new ChangeProductStockCommand();
            changeProductStockCommand.Execute(changeProductStockCommandRequest);

            // Price calculation for product
            CalculatePriceForProductRequest calculatePriceForProductRequest = new CalculatePriceForProductRequest() { ProductCode = commandRequest.ProductCode };
            CalculatePriceForProductCommand calculatePriceForProductCommand = new CalculatePriceForProductCommand();
            calculatePriceForProductCommand.Execute(calculatePriceForProductRequest);

            Order order = new Order()
            {
                Product = changeProductStockCommand.CurrentProduct,
                Quantity = commandRequest.Quantity,
                CalculatedPrice = calculatePriceForProductCommand.CalculatedPrice
            };

            // Order validation
            OrderValidator orderValidator = new OrderValidator();
            ValidationResult validationResult = orderValidator.Validate(order);
            if (validationResult.IsValid == false)
                throw new Exception(validationResult.ToString(Environment.NewLine));
            
            //Product stock will be decreased as order quantity
            DataContext.Instance.Orders.Add(order);
            
            // Find active campaign to add as GivenOrder
            Campaign activeCampaign = DataContext.Instance.Campaigns.FirstOrDefault(c => c.Product.ProductCode.Equals(changeProductStockCommand.CurrentProduct.ProductCode) && c.IsActive == true);
            if(activeCampaign != null)
                activeCampaign.GivenOrders.Add(order);
        }

        protected override bool CommandQueryIsValid(string[] splittedCommandQuery)
        {
            if(splittedCommandQuery[0].Equals("create_order") == false)
                throw new Exception("Create Order command query should be start with \"create_order\"!");

            if (splittedCommandQuery.Length != 3)
                throw new Exception("Wrong Create Order command format! Format should be as: \"create_order PRODUCTCODE QUANTITY\"!");

            return true;
        }

        protected override CreateOrderRequest GetCommandRequestByQuery(string[] splittedCommandQuery)
        {
            return new CreateOrderRequest()
            {
                ProductCode = splittedCommandQuery[1],
                Quantity = Convert.ToDecimal(splittedCommandQuery[2])
            };
        }
    }
}