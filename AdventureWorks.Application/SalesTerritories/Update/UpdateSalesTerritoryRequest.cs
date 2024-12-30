namespace AdventureWorks.Application.SalesTerritories.Update;
public sealed record class UpdateSalesTerritoryRequest(
    string Name,
    string CountryRegionCode,
    string Group,
    decimal SalesYtd,
    decimal SalesLastYear,
    decimal CostYtd,
    decimal CostLastYear,
    Guid Rowguid);
