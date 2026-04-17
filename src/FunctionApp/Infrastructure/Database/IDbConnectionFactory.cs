using System.Data;

namespace FunctionApp.Infrastructure.Database;

public interface IDbConnectionFactory
{
    public Task<IDbConnection> CreateOpenConnectionAsync(CancellationToken cancellationToken);
}
