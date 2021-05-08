using Shop.Application.OrdersAdmin;
using Shop.Application.ProductsAdmin;
using Shop.Application.StockAdmin;
using Shop.Application.UsersAdmin;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceRegister
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection @this)
        {
            //User services
            @this.AddTransient<CreateUser>();

            //Order sevices
            @this.AddTransient<GetOrder>();
            @this.AddTransient<GetOrders>();
            @this.AddTransient<UpdateOrder>();

            //Product services
            @this.AddTransient<GetProducts>();
            @this.AddTransient<GetProduct>();
            @this.AddTransient<CreateProduct>();
            @this.AddTransient<DeleteProduct>();
            @this.AddTransient<UpdateProduct>();

            //Stock services
            @this.AddTransient<GetStock>();
            @this.AddTransient<CreateStock>();
            @this.AddTransient<DeleteStock>();
            @this.AddTransient<UpdateStock>();

            return @this;
        }

    }
}
