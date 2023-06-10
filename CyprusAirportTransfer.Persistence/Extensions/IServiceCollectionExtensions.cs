using CyprusAirportTransfer.App.Interfaces;
using CyprusAirportTransfer.App.Interfaces.Repositories;
using CyprusAirportTransfer.Persistence.Contexts;
using CyprusAirportTransfer.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyprusAirportTransfer.Persistence.Extensions
{
    public static class IServiceCollectionExtensions
    {

        public static void AddPersistenceLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext(configuration);
        services.AddRepositories();
    }

    public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<ApplicationDbContext>(options =>
           options.UseSqlServer(connectionString,
               builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services
            .AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork))
            .AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>))
            .AddTransient<IUserRepository, UserRepository>();
    }
}
}
