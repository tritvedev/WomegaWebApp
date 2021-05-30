using Shop.Domain.Infrastructure;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Database
{
    public class AccountTicketManager : IAccountTicketManager
    {
        private ApplicationDbContext _ctx;

        public AccountTicketManager(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public Task<int> CreateAccountTicket(AccountTicket ticket)
        {
            _ctx.AccountTickets.Add(ticket);

            return _ctx.SaveChangesAsync();
        }

        public Task<int> DeleteAccountTicket(string id)
        {
            var ticket = _ctx.AccountTickets.FirstOrDefault(x => x.AppUserId == id);
            _ctx.AccountTickets.Remove(ticket);

            return _ctx.SaveChangesAsync();
        }

        public Task<int> UpdateAccountTicket(AccountTicket ticket)
        {
            _ctx.AccountTickets.Update(ticket);

            return _ctx.SaveChangesAsync();
        }

        public TResult GetAccountTicketById<TResult>(string id, Func<AccountTicket, TResult> selector)
        {
            return _ctx.AccountTickets
                .Where(x => x.AppUserId == id)
                .Select(selector)
                .FirstOrDefault();
        }
    }
}
