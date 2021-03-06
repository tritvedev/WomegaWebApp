using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Domain.Models;

namespace Shop.UI.Pages.Accounts
{
    public class LoginModel : PageModel
    {
        private SignInManager<User> _signInManager;

        public LoginModel(SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
        }

        [BindProperty]
        public LoginViewModel Input { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            var result = await _signInManager.PasswordSignInAsync(Input.Username, Input.Password, false, false);

            if (result.Succeeded)
            {
                if (User.HasClaim("Role", "Admin") || User.HasClaim("Role", "Manager"))
                {
                    return RedirectToPage("/Admin/Index");
                }
                return RedirectToPage("/Index");
            }
            else
            {
                return Page();
            }
        }

        public class LoginViewModel
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }
    }
}
