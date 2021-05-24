using Shop.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Domain.Models
{
    public class AccountTicket
    {
        public User AppUser { get; set; }
        public string CompanyName { get; set; }
        public TicketStatus Status { get; set; }
    }
}
