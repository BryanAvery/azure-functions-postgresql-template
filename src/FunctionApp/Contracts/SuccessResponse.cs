namespace FunctionApp.Contracts;

public sealed record SuccessResponse<T>(T Data, string CorrelationId, DateTime TimestampUtc);
