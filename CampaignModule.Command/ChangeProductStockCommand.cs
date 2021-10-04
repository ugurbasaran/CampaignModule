using System;
using System.Linq;
using CampaignModule.Domain;

namespace CampaignModule.Command
{
    public class ChangeProductStockCommand : Command<ChangeProductStockCommandRequest>
    {
        public Product CurrentProduct { get; set; }
        
        protected override void ExecuteTemplate(ChangeProductStockCommandRequest commandRequest)
        {
            Product product = DataContext.Instance.Products.FirstOrDefault(p => p.ProductCode.Equals(commandRequest.ProductCode));
            if (product == null)
                throw new Exception($"Product can not be found! Product code: {commandRequest.ProductCode}");
            
            if(commandRequest.Quantity > product.Stock)
                throw new Exception($"There are not enough stock for product! Product code: {commandRequest.ProductCode}, Stock: {product.Stock}");
            
            product.Stock -= commandRequest.Quantity;
            CurrentProduct = product;
        }
    }
}