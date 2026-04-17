using Dapper;
using FluentAssertions;
using Npgsql;
using Xunit;

namespace IntegrationTests;

public sealed class DatabaseConnectivityTests
{
    [Fact]
    public async Task PostgreSqlConnection_CanRunSimpleQuery_WhenConnectionStringProvided()
    {
        var connectionString = Environment.GetEnvironmentVariable("INTEGRATION_TEST_POSTGRES_CONNECTION_STRING");

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            return;
        }

        await using var connection = new NpgsqlConnection(connectionString);
        await connection.OpenAsync();

        var result = await connection.ExecuteScalarAsync<int>("SELECT 1;");

        result.Should().Be(1);
    }
}
