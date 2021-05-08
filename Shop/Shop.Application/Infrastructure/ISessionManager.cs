using Shop.Domain.Models;
using System.Collections.Generic;

namespace Shop.Application.Infrastructure
{
    public interface ISessionManager
    {
        //Procedure:  Session Id, Cart Products , save cart products to the session
        string GetId();
        void AddProduct(int stockId, int qty);

        // return
        List<CartProduct> GetCart();

        void AddCustomerInformation(CustomerInformation customer);
        CustomerInformation GetCustomerInformation();

        void RemoveProduct(int stockId, int qty);


    }


}
