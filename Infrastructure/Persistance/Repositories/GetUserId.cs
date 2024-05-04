using System.Security.Claims;
using Application.Persistance.Interfaces.ClaimUserId;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Persistance.Repositories;

public class GetUserId : IGetUserId
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetUserId(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    public ClaimsPrincipal User => _httpContextAccessor.HttpContext?.User;

    public Guid UserId => Guid.Parse(User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value);
}