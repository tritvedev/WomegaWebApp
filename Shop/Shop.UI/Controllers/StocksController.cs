using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.StockAdmin;
using Shop.Database;
using System.Threading.Tasks;
namespace Shop.UI.Controllers
{
    [Route("{controller}")]
    [Authorize(Policy = "Manager")]

    public class StocksController : Controller
    {
        /*
         * 
         *          Stocks Admin Controller
         * 
         * */

        [HttpGet("")]
        public IActionResult GetStock(
            [FromServices] GetStock getStock) => Ok(getStock.Do());

        [HttpPost("")]
        public async Task<IActionResult> CreateStock(
             [FromBody] CreateStock.Request req,
            [FromServices] CreateStock createStock) => Ok(await createStock.Do(req));

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStock(
            int id,
            [FromServices] DeleteStock deleteStock) => Ok(await deleteStock.Do(id));

        [HttpPut("")]
        public async Task<IActionResult> UpdateStock(
             [FromBody] UpdateStock.Request req,
            [FromServices] UpdateStock updateStock) => Ok(await updateStock.Do(req));

    }
}
