using System.Data;
using AdventureWorks.Application.Abstractions.Data;
using AdventureWorks.Application.Abstractions.Messaging;
using AdventureWorks.Shared;
using Dapper;

namespace AdventureWorks.Application.Reports.Sales.GetLast10SalesWithCustomerAddress;
internal sealed class GetLast10SalesWithCustomerAddressQueryHandler
    : IQueryHandler<GetLast10SalesWithCustomerAddressQuery, List<SalesOrderWithCustomerAddressResponse>>
{
    private readonly IDbConnectionFactory _connectionFactory;

    public GetLast10SalesWithCustomerAddressQueryHandler(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<Result<List<SalesOrderWithCustomerAddressResponse>>> Handle(GetLast10SalesWithCustomerAddressQuery request, CancellationToken cancellationToken)
    {
        const string sql = "EXEC uspGetLast10SalesWithCustomerAddress";

        using IDbConnection connection = _connectionFactory.GetOpenConnection();

        IEnumerable<SalesOrderWithCustomerAddressResponse>? salesOrders = await connection.QueryAsync<SalesOrderWithCustomerAddressResponse>(sql);

        if (salesOrders is null || !salesOrders.Any())
        {
            return Result.Failure<List<SalesOrderWithCustomerAddressResponse>>(ReportErrors.NotFound("uspGetLast10SalesWithCustomerAddress"));
        }

        return Result.Success(salesOrders.ToList());
    }
}
