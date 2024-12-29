using System.Data;
using AdventureWorks.Application.Abstractions.Data;
using AdventureWorks.Application.Abstractions.Messaging;
using AdventureWorks.Domain.SalesTerritory;
using AdventureWorks.Shared;
using Dapper;

namespace AdventureWorks.Application.SalesTerritories.GetById;
internal sealed class SalesTerritoryByIdQueryHandler
    : IQueryHandler<GetSalesTerritoryByIdQuery, SalesTerritoryResponse>
{
    private readonly IDbConnectionFactory _connectionFactory;

    public SalesTerritoryByIdQueryHandler(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }
    public async Task<Result<SalesTerritoryResponse>>
        Handle(GetSalesTerritoryByIdQuery query, CancellationToken cancellationToken)
    {
        const string sql =
            """
            SELECT TerritoryID
                ,Name
                ,CountryRegionCode
                ,[Group]
                ,SalesYTD
                ,SalesLastYear
                ,CostYTD
                ,CostLastYear
                ,rowguid
                ,ModifiedDate
            FROM Sales.SalesTerritory as st

            WHERE st.TerritoryID = @TerritoryId
            """;

        using IDbConnection connection = _connectionFactory.GetOpenConnection();

        SalesTerritoryResponse? salesTerritory = await connection.QueryFirstOrDefaultAsync<SalesTerritoryResponse>(
            sql,
            query);

        if (salesTerritory is null)
        {
            return Result.Failure<SalesTerritoryResponse>(SalesTerritoryErrors.NotFound(query.territoryId));
        }

        return salesTerritory;
    }
}
