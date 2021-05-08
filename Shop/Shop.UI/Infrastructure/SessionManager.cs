using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Shop.Application.Infrastructure;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shop.UI.Infrastructure
{
    public class SessionManager : ISessionManager
    {
        private readonly ISession _session;

        public SessionManager(IHttpContextAccessor httpContextAccessor)
        {
            // properly injecting HTTP context into our service
            _session = httpContextAccessor.HttpContext.Session;
        }

        public void AddCustomerInformation(CustomerInformation customer)
        {
            var stringObject = JsonConvert.SerializeObject(customer);

            _session.SetString("customer-info", stringObject);
        }

        public void AddProduct(int stockId, int qty)
        {
            var cartList = new List<CartProduct>();
            
            var stringObject = _session.GetString("cart");
            
            if (!string.IsNullOrEmpty(stringObject))
            {
                cartList = JsonConvert.DeserializeObject<List<CartProduct>>(stringObject);
            }
            
            if(cartList.Any(x => x.StockId == stockId))
            {
                cartList.Find(x => x.StockId == stockId).Qty += qty;
            }
            else
            {
                cartList.Add(new CartProduct
                {
                    StockId = stockId,
                    Qty = qty
                });
            }
            
            
            stringObject = JsonConvert.SerializeObject(cartList);
            
            _session.SetString("cart", stringObject);
        }

        public List<CartProduct> GetCart()
        {
            var stringObject = _session.GetString("cart");
            if (string.IsNullOrEmpty(stringObject))
            {
                return null;
            }
            var cartList = JsonConvert.DeserializeObject<List<CartProduct>>(stringObject);

            return cartList;
        }

        public CustomerInformation GetCustomerInformation()
        {
            var stringObject = _session.GetString("customer-info");

            if (String.IsNullOrEmpty(stringObject))
                return null;

            var customerInformation = JsonConvert.DeserializeObject<CustomerInformation>(stringObject);
            return customerInformation;
        }

        public string GetId() => _session.Id;

        public void RemoveProduct(int stockId, int qty)
        {
            var cartList = new List<CartProduct>();

            var stringObject = _session.GetString("cart");

            // if we dont have any cartlist yet
            if (string.IsNullOrEmpty(stringObject)) return;

            cartList = JsonConvert.DeserializeObject<List<CartProduct>>(stringObject);

            // if we don't have any product in the cart
            if (!cartList.Any(x => x.StockId == stockId)) return;

            // At this point, we know the stock we want to remove

            var product =cartList.First(x => x.StockId == stockId);

            product.Qty -= qty;

            // if there is 0 product left, remove that particular product from the list

            if(product.Qty <= 0)
            {
                cartList.Remove(product);
            }

            // serialize the object back and store it in the session
            stringObject = JsonConvert.SerializeObject(cartList);

            _session.SetString("cart", stringObject);

        }
    }


}
