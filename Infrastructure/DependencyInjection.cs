using System.Reflection.Metadata;
using Application.Persistance.Interfaces.AccountInterfaces;
using Infrastructure.Persistance;
using Infrastructure.Persistance.Repositories.AccountRepositories;
using Infrastructure.Persistance.Seeders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<Seeder>();
        
        return services;
    }

    public static IServiceCollection AddDatabaseConntection(this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.AddDbContext<TaskBroDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("WebApiDatabase"),
                r => r.MigrationsAssembly("Infrastructure")));
        
        return services;
    }
}