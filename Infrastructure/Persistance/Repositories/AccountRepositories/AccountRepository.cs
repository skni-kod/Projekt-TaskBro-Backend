using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Application.Persistance.Interfaces.AccountInterfaces;
using Domain.Entities;
using Domain.RefreshToken;
using Infrastructure.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using BC = BCrypt.Net.BCrypt;

namespace Infrastructure.Persistance.Repositories.AccountRepositories;

public class AccountRepository : IAccountRepository
{
    private readonly TaskBroDbContext _context;
    private readonly JwtSettings _jwtSettings;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AccountRepository(TaskBroDbContext context, JwtSettings jwtSettings, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _jwtSettings = jwtSettings;
        _httpContextAccessor = httpContextAccessor;
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

    public async Task<string> SignIn(string email, string password, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email, cancellationToken);

        if (user is null) return null;

        var passwordVerification = BCrypt.Net.BCrypt.Verify(password, user.Password);

        if (!passwordVerification) return null;

        var token = GenerateToken(user.Id, user.Name, user.Surname);
        
        var refreshToken = GenerateRefreshToken();
        SetRefreshToken(refreshToken, user);

        await _context.SaveChangesAsync(cancellationToken);
        return token;
    }

    public async Task<string> RefreshToken(string refreshToken, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.RefreshToken == refreshToken, cancellationToken);

        if (user is null || user.TokenExpires < DateTimeOffset.Now) return null;

        var newToken = GenerateToken(user.Id, user.Name, user.Surname);
        var newRefreshToken = GenerateRefreshToken();
        SetRefreshToken(newRefreshToken, user);

        await _context.SaveChangesAsync(cancellationToken);
        return newToken;
    }

    private string GenerateToken(Guid userId, string name, string surname)
    {
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
            new Claim(ClaimTypes.Name, name),
            new Claim(ClaimTypes.Surname, surname)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.Now.AddMinutes(_jwtSettings.ExpiryMinutes);

        var token = new JwtSecurityToken(
            _jwtSettings.Issuer,
            _jwtSettings.Audience,
            claims,
            expires: expires,
            signingCredentials: cred
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    
    private RefreshToken GenerateRefreshToken()
    {
        return new RefreshToken
        {
            Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
            Expires = DateTimeOffset.Now.AddDays(7)
        };
    }

    private void SetRefreshToken(RefreshToken newRefreshToken, User user)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = newRefreshToken.Expires
        };
        _httpContextAccessor.HttpContext.Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);

        user.RefreshToken = newRefreshToken.Token;
        user.TokenCreated = newRefreshToken.Created;
        user.TokenExpires = newRefreshToken.Expires;
    }
}