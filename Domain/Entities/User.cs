namespace Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string RefreshToken { get; set; } = string.Empty;
    public DateTimeOffset TokenCreated { get; set; }
    public DateTimeOffset TokenExpires { get; set; }

    public User() {}

    public User(string name, string surname, string email, string password)
    {
        Name = name;
        Surname = surname;
        Email = email;
        Password = password;
    }
}