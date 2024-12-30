using AdventureWorks.Application.Abstractions.Messaging;

namespace AdventureWorks.Application.SalesTerritories.Update;

public sealed record UpdateSalesTerritoryCommand(
    string Name,
    string CountryRegionCode,
    string Group,
    decimal SalesYtd,
    decimal SalesLastYear,
    decimal CostYtd,
    decimal CostLastYear,
    Guid Rowguid) : ICommand<Guid>;
