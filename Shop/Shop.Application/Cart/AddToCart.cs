using Shop.Application.Infrastructure;
using Shop.Database;
using Shop.Domain.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Application.Cart
{
    public class AddToCart
    {
        private ISessionManager _sessionManager;
        private ApplicationDbContext _ctx;

        public AddToCart(ISessionManager sessionManager, ApplicationDbContext ctx)
        {
            _sessionManager = sessionManager;
            _ctx = ctx;
        }

        public async Task<bool> Do( Request request)
        {
            var stockOnHold = _ctx.StocksOnHold.AsEnumerable().Where(x => x.SessionId == _sessionManager.GetId()).ToList();
            var stockToHold = _ctx.Stock.Where(x => x.Id == request.StockId).FirstOrDefault();

            if(stockToHold.Qty < request.Qty)
            {
                //return not enough stock
                return false;
            }

            // if item exists in the stockOnHold, then increment it's quantity otherwise add it
            if(stockOnHold.Any(x => x.StockId == request.StockId)) // if there is any item in the list
            {
                // add to stock on hold
                stockOnHold.Find(x => x.StockId == request.StockId).Qty += request.Qty;
            }
            else
            {
                _ctx.StocksOnHold.Add(new StocksOnHold
                {
                    StockId = stockToHold.Id,
                    SessionId = _sessionManager.GetId(),
                    Qty = request.Qty,
                    ExpiryDate = DateTime.Now.AddMinutes(20)        // hold it for 20 mins
                });
            }

            stockToHold.Qty -= request.Qty;

            foreach (var stock in stockOnHold)
            {
                stock.ExpiryDate = DateTime.Now.AddMinutes(20);
            }

            await _ctx.SaveChangesAsync();

            _sessionManager.AddProduct(request.StockId, request.Qty);


            return true;
        }

        public class Request
        {
            public int StockId { get; set; }
            public int Qty { get; set; }
        }
    }
}
