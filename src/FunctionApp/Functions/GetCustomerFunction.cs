using System.Net;
using FunctionApp.Application.Interfaces;
using FunctionApp.Contracts;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace FunctionApp.Functions;

public sealed class GetCustomerFunction(
    ILogger<GetCustomerFunction> logger,
    ICustomerService customerService)
{
    private readonly ILogger<GetCustomerFunction> _logger = logger;
    private readonly ICustomerService _customerService = customerService;

    [Function("GetCustomer")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "customers/{customerId:guid}")]
        HttpRequestData request,
        Guid customerId,
        FunctionContext executionContext)
    {
        var correlationId = executionContext.InvocationId;

        using var logScope = _logger.BeginScope(new Dictionary<string, object>
        {
            ["FunctionName"] = nameof(GetCustomerFunction),
            ["CustomerId"] = customerId,
            ["InvocationId"] = correlationId
        });

        _logger.LogInformation("Looking up customer record.");

        var customer = await _customerService.GetCustomerAsync(customerId, executionContext.CancellationToken);

        if (customer is null)
        {
            _logger.LogWarning("Customer not found.");
            var notFoundResponse = request.CreateResponse(HttpStatusCode.NotFound);
            await notFoundResponse.WriteAsJsonAsync(new ProblemResponse(
                Type: "https://httpstatuses.com/404",
                Title: "Not found",
                Status: (int)HttpStatusCode.NotFound,
                Detail: $"Customer '{customerId}' was not found.",
                Instance: request.Url.AbsolutePath));
            return notFoundResponse;
        }

        var response = request.CreateResponse(HttpStatusCode.OK);
        await response.WriteAsJsonAsync(new SuccessResponse<CustomerResponse>(
            Data: customer,
            CorrelationId: correlationId,
            TimestampUtc: DateTime.UtcNow));

        _logger.LogInformation("Customer returned successfully.");

        return response;
    }
}
