using Shop.Domain.Enums;
using Shop.Domain.Infrastructure;
using System.Collections.Generic;

namespace Shop.Application.ProductsAdmin
{
    [Service]
    public class GetProducts
    {
        private IProductManager _productManager;

        public GetProducts(IProductManager productManager)
        {
            _productManager = productManager;
        }

        // gives the list of products in database
        public IEnumerable<ProductViewModel> Do() =>
            _productManager.GetProductsWithStock(x => new ProductViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Value = x.Value,
                Category = x.Category
            });

        public class ProductViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public Category Category { get; set; }
            public decimal Value { get; set; }
        }
    }
}
