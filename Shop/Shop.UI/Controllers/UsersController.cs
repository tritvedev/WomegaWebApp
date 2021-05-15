using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Users;
using Shop.UI.Pages.Accounts;
using System.Threading.Tasks;

namespace Shop.UI.Controllers
{
    [Route("{controller}")]
    public class UsersController : Controller
    {
        /*
         * 
         *          Users Controller
         * 
         */

        [HttpPost("")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUser.Request request, [FromServices] CreateUser createuser) =>
            Ok((await createuser.Do(request)));

    }
}
