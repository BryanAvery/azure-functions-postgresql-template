using FunctionApp.Domain.Models;

namespace FunctionApp.Application.Interfaces;

public interface ICustomerRepository
{
    public Task<Customer?> GetByIdAsync(Guid customerId, CancellationToken cancellationToken);

    public Task<Customer> CreateAsync(Customer customer, CancellationToken cancellationToken);
}
