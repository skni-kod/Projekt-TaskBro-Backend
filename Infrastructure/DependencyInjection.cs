using System.Reflection.Metadata;
using System.Text;
using Application.Account.Commands.RefreshToken;
using Application.Persistance.Interfaces.AccountInterfaces;
using FluentValidation;
using Infrastructure.Authentication;
using Infrastructure.Persistance;
using Infrastructure.Persistance.Repositories.AccountRepositories;
using Infrastructure.Persistance.Seeders;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<Seeder>();
        services.AddHttpContextAccessor();
        services.AddValidatorsFromAssemblyContaining<RefreshTokenCommand>();
        
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
    
    public static IServiceCollection AddAuthorization(this IServiceCollection services, IConfiguration configuration)
    {
        var authenticationSettings = new JwtSettings();
        configuration.GetSection(JwtSettings.SectionName).Bind(authenticationSettings);
        services.AddSingleton(authenticationSettings);

        services.AddAuthentication( opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(cfg =>
        {
            cfg.RequireHttpsMetadata = false;
            cfg.SaveToken = true;
            cfg.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = authenticationSettings.Issuer,
                ValidAudience = authenticationSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.Secret))
            };
        });

        return services;
    }
}