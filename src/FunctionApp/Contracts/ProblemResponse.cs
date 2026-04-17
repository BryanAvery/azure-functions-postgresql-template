namespace FunctionApp.Contracts;

public sealed record ProblemResponse(
    string Type,
    string Title,
    int Status,
    string Detail,
    string Instance,
    IReadOnlyCollection<string>? Errors = null);
