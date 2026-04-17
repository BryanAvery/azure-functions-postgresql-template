using System.Net;
using FunctionApp.Application.Interfaces;
using FunctionApp.Contracts;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace FunctionApp.Functions;

public sealed class CreateCustomerFunction(
    ILogger<CreateCustomerFunction> logger,
    ICustomerService customerService)
{
    private readonly ILogger<CreateCustomerFunction> _logger = logger;
    private readonly ICustomerService _customerService = customerService;

    [Function("CreateCustomer")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "customers")]
        HttpRequestData request,
        FunctionContext executionContext)
    {
        var correlationId = executionContext.InvocationId;

        using var logScope = _logger.BeginScope(new Dictionary<string, object>
        {
            ["FunctionName"] = nameof(CreateCustomerFunction),
            ["InvocationId"] = correlationId
        });

        var createRequest = await request.ReadFromJsonAsync<CreateCustomerRequest>(cancellationToken: executionContext.CancellationToken);

        if (createRequest is null)
        {
            var badRequestResponse = request.CreateResponse(HttpStatusCode.BadRequest);
            await badRequestResponse.WriteAsJsonAsync(new ProblemResponse(
                Type: "https://httpstatuses.com/400",
                Title: "Invalid request",
                Status: (int)HttpStatusCode.BadRequest,
                Detail: "Request payload is required.",
                Instance: request.Url.AbsolutePath));
            return badRequestResponse;
        }

        var result = await _customerService.CreateCustomerAsync(createRequest, executionContext.CancellationToken);

        if (!result.IsSuccess)
        {
            var badRequestResponse = request.CreateResponse(HttpStatusCode.BadRequest);
            await badRequestResponse.WriteAsJsonAsync(new ProblemResponse(
                Type: "https://httpstatuses.com/400",
                Title: "Validation failed",
                Status: (int)HttpStatusCode.BadRequest,
                Detail: "Request validation failed.",
                Instance: request.Url.AbsolutePath,
                Errors: result.Errors));
            return badRequestResponse;
        }

        var response = request.CreateResponse(HttpStatusCode.Created);
        await response.WriteAsJsonAsync(new SuccessResponse<CustomerResponse>(
            Data: result.Customer!,
            CorrelationId: correlationId,
            TimestampUtc: DateTime.UtcNow));

        return response;
    }
}
