using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System.Reflection;
using Blazored.FluentValidation;
using CyprusAirportTransfer.App.UseCases.Users.Commands.CreateUser;
using FluentValidation;

namespace CyprusAirportTransfer.App.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddAutoMapper();
            services.AddMediator();
            services.AddValidators();
        }

        private static void AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }

        private static void AddMediator(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        } 

        private static void AddValidators(this IServiceCollection services)
        {
           services.AddScoped<IValidator<CreateUserCommand>, CreateUserValidator>();
        }        
    }
}
