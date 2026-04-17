using FunctionApp.Contracts;

namespace FunctionApp.Application.Interfaces;

public interface ICustomerService
{
    public Task<CustomerResponse?> GetCustomerAsync(Guid customerId, CancellationToken cancellationToken);

    public Task<CreateCustomerResult> CreateCustomerAsync(CreateCustomerRequest request, CancellationToken cancellationToken);
}
