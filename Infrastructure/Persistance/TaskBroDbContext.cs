using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance;

public class TaskBroDbContext : DbContext
{
    public TaskBroDbContext(DbContextOptions<TaskBroDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<DailyTask> Tasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
    }

}