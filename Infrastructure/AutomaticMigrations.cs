using Infrastructure.Persistance.Seeders;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class AutomaticMigrations
{
    public static async Task ApplyPendingMigrations(this IApplicationBuilder app)
    {
        using (var scope = app.ApplicationServices.CreateScope())
        {
            var seeder = scope.ServiceProvider.GetRequiredService<Seeder>();
            await seeder.ApplyPendingMigrations();
        }
    }
}