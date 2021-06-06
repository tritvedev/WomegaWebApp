using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.AccountTickets;

namespace Shop.UI.Pages.Admin.AccountTickets
{
    public class AccountTicket_DeclinedModel : PageModel
    {
        public IEnumerable<GetAccountTickets.AccountTicketViewModel> AccountTickets { get; set; }

        public void OnGet([FromServices] GetAccountTickets getAccountTickets)
        {
            AccountTickets = getAccountTickets.Do();
        }
    }
}
