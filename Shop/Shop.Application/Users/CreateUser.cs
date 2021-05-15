using Microsoft.AspNetCore.Identity;
using Shop.Domain.Models;
using System.Threading.Tasks;

namespace Shop.Application.Users
{
    [Service]
    public class CreateUser
    {
        private UserManager<User> _accountManager;

        public CreateUser(UserManager<User> accountManager)
        {
            _accountManager = accountManager;
        }

        public async Task<Response> Do(Request request)
        {
            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                Email = request.Email
            };

            _accountManager.CreateAsync(user, request.Password).GetAwaiter().GetResult();

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
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class Response
        {
            public string Index { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string UserName { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
        }
    }
}
