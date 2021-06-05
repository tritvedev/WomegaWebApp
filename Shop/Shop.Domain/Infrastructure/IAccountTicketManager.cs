using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Infrastructure
{
    public interface IAccountTicketManager
    {
        Task<int> CreateAccountTicket(AccountTicket ticket);
        Task<int> DeleteAccountTicket(string id);
        Task<int> UpdateAccountTicket(AccountTicket ticket);

        IEnumerable<TResult> GetAccountTickets<TResult>(Func<AccountTicket, TResult> selector);
        TResult GetAccountTicketById<TResult>(string id, Func<AccountTicket, TResult> selector);
    }
}
