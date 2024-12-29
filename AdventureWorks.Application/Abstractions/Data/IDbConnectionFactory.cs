using System.Data;

namespace AdventureWorks.Application.Abstractions.Data;
public interface IDbConnectionFactory
{
    IDbConnection GetOpenConnection();
}
