using FunctionApp.Application.Interfaces;
using FunctionApp.Contracts;
using FunctionApp.Domain.Models;

namespace FunctionApp.Application.Services;

public sealed class CustomerService(ICustomerRepository customerRepository) : ICustomerService
{
    private readonly ICustomerRepository _customerRepository = customerRepository;

    public async Task<CustomerResponse?> GetCustomerAsync(Guid customerId, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(customerId, cancellationToken);
        return customer is null ? null : MapToResponse(customer);
    }

    public async Task<CreateCustomerResult> CreateCustomerAsync(CreateCustomerRequest request, CancellationToken cancellationToken)
    {
        var validationErrors = ValidateCreateRequest(request);
        if (validationErrors.Count > 0)
        {
            return CreateCustomerResult.Failure(validationErrors.ToArray());
        }

        var customer = new Customer
        {
            Id = Guid.NewGuid(),
            FullName = request.FullName.Trim(),
            Email = request.Email.Trim().ToLowerInvariant(),
            CreatedUtc = DateTime.UtcNow
        };

        var persistedCustomer = await _customerRepository.CreateAsync(customer, cancellationToken);

        return CreateCustomerResult.Success(MapToResponse(persistedCustomer));
    }

    private static CustomerResponse MapToResponse(Customer customer)
        => new(customer.Id, customer.FullName, customer.Email, customer.CreatedUtc);

    private static List<string> ValidateCreateRequest(CreateCustomerRequest request)
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(request.FullName))
        {
            errors.Add("FullName is required.");
        }

        if (string.IsNullOrWhiteSpace(request.Email))
        {
            errors.Add("Email is required.");
        }
        else if (!request.Email.Contains('@', StringComparison.Ordinal))
        {
            errors.Add("Email must be a valid email address.");
        }

        return errors;
    }
}
