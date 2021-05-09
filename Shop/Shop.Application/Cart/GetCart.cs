using Microsoft.EntityFrameworkCore;
using Shop.Database;
using Shop.Domain.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace Shop.Application.Cart
{
    public class GetCart
    {
        private ISessionManager _sessionManager;

        public GetCart(ISessionManager sessionManager)
        {
            _sessionManager = sessionManager;
        }

        // Getting the cart from the database
        public IEnumerable<Response> Do()
        {
            // account for multiple items in the cart
            return _sessionManager
                .GetCart(x => new Response
                {
                    Name = x.ProductName,
                    Value = x.Value.GetValueString(),
                    RealValue = x.Value,
                    StockId = x.StockId,
                    Qty = x.Qty

                });

        }

        public class Response
        {
            public string Name { get; set; }
            public string Value { get; set; }
            public decimal RealValue { get; set; }
            public int Qty { get; set; }
            public int StockId { get; set; }
        }
    }
}
