using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Stone.AppStore.Business.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stone.AppStore.Business.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<AppStoreBusinessDbContext>(options =>
             options.UseSqlServer(configuration.GetConnectionString("AppStoreBusinessConnection"),
             b => b.MigrationsAssembly(typeof(AppStoreBusinessDbContext).Assembly.FullName)));

            //services.AddScoped<IAppRepository, AppRepository>();
            //services.AddScoped<IAppService, AppService>();

            //services.AddSingleton<IPaymentSender, PaymentSender>();


            //services.AddAutoMapper(typeof(DomainToModelMappingProfile));

            var myhandlers = AppDomain.CurrentDomain.Load("Stone.AppStore.Business.Application");
            services.AddMediatR(myhandlers);

            return services;
        }
    }
}
