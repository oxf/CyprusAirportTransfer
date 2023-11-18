using CyprusAirportTransfer.App.Interfaces;
using CyprusAirportTransfer.Core.Common;
using CyprusAirportTransfer.Core.Commons.Interfaces;
using CyprusAirportTransfer.Infrastructure.Services;
using CyprusAirportTransfer.Persistence.Contexts;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Telegram.Bot;

namespace CyprusAirportTransfer.Infrastructure.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddServices(configuration);
            services.AddAuthorization(configuration);
        }

        private static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddTransient<IMediator, Mediator>()
                .AddTransient<IDomainEventDispatcher, DomainEventDispatcher>()
                .AddTransient<IDateTimeService, DateTimeService>()
                .AddTransient<IEmailService, EmailService>()
                .AddTransient<IHashingService, HashingService>()
                .AddHttpClient("telegram_bot_client")
                .AddTypedClient<ITelegramBotClient>((httpClient, sp) =>
                {
                    TelegramBotClientOptions options = new TelegramBotClientOptions(configuration.GetSection("TelegramSettings").Get<TelegramSettings>().TelegramBotToken);
                    return new TelegramBotClient(options, httpClient);
                });
                services.AddTransient<IChatbotService, TelegramBotService>();

            
        }

        private static void AddAuthorization(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 8;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings
                options.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            var jwtSettings = configuration.GetSection("JwtSettings");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings["Issuer"],
                        ValidAudience = jwtSettings["Audience"],
                        IssuerSigningKey = key
                    };
                });
        }
    }


    public class TelegramSettings
    {
        public string TelegramBotToken { get; set; }
    }
}
