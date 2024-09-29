using Application.Common;
using Application.Services;
using Core.Events;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.EventHandlers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class InfrastructureServiceExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PaymentDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // Event Handlers
            services.AddScoped<DomainEventDispatcher>();
            services.AddScoped<IEventHandler<TransactionCancelledEvent>, TransactionCancelledEventHandler>();
            services.AddScoped<IEventHandler<TransactionSucceededEvent>, TransactionSucceededEventHandler>();
            services.AddScoped<IEventHandler<TransactionRefundedEvent>, TransactionRefundedEventHandler>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IBankService, BankService>();

            return services;
        }
    }
}
