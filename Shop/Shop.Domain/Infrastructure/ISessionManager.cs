using Shop.Domain.Models;
using System;
using System.Collections.Generic;

namespace Shop.Domain.Infrastructure
{
    public interface ISessionManager
    {
        //Procedure:  Session Id, Cart Products , save cart products to the session
        string GetId();
        void AddProduct(CartProduct cartProduct);

        // return
        IEnumerable<TResult> GetCart<TResult>(Func<CartProduct, TResult> selector);

        void AddCustomerInformation(CustomerInformation customer);
        CustomerInformation GetCustomerInformation();

        void RemoveProduct(int stockId, int qty);


    }


}
