using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Stone.AppStore.Business.Application.ApiClientService.ClientFactory;
using Stone.AppStore.Business.Application.Consumer;
using Stone.AppStore.Business.Application.Mappings;
using Stone.AppStore.Business.Application.Services;
using Stone.AppStore.Business.Domain.Interfaces;
using Stone.AppStore.Business.Infrastructure.Context;
using Stone.AppStore.Business.Infrastructure.Repositories;
using System;

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

            services.AddScoped<IPaymentRepository, PaymentRepository>();

            services.AddTransient<IPaymentService, PaymentService>();

            services.AddAutoMapper(typeof(DomainToModelMappingProfile));

            services.AddHttpClient();

            services.AddTransient<PaymentConfirmationClientFactory>();

            services.AddHostedService<PaymentConsumer>();

            return services;
        }
    }
}
