using Microsoft.AspNetCore.Identity;
using Shop.Domain.Enums;

namespace Shop.Domain.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string GstNumber { get; set; }
    }
}
