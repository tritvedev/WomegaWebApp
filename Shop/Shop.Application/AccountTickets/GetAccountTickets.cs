using Microsoft.AspNetCore.Identity;
using Shop.Domain.Enums;
using Shop.Domain.Infrastructure;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Application.AccountTickets
{
    [Service]
    public class GetAccountTickets
    {
        readonly IAccountTicketManager _accountTicketManager;
        readonly UserManager<User> _userManager;

        public GetAccountTickets(IAccountTicketManager accountTicketManager, UserManager<User> userManager)
        {
            _accountTicketManager = accountTicketManager;
            _userManager = userManager;
        }

        public IEnumerable<AccountTicketViewModel> Do() =>
            _accountTicketManager.GetAccountTickets(x => new AccountTicketViewModel
            {
                UserId = x.AppUserId,
                FullName = _userManager.FindByIdAsync(x.AppUserId).Result.FullName,
                CompanyName = x.CompanyName,
                GstNumber = _userManager.FindByIdAsync(x.AppUserId).Result.GstNumber,
                Status = x.Status
            });

        public class AccountTicketViewModel
        {
            public string UserId { get; set; }
            public string FullName { get; set; }
            public string CompanyName { get; set; }
            public string GstNumber { get; set; }
            public TicketStatus Status { get; set; }
        }
    }
}
