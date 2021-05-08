using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Application.Infrastructure
{
    public interface ISessionManager
    {
        //Procedure:  Session Id, Cart Products , save cart products to the session
        string GetId();
        void AddProduct(CartProduct cartProduct);

        // return
        List<CartProduct> GetProducts();

    }
}
