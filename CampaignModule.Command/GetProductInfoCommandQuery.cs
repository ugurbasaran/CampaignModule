using System;
using System.Linq;
using CampaignModule.Domain;

namespace CampaignModule.Command
{
    public class GetProductInfoCommandQuery : CommandQuery<GetProductInfoRequest>
    {
        protected override void ExecuteTemplate(GetProductInfoRequest commandRequest)
        {
            Product product =
                DataContext.Instance.Products.FirstOrDefault(p => p.ProductCode.Equals(commandRequest.ProductCode));
            if (product == null)
                throw new Exception($"Product could not be found! Product Code: {commandRequest.ProductCode}");

            CalculatePriceForProductRequest calculatePriceForProductRequest = new CalculatePriceForProductRequest() { ProductCode = commandRequest.ProductCode };
            CalculatePriceForProductCommand calculatePriceForProductCommand = new CalculatePriceForProductCommand();
            calculatePriceForProductCommand.Execute(calculatePriceForProductRequest);

            Console.WriteLine($"Product {product.ProductCode} info; price {calculatePriceForProductCommand.CalculatedPrice}, stock {product.Stock}");
        }

        protected override bool CommandQueryIsValid(string[] splittedCommandQuery)
        {
            if (splittedCommandQuery[0].Equals("get_product_info") == false)
                throw new Exception("Get Product Info command query should be start with \"get_product_info\"!");

            if (splittedCommandQuery.Length != 2)
                throw new Exception(
                    "Wrong Get Product Info command format! Format should be as: \"get_product_info PRODUCTCODE\"!");

            return true;
        }

        protected override GetProductInfoRequest GetCommandRequestByQuery(string[] splittedCommandQuery)
        {
            return new GetProductInfoRequest()
            {
                ProductCode = splittedCommandQuery[1]
            };
        }
    }
}