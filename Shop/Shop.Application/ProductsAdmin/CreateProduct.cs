using Shop.Domain.Enums;
using Shop.Domain.Infrastructure;
using Shop.Domain.Models;
using System;
using System.Threading.Tasks;

namespace Shop.Application.ProductsAdmin
{
    [Service]
    public class CreateProduct
    {
        private IProductManager _productManager;

        public CreateProduct(IProductManager productManager)
        {
            _productManager = productManager;
        }

        public async Task<Response> Do(Request request)
        {
            var product = new Product
            {
                Name = request.Name,
                Description = request.Description,
                Category = request.Category,
                Value = request.Value
            };

            if (await _productManager.CreateProduct(product) <= 0)
            {
                throw new Exception("Failed to create product");
            }

            return new Response
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Category = product.Category,
                Value = product.Value
            };
        }

        private Exception Exception()
        {
            throw new NotImplementedException();
        }

        public class Request
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public Category Category { get; set; }
            public decimal Value { get; set; }
        }

        public class Response
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public Category Category { get; set; }
            public decimal Value { get; set; }
        }
    }
   
}
