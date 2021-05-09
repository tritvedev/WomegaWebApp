using Shop.Database;
using Shop.Domain.Cart;
using Shop.Domain.Infrastructure;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Application.Cart
{
    public class RemoveFromCart
    {
        private ISessionManager _sessionManager;
        private IStockManager _stockManager;

        public RemoveFromCart(ISessionManager sessionManager, IStockManager stockManager)
        {
            _sessionManager = sessionManager;
            _stockManager = stockManager;
        }

        public async Task<bool> Do( Request request)
        {
            /*
             * Get the stock which is on hold
             * */

            if(request.Qty <= 0)
            {
                return false;
            }

            await _stockManager.RemoveStockFromHold(request.StockId, request.Qty, _sessionManager.GetId());
            
            _sessionManager.RemoveProduct(request.StockId, request.Qty);
           
            return true;
        }

        public class Request
        {
            public int StockId { get; set; }
            public int Qty { get; set; }
        }
    }
}
