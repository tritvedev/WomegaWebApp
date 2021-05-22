using Microsoft.AspNetCore.Identity;
using Shop.Domain.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Shop.Application.Users
{
    [Service]
    public class CreateUser
    {
        private UserManager<User> _userManager;

        public CreateUser(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Response> Do(Request request)
        {
            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                Email = request.Email,
                GstNumber = request.GstNumber
            };
            var userClaim = new Claim("Role", "User");

            _userManager.CreateAsync(user, request.Password).GetAwaiter().GetResult();
            _userManager.AddClaimAsync(user, userClaim).GetAwaiter().GetResult();


            return new Response
            {
                Index = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                Password = user.PasswordHash
            };
        }

        public class Request
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string UserName { get; set; }
            public string GstNumber { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class Response
        {
            public string Index { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string UserName { get; set; }
            public string GstNumber { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
        }
    }
}
