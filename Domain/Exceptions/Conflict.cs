namespace Domain.Exceptions;

public class Conflict : Exception
{
    public Conflict(string message) : base(message) { }
}