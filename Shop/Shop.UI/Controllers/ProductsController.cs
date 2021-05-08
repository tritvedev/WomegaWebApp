using Microsoft.AspNetCore.Authorization;
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
    [Route("{controller}")]
    [Authorize(Policy = "Manager")]
    public class ProductsController : Controller
    {
        /*
         * 
         *          Products Admin Controller
         * 
         * */

        [HttpGet("")]
        public IActionResult GetProducts(
           [FromServices] GetProducts getProducts) => Ok(getProducts.Do());

        [HttpGet("{id}")]
        public IActionResult GetProduct(
            int id,
           [FromServices] GetProduct getProduct) => Ok(getProduct.Do(id));

        [HttpPost("")]
        public async Task<IActionResult> CreateProduct(
            [FromBody] CreateProduct.Request req,
           [FromServices] CreateProduct createProduct) => Ok(await createProduct.Do(req));

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(
            int id,
            [FromServices] DeleteProduct deleteProduct) => Ok(await deleteProduct.Do(id));

        [HttpPut("")]
        public async Task<IActionResult> UpdateProducts(
             [FromBody] UpdateProduct.Request req,
            [FromServices] UpdateProduct updateProduct) => Ok(await updateProduct.Do(req));

    }
}
