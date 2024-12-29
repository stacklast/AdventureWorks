using System.Data;
using AdventureWorks.Application.Abstractions.Data;
using Microsoft.Data.SqlClient;

namespace AdventureWorks.Infrastructure.Persistence;
internal sealed class DbConnectionFactory : IDbConnectionFactory
{
    private readonly string _connectionString;

    public DbConnectionFactory(string connectionString)
    {
        _connectionString = connectionString;
    }

    public IDbConnection GetOpenConnection()
    {
        var connection = new SqlConnection(_connectionString);
        connection.Open();
        return connection;
    }
}
