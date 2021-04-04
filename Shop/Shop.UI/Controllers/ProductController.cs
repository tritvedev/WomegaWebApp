using Microsoft.AspNetCore.Mvc;
using Shop.Application.ProductsAdmin;
using Shop.Application.StockAdmin;
using Shop.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.UI.Controllers
{    
    public class ProductController : BaseController
    {
        public ProductController(ApplicationDbContext ctx):base(ctx)
        {
        }

        [HttpGet]
        public IActionResult GetProducts() => Ok(new GetProducts(_ctx).Do());

        [HttpGet]
        public IActionResult GetProduct(int id) => Ok(new GetProduct(_ctx).Do(id));

        [HttpPost("products")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProduct.Request request) => Ok(await new CreateProduct(_ctx).Do(request));

        [HttpDelete("products/{id}")]
        public async Task<IActionResult> DeleteProduct(int id) => Ok(await new DeleteProduct(_ctx).Do(id));

        [HttpPut("products")]
        public async Task<IActionResult> UpdateProducts([FromBody] UpdateProduct.Request request) => Ok(await new UpdateProduct(_ctx).Do(request));

    }
}
