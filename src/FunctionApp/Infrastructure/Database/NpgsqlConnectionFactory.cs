using System.Data;
using FunctionApp.Configuration;
using Microsoft.Extensions.Options;
using Npgsql;

namespace FunctionApp.Infrastructure.Database;

public sealed class NpgsqlConnectionFactory(IOptions<PostgresOptions> postgresOptions) : IDbConnectionFactory
{
    private readonly string _connectionString = postgresOptions.Value.ConnectionString;

    public async Task<IDbConnection> CreateOpenConnectionAsync(CancellationToken cancellationToken)
    {
        var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);
        return connection;
    }
}
