using Shop.Application.Infrastructure;
using Shop.Database;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Application.Cart
{
    public class RemoveFromCart
    {
        private ISessionManager _sessionManager;
        private ApplicationDbContext _ctx;

        public RemoveFromCart(ISessionManager sessionManager, ApplicationDbContext ctx)
        {
            _sessionManager = sessionManager;
            _ctx = ctx;
        }

        public async Task<bool> Do( Request request)
        { 
            /*
             * Get the stock which is on hold
             * */
            var stockOnHold = _ctx.StocksOnHold
                .FirstOrDefault(x => x.StockId == request.StockId
                                && x.SessionId == _sessionManager.GetId()) ;

            var stock = _ctx.Stock.FirstOrDefault(x => x.Id == request.StockId);
            
            if (request.All)
            {
                stock.Qty += stockOnHold.Qty;
                _sessionManager.RemoveProduct(request.StockId, stockOnHold.Qty);
                stockOnHold.Qty = 0;
            }
            else
            {
                stock.Qty += request.Qty;
                stockOnHold.Qty -= request.Qty;
                _sessionManager.RemoveProduct(request.StockId, request.Qty);
            }


            if (stockOnHold.Qty <= 0)
            {
                // remove the stock on hold from the database
                _ctx.Remove(stockOnHold);
            }

            await _ctx.SaveChangesAsync();

            return true;
        }

        public class Request
        {
            public int StockId { get; set; }
            public int Qty { get; set; }
            public bool All { get; set; }
        }
    }
}
