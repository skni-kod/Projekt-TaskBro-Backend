using Application.Account.DTOs;

namespace Application.Persistance.Interfaces.AccountInterfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(Guid userId, string name, string surname);
    
}