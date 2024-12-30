using System.Data;
using AdventureWorks.Application.Abstractions.Data;
using AdventureWorks.Application.Abstractions.Messaging;
using AdventureWorks.Shared;
using Dapper;

namespace AdventureWorks.Application.Reports.Sales.GetLast10SalesWithCustomerAddress;
public interface IGetLast10SalesWithCustomerAddressQueryHandler
{
    Task<Result<List<SalesOrderWithCustomerAddressResponse>>> Handle(
        GetLast10SalesWithCustomerAddressQuery request,
        CancellationToken cancellationToken);
}
public interface IDapperWrapper
{
    Task<IEnumerable<T>> QueryAsync<T>(IDbConnection connection, string sql, object? param = null);
}
public class DapperWrapper : IDapperWrapper
{
    public async Task<IEnumerable<T>> QueryAsync<T>(IDbConnection connection, string sql, object? param = null)
    {
        return await connection.QueryAsync<T>(sql, param);
    }
}
internal sealed class GetLast10SalesWithCustomerAddressQueryHandler
    : IGetLast10SalesWithCustomerAddressQueryHandler,
      IQueryHandler<GetLast10SalesWithCustomerAddressQuery,
      List<SalesOrderWithCustomerAddressResponse>>
{
    private readonly IDbConnectionFactory _connectionFactory;
    private readonly IDapperWrapper _dapperWrapper;

    public GetLast10SalesWithCustomerAddressQueryHandler(
        IDbConnectionFactory connectionFactory, IDapperWrapper dapperWrapper)
    {
        _connectionFactory = connectionFactory;
        _dapperWrapper = dapperWrapper;
    }

    public async Task<Result<List<SalesOrderWithCustomerAddressResponse>>> Handle(GetLast10SalesWithCustomerAddressQuery request, CancellationToken cancellationToken)
    {
        const string sql = "EXEC uspGetLast10SalesWithCustomerAddress";

        using IDbConnection connection = _connectionFactory.GetOpenConnection();

        IEnumerable<SalesOrderWithCustomerAddressResponse>? salesOrders = await _dapperWrapper.QueryAsync<SalesOrderWithCustomerAddressResponse>(connection, sql);

        if (salesOrders is null || !salesOrders.Any())
        {
            return Result.Failure<List<SalesOrderWithCustomerAddressResponse>>(ReportErrors.NotFound("uspGetLast10SalesWithCustomerAddress"));
        }

        return Result.Success(salesOrders.ToList());
    }
}
