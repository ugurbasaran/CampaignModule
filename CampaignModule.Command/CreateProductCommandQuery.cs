using System;
using System.Linq;
using CampaignModule.Domain;
using CampaignModule.Validator;
using FluentValidation.Results;

namespace CampaignModule.Command
{
    public class CreateProductCommandQuery : CommandQuery<CreateProductRequest>
    {
        protected override void ExecuteTemplate(CreateProductRequest commandRequest)
        {
            Product product = new Product()
            {
                ProductCode = commandRequest.ProductCode,
                Price = commandRequest.Price,
                Stock = commandRequest.Stock
            };

            if (DataContext.Instance.Products.Any(p => p.ProductCode.Equals(product.ProductCode)))
                throw new Exception("Product can not be added with same product code!");

            ProductValidator productValidator = new ProductValidator();
            ValidationResult validationResult = productValidator.Validate(product);
            if (validationResult.IsValid == false)
                throw new Exception(validationResult.ToString(Environment.NewLine));

            DataContext.Instance.Products.Add(product);
            Console.WriteLine($"Product created; code {product.ProductCode}, price {product.Price}, stock {product.Stock}");
        }

        protected override bool CommandQueryIsValid(string[] splittedCommandQuery)
        {
            if (splittedCommandQuery[0].Equals("create_product") == false)
                throw new Exception("Create Product command query should be start with \"create_product\"!");

            if (splittedCommandQuery.Length != 4)
                throw new Exception(
                    "Wrong create product command format! Format should be as: \"create_product PRODUCTCODE PRICE STOCK\"!");

            if (decimal.TryParse(splittedCommandQuery[2], out _) == false)
                throw new Exception("Price parameter should be decimal!");

            if (decimal.TryParse(splittedCommandQuery[3], out _) == false)
                throw new Exception("Stock parameter should be decimal!");

            return true;
        }

        protected override CreateProductRequest GetCommandRequestByQuery(string[] splittedCommandQuery)
        {
            return new CreateProductRequest()
            {
                ProductCode = splittedCommandQuery[1],
                Price = Convert.ToDecimal(splittedCommandQuery[2]),
                Stock = Convert.ToDecimal(splittedCommandQuery[3])
            };
        }
    }
}