using System.Net;
using Dapper;
using FunctionApp.Contracts;
using FunctionApp.Infrastructure.Database;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace FunctionApp.Functions;

public sealed class HealthCheckFunction(
    ILogger<HealthCheckFunction> logger,
    IDbConnectionFactory dbConnectionFactory)
{
    private readonly ILogger<HealthCheckFunction> _logger = logger;
    private readonly IDbConnectionFactory _dbConnectionFactory = dbConnectionFactory;

    [Function("ApiHealthCheck")]
    public async Task<HttpResponseData> Api(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "health/api")]
        HttpRequestData request,
        FunctionContext executionContext)
    {
        using var logScope = _logger.BeginScope(new Dictionary<string, object>
        {
            ["FunctionName"] = nameof(HealthCheckFunction),
            ["CheckType"] = "api",
            ["InvocationId"] = executionContext.InvocationId
        });

        _logger.LogInformation("API health check requested.");

        var response = request.CreateResponse(HttpStatusCode.OK);
        await response.WriteAsJsonAsync(new SuccessResponse<object>(
            Data: new { Status = "healthy", Service = "api" },
            CorrelationId: executionContext.InvocationId,
            TimestampUtc: DateTime.UtcNow));

        return response;
    }

    [Function("DatabaseHealthCheck")]
    public async Task<HttpResponseData> Database(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "health/database")]
        HttpRequestData request,
        FunctionContext executionContext)
    {
        using var logScope = _logger.BeginScope(new Dictionary<string, object>
        {
            ["FunctionName"] = nameof(HealthCheckFunction),
            ["CheckType"] = "database",
            ["InvocationId"] = executionContext.InvocationId
        });

        try
        {
            using var connection = await _dbConnectionFactory.CreateOpenConnectionAsync(executionContext.CancellationToken);

            var databaseResult = await connection.ExecuteScalarAsync<int>(
                new CommandDefinition("SELECT 1", cancellationToken: executionContext.CancellationToken));

            var statusCode = databaseResult == 1 ? HttpStatusCode.OK : HttpStatusCode.ServiceUnavailable;
            var status = databaseResult == 1 ? "healthy" : "unhealthy";

            var response = request.CreateResponse(statusCode);
            await response.WriteAsJsonAsync(new SuccessResponse<object>(
                Data: new { Status = status, Service = "database" },
                CorrelationId: executionContext.InvocationId,
                TimestampUtc: DateTime.UtcNow));

            return response;
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Database health check failed.");

            var response = request.CreateResponse(HttpStatusCode.ServiceUnavailable);
            await response.WriteAsJsonAsync(new ProblemResponse(
                Type: "https://httpstatuses.com/503",
                Title: "Service unavailable",
                Status: (int)HttpStatusCode.ServiceUnavailable,
                Detail: "Database health check failed.",
                Instance: request.Url.AbsolutePath));

            return response;
        }
    }
}
