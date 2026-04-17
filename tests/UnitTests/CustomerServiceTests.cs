using FluentAssertions;
using FunctionApp.Application.Interfaces;
using FunctionApp.Application.Services;
using FunctionApp.Contracts;
using FunctionApp.Domain.Models;
using Moq;
using Xunit;

namespace UnitTests;

public sealed class CustomerServiceTests
{
    private readonly Mock<ICustomerRepository> _customerRepository = new();

    [Fact]
    public async Task GetCustomerAsync_ReturnsMappedResponse_WhenCustomerExists()
    {
        var customerId = Guid.NewGuid();
        var createdUtc = new DateTime(2026, 4, 14, 10, 0, 0, DateTimeKind.Utc);

        _customerRepository
            .Setup(repository => repository.GetByIdAsync(customerId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Customer
            {
                Id = customerId,
                Email = "alice@example.com",
                FullName = "Alice Smith",
                CreatedUtc = createdUtc
            });

        var service = new CustomerService(_customerRepository.Object);

        var result = await service.GetCustomerAsync(customerId, CancellationToken.None);

        result.Should().NotBeNull();
        result!.Id.Should().Be(customerId);
        result.FullName.Should().Be("Alice Smith");
        result.Email.Should().Be("alice@example.com");
        result.CreatedUtc.Should().Be(createdUtc);
    }

    [Fact]
    public async Task GetCustomerAsync_ReturnsNull_WhenCustomerDoesNotExist()
    {
        var customerId = Guid.NewGuid();

        _customerRepository
            .Setup(repository => repository.GetByIdAsync(customerId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Customer?)null);

        var service = new CustomerService(_customerRepository.Object);

        var result = await service.GetCustomerAsync(customerId, CancellationToken.None);

        result.Should().BeNull();
    }

    [Fact]
    public async Task CreateCustomerAsync_ReturnsValidationErrors_WhenRequestIsInvalid()
    {
        var service = new CustomerService(_customerRepository.Object);

        var result = await service.CreateCustomerAsync(new CreateCustomerRequest("", "not-an-email"), CancellationToken.None);

        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().Contain("FullName is required.");
        result.Errors.Should().Contain("Email must be a valid email address.");
        _customerRepository.Verify(repository => repository.CreateAsync(It.IsAny<Customer>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task CreateCustomerAsync_CreatesCustomer_WhenRequestIsValid()
    {
        var createdUtc = new DateTime(2026, 4, 15, 9, 30, 0, DateTimeKind.Utc);

        _customerRepository
            .Setup(repository => repository.CreateAsync(It.IsAny<Customer>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Customer customer, CancellationToken _) => new Customer
            {
                Id = customer.Id,
                FullName = customer.FullName,
                Email = customer.Email,
                CreatedUtc = createdUtc
            });

        var service = new CustomerService(_customerRepository.Object);

        var result = await service.CreateCustomerAsync(
            new CreateCustomerRequest("Alice Smith", "Alice@Example.com"),
            CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.Customer.Should().NotBeNull();
        result.Customer!.Email.Should().Be("alice@example.com");
        result.Customer.FullName.Should().Be("Alice Smith");
    }
}
