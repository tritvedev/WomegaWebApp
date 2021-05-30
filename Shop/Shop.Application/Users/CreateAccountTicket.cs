using Shop.Domain.Enums;
using Shop.Domain.Infrastructure;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Users
{
    public class CreateAccountTicket
    {
        private IAccountTicketManager _ticketManager;

        public CreateAccountTicket(IAccountTicketManager ticketManager)
        {
            _ticketManager = ticketManager;
        }

        public async Task<Response> Do(Request request)
        {
            var ticket = new AccountTicket
            {
                AppUserId = request.AppUserId,
                CompanyName = request.CompanyName,
                Status = TicketStatus.Pending
            };

            if (await _ticketManager.CreateAccountTicket(ticket) <= 0)
            {
                throw new Exception("Failed to create an Accoutn Ticket");
            }

            return new Response
            {
                AppUserId = ticket.AppUserId,
                CompanyName = ticket.CompanyName,
                Status = ticket.Status
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
