using System.Security.Claims;

namespace Application.Persistance.Interfaces.ClaimUserId;

public interface IGetUserId
{
    ClaimsPrincipal User { get; }
    Guid UserId { get; }
}