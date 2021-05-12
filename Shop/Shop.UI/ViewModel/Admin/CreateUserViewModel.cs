using System.ComponentModel.DataAnnotations;

namespace Shop.UI.ViewModel.Admin
{
    public class CreateUserViewModel
    {
        [Required]
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
