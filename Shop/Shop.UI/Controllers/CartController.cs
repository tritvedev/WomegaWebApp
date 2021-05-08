using Microsoft.AspNetCore.Mvc;
using Shop.Application.Cart;
using System.Threading.Tasks;

namespace Shop.UI.Controllers
{
    [Route("{controller}/{action}")]
    public class CartController : Controller
    {
        [HttpPost("{stockId}")]
        public async Task<IActionResult> AddOne(
            int stockId,
            [FromServices] AddToCart addToCart)
        {
            var request = new AddToCart.Request
            {
                StockId = stockId,
                Qty = 1
            };

            var success = await addToCart.Do(request);

            if (success)
            {
                return Ok("Item added to Cart");
            }

            return BadRequest("Failed to add to Cart");

        }

        [HttpPost("{stockId}")]
        public async Task<IActionResult> RemoveOne(
            int stockId,
            [FromServices] RemoveFromCart removeFromCart )
        {
            var request = new RemoveFromCart.Request
            {
                StockId = stockId,
                Qty = 1
            };

            var success = await removeFromCart.Do(request);

            if (success)
            {
                return Ok("Item removed from Cart");
            }

            return BadRequest("Failed to remove from Cart");

        }

        [HttpPost("{stockId}")]
        public async Task<IActionResult> RemoveAll(
            int stockId,
            [FromServices] RemoveFromCart removeFromCart )
        {
            var request = new RemoveFromCart.Request
            {
                StockId = stockId,
                All = true
            };

            var success = await removeFromCart.Do(request);

            if (success)
            {
                return Ok("All Items removed from Cart");
            }

            return BadRequest("Failed to remove All Items from Cart");

        }

    }
}
