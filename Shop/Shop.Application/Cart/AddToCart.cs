using Shop.Domain.Cart;
using Shop.Domain.Infrastructure;
using Shop.Domain.Models;
using System.Threading.Tasks;

namespace Shop.Application.Cart
{
    [Service]
    public class AddToCart
    {
        private ISessionManager _sessionManager;
        private IStockManager _stockManager;

        public AddToCart(ISessionManager sessionManager, IStockManager stockManager)
        {
            _sessionManager = sessionManager;
            _stockManager = stockManager;
        }

        public async Task<bool> Do( Request request)
        {

            /*
             * Service Responsibility : where it's using the database to check if we have enough stock
             */

            // the product that we get from the context to StockToHold, just to check if we have enough quantity
            if (!_stockManager.EnoughStock(request.StockId, request.Qty))
            {
                //return not enough stock
                return false;
            }
            await _stockManager.PutStockOnHold(request.StockId, request.Qty, _sessionManager.GetId());

            var stock = _stockManager.GetStockWithProduct(request.StockId);
            var cartProduct = new CartProduct() 
            { 
                ProductId = stock.ProductId,
                ProductName = stock.Product.Name,
                StockId = stock.Id,
                Qty = request.Qty,
                Value =  stock.Product.Value
            };

            _sessionManager.AddProduct(cartProduct);

            return true;
        }

        public class Request
        {
            public int StockId { get; set; }
            public int Qty { get; set; }
        }
    }
}
