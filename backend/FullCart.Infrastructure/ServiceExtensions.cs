using FullCart.Application.Contracts;
using FullCart.Application.Services;
using FullCart.Domain.Interfaces;
using FullCart.Infrastructure.Authentication;
using FullCart.Infrastructure.Context;
using FullCart.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace FullCart.Infrastructure;

public static class ServiceExtensions
{
    public static IServiceCollection ConfigurePersistence(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddDbContext<FullCartDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("Database"));
        });
        return services;
    }

    public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtOptions>(options =>
        {
            configuration.GetSection("jwt").Bind(options);
        });
        
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                var jwtOption = configuration.GetSection("jwt");
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtOption["Issuer"],
                    ValidAudience = jwtOption["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtOption["SecretKey"]))

                };
            });

        return services;
    }


    public static IServiceCollection AddRepository(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IInventoryRepository, InventoryRepository>();
        services.AddScoped<ICartRepository,CartRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<PasswordHasherService>();
        services.AddScoped<JwtProvider>();
        return services;
    }
}
