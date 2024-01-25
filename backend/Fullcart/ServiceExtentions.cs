using FullCart.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FullCart;

public static class ServiceExtentions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddTransient<AuthService>();
        services.AddTransient<UserService>();
        services.AddTransient<CategoryService>();
        services.AddTransient<InventoryService>();
        services.AddTransient<CartService>();
        services.AddTransient<OrderService>();
        return services;
    }
}
