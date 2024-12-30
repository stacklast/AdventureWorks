using AdventureWorks.Shared;

namespace AdventureWorks.Domain.SalesTerritories;
public static class SalesTerritoryErrors
{
    public static Error NotFound(int territoryId) => Error.NotFound(
        "SalesTerritory.NotFound",
        $"The territory with the Id = '{territoryId}' was not found");
    public static Error NotFound(Guid territoryRowguid) => Error.NotFound(
        "SalesTerritory.NotFound",
        $"The territory with the Rowguid = '{territoryRowguid}' was not found");
}
