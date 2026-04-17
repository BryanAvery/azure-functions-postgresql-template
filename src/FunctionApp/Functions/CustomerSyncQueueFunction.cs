using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace FunctionApp.Functions;

public sealed class CustomerSyncQueueFunction(ILogger<CustomerSyncQueueFunction> logger)
{
    private readonly ILogger<CustomerSyncQueueFunction> _logger = logger;

    [Function("CustomerSyncQueue")]
    public Task Run(
        [QueueTrigger("customer-sync", Connection = "AzureWebJobsStorage")]
        string message,
        FunctionContext executionContext)
    {
        using var scope = _logger.BeginScope(new Dictionary<string, object>
        {
            ["FunctionName"] = nameof(CustomerSyncQueueFunction),
            ["InvocationId"] = executionContext.InvocationId
        });

        _logger.LogInformation("Received customer sync message. Size: {MessageLength}", message.Length);
        return Task.CompletedTask;
    }
}
