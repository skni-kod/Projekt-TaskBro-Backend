namespace Application.Account.DTOs;

public sealed class JsonWebToken
{
    public string AccessToken { get; set; }
    public DateTimeOffset Expires { get; set; }
}