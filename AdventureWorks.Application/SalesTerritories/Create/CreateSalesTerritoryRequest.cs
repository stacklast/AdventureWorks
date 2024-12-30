namespace AdventureWorks.Application.SalesTerritories.Create;
public sealed record CreateSalesTerritoryRequest(
    int TerritoryId,
    string Name,
    string CountryRegionCode,
    string Group,
    decimal SalesYtd,
    decimal SalesLastYear,
    decimal CostYtd,
    decimal CostLastYear
);
