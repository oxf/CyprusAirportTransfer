using CyprusAirportTransfer.App.Interfaces;
using CyprusAirportTransfer.Core.Common;
using CyprusAirportTransfer.Core.Commons.Interfaces;
using CyprusAirportTransfer.Infrastructure.Services;
using MediatR;

using Microsoft.Extensions.DependencyInjection;

namespace CyprusAirportTransfer.Infrastructure.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddInfrastructureLayer(this IServiceCollection services)
        {
            services.AddServices();
        }

        private static void AddServices(this IServiceCollection services)
        {
            services
                .AddTransient<IMediator, Mediator>()
                .AddTransient<IDomainEventDispatcher, DomainEventDispatcher>()
                .AddTransient<IDateTimeService, DateTimeService>()
                .AddTransient<IEmailService, EmailService>()
                .AddTransient<IHashingService, HashingService>();
        }
    }
}
