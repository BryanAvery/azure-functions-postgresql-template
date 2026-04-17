using Dapper;
using FunctionApp.Application.Interfaces;
using FunctionApp.Domain.Models;
using FunctionApp.Infrastructure.Database;

namespace FunctionApp.Infrastructure.Repositories;

public sealed class CustomerRepository(IDbConnectionFactory dbConnectionFactory) : ICustomerRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory = dbConnectionFactory;

    public async Task<Customer?> GetByIdAsync(Guid customerId, CancellationToken cancellationToken)
    {
        const string sql = """
            SELECT
                id,
                email,
                full_name AS FullName,
                created_utc AS CreatedUtc
            FROM customers
            WHERE id = @CustomerId;
            """;

        using var connection = await _dbConnectionFactory.CreateOpenConnectionAsync(cancellationToken);

        var command = new CommandDefinition(
            commandText: sql,
            parameters: new { CustomerId = customerId },
            cancellationToken: cancellationToken);

        return await connection.QuerySingleOrDefaultAsync<Customer>(command);
    }

    public async Task<Customer> CreateAsync(Customer customer, CancellationToken cancellationToken)
    {
        const string sql = """
            INSERT INTO customers (id, full_name, email, created_utc)
            VALUES (@Id, @FullName, @Email, @CreatedUtc)
            RETURNING id, full_name AS FullName, email, created_utc AS CreatedUtc;
            """;

        using var connection = await _dbConnectionFactory.CreateOpenConnectionAsync(cancellationToken);

        var command = new CommandDefinition(
            commandText: sql,
            parameters: customer,
            cancellationToken: cancellationToken,
            commandTimeout: 30);

        return await connection.QuerySingleAsync<Customer>(command);
    }
}
