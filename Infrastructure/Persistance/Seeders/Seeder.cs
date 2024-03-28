using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance.Seeders;

public class Seeder
{
    private readonly TaskBroDbContext _dbContext;

    public Seeder(TaskBroDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void ApplyPendingMigrations()
    {
        if (_dbContext.Database.CanConnect() && _dbContext.Database.IsRelational())
        {
            var pendingMigrations = _dbContext.Database.GetPendingMigrations();
            if (pendingMigrations != null && pendingMigrations.Any())
            {
                _dbContext.Database.Migrate();
            }
        }
    }
}