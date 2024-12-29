using AdventureWorks.Shared;

namespace AdventureWorks.Domain.SalesTerritory;
public static class SalesTerritoryErrors
{
    public static Error NotFound(int territoryId) => Error.NotFound(
        "SalesTerritory.NotFound",
        $"The territory with the Id = '{territoryId}' was not found");

}
