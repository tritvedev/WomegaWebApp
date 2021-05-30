using Microsoft.AspNetCore.Identity;
using Shop.Domain.Enums;
using Shop.Domain.Infrastructure;
using Shop.Domain.Models;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Shop.Application.Users
{
    [Service]
    public class CreateUser
    {
        private readonly UserManager<User> _userManager;
        private readonly IAccountTicketManager _ticketManager;

        private readonly DateTimeOffset lockOutEnd = new DateTimeOffset(new DateTime(2100, 01, 01));

        public CreateUser(UserManager<User> userManager, IAccountTicketManager ticketManager)
        {
            _userManager = userManager;
            _ticketManager = ticketManager;
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

            Claim userClaim;

            //Extra fields fot Wholesale Users
            if (request.IsWholesaleAccount)
            {
                user.GstNumber = request.GstNumber;
                user.LockoutEnabled = true;
                user.LockoutEnd = lockOutEnd;
                var ticket = new AccountTicket
                {
                    AppUserId = user.Id,
                    CompanyName = request.CommanyName,
                    Status = TicketStatus.Pending
                };
                _ = _ticketManager.CreateAccountTicket(ticket).GetAwaiter().GetResult();
                userClaim = new Claim("Role", "WholesaleUser");
            }
            else
            {
                userClaim = new Claim("Role", "RetailUser");
            }

            _ = await _userManager.CreateAsync(user, request.Password);
            _ = _userManager.AddClaimAsync(user, userClaim).GetAwaiter().GetResult();


            return new Response
            {
                Index = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email
            };
        }

        public class Request
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string UserName { get; set; }
            public string GstNumber { get; set; }
            public string CommanyName { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public bool IsWholesaleAccount { get; set; }
        }

        public class Response
        {
            public string Index { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string UserName { get; set; }
            public string Email { get; set; }
        }
    }
}
