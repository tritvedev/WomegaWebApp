using Microsoft.AspNetCore.Identity;
using Shop.Application;
using Shop.Database;
using Shop.Database.Cart;
using Shop.Domain.Cart;
using Shop.Domain.Infrastructure;
using Shop.Domain.Models;
using Shop.UI.Infrastructure;
using System.Linq;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceRegister
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection @this)
        {
            var serviceType = typeof(Service);
            var definedTypes = serviceType.Assembly.DefinedTypes;

            // get list of type info services
            var services = definedTypes.Where(x => x.GetTypeInfo().GetCustomAttribute<Service>() != null);

            foreach(var service in services)
            {
                @this.AddTransient(service);
            }


            @this.AddTransient<IStockManager, StockManager>();
            @this.AddTransient<IProductManager, ProductManager>();
            @this.AddTransient<IOrderManager, OrderManager>();
            @this.AddScoped<ISessionManager, SessionManager>();
            @this.AddTransient<IAccountTicketManager, AccountTicketManager>();
            @this.AddTransient<UserManager<User>>();

            return @this;
        }

    }
}
