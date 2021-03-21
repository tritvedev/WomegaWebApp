using Microsoft.AspNetCore.Mvc;
using Shop.Application.ProductsAdmin;
using Shop.Database;
using System.Threading.Tasks;

namespace Shop.UI.Controllers
{
    [Route("[controller]")]         // route to point towards our products
    public class AdminController : Controller
    {
        private ApplicationDbContext _ctx;

        public AdminController(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet("products")]
        public IActionResult GetProducts() => Ok(new GetProducts(_ctx).Do());

        [HttpGet("products/{Id}")]
        public IActionResult GetProduct(int Id) => Ok(new GetProduct(_ctx).Do(Id));

        [HttpPost("products")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProduct.Request request) => Ok(await new CreateProduct(_ctx).Do(request)); // FromBody because we are putting data in JSON format

        [HttpDelete("products/{Id}")]
        public async Task<IActionResult> DeleteProduct(int Id) => Ok((await new DeleteProduct(_ctx).Do(Id)));

        [HttpPut("products")]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProduct.Request request) => Ok(await new UpdateProduct(_ctx).Do(request)); // FromBody because we are updating data in JSON format
    }
}
