using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Shop.UI.Pages
{
    public class AboutModel : PageModel
    {
        public string Message { get; set; }
        public void OnGet()
        {
            Message = "Womega";
        }
    }
}
