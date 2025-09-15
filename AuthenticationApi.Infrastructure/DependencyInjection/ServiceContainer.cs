using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthenticationApi.Application.Interfaces;
using AuthenticationApi.Infrastructure.Data;
using AuthenticationApi.Infrastructure.Repositories;
using ECommerece.CommonLibrary.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuthenticationApi.Infrastructure.DependencyInjection
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection serviceCollection, IConfiguration config)
        {
            SharedServiceContainer.AddSharedService<AuthenticationDbContext>(serviceCollection, config, config["MySerilog:FileName"]!);
            serviceCollection.AddScoped<IUser, UserRepository>();
            return serviceCollection;
        }

        public static IApplicationBuilder UseInfrastructureAppBuilder(this IApplicationBuilder app)
        {
            SharedServiceContainer.AddSharedMiddlewares(app);
            return app;
        }
    }
}
