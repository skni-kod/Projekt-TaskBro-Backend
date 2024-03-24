using Application.Persistance.Interfaces.AccountInterfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using BC = BCrypt.Net.BCrypt;

namespace Infrastructure.Persistance.Repositories.AccountRepositories;

public class AccountRepository : IAccountRepository
{
    private readonly TaskBroDbContext _context;

    public AccountRepository(TaskBroDbContext context)
    {
        _context = context;
    }
    
    public async Task<bool> IsEmailExist(string email, CancellationToken cancellationToken) 
        => await _context.Users.AnyAsync(x => x.Email == email, cancellationToken);

    public async Task<Guid> CreateAccount(User user, CancellationToken cancellationToken)
    {
        user.Password = BC.HashPassword(user.Password);
        
        await _context.Users.AddAsync(user, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}