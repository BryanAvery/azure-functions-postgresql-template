namespace FunctionApp.Domain.Models;

public sealed class Customer
{
    public Guid Id { get; init; }

    public string Email { get; init; } = string.Empty;

    public string FullName { get; init; } = string.Empty;

    public DateTime CreatedUtc { get; init; }
}
