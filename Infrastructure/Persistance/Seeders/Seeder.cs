using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance.Seeders;

public class Seeder
{
    private readonly TaskBroDbContext _dbContext;

    public Seeder(TaskBroDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task ApplyPendingMigrations()
    {
        if (await _dbContext.Database.CanConnectAsync() && _dbContext.Database.IsRelational())
        {
            var pendingMigrations = await _dbContext.Database.GetPendingMigrationsAsync();
            if (pendingMigrations != null && pendingMigrations.Any())
            {
                await _dbContext.Database.MigrateAsync();
            }
        }
    }
}