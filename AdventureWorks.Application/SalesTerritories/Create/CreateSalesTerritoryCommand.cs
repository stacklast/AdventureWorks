using AdventureWorks.Application.Abstractions.Messaging;

namespace AdventureWorks.Application.SalesTerritories.Create;

public sealed record CreateSalesTerritoryCommand(
    string Name,
    string CountryRegionCode,
    string Group,
    decimal SalesYtd,
    decimal SalesLastYear,
    decimal CostYtd,
    decimal CostLastYear) : ICommand<Guid>;
