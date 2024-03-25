using Application.Account.DTOs;
using Domain.Entities;

namespace Application.Persistance.Interfaces.AccountInterfaces;

public interface IAccountRepository
{
    Task<bool> IsEmailExist(string email, CancellationToken cancellationToken);
    Task<Guid> CreateAccount(User user, CancellationToken cancellationToken);
    Task<User> SignIn(string email, string password, CancellationToken cancellationToken);
}