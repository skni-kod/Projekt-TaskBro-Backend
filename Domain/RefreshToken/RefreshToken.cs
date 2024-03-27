namespace Domain.RefreshToken;

public class RefreshToken
{
    public required string Token { get; set; }
    public DateTimeOffset Created { get; set; } = DateTimeOffset.Now;
    public DateTimeOffset Expires { get; set; }
}