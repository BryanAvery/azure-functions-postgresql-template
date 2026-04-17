namespace FunctionApp.Contracts;

public sealed record CustomerResponse(
    Guid Id,
    string FullName,
    string Email,
    DateTime CreatedUtc);
