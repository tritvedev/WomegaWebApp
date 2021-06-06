using Microsoft.AspNetCore.Identity;
using Shop.Domain.Enums;
using Shop.Domain.Infrastructure;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.AccountTickets
{
    [Service]

    public class UpdateAccountTicket
    {
        readonly IAccountTicketManager _accountTicketManager;
        readonly UserManager<User> _userManager;

        public UpdateAccountTicket(IAccountTicketManager accountTicketManager, UserManager<User> userManager)
        {
            _accountTicketManager = accountTicketManager;
            _userManager = userManager;
        }

        public async Task<Response> Do(Request request)
        {
            //Ticket Update
            var ticket = _accountTicketManager.GetAccountTicketById(request.AppUserId, x => x);
            ticket.Status = request.Status;
            await _accountTicketManager.UpdateAccountTicket(ticket);

            //User Update
            var user = _userManager.FindByIdAsync(request.AppUserId).GetAwaiter().GetResult();
            user.LockoutEnd = DateTime.Now;
            _userManager.UpdateAsync(user).GetAwaiter().GetResult();

            return new Response
            {
                AppUserId = request.AppUserId,
                CompanyName = request.CompanyName,
                Status = request.Status
            };
        }

        public class Request
        {
            public string AppUserId { get; set; }
            public string CompanyName { get; set; }
            public TicketStatus Status { get; set; }
        }

        public class Response
        {
            public string AppUserId { get; set; }
            public string CompanyName { get; set; }
            public TicketStatus Status { get; set; }
        }
    }
}
