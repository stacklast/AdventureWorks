using AdventureWorks.Application.Abstractions.Messaging;

namespace AdventureWorks.Application.SalesTerritories.GetById;

public sealed record GetSalesTerritoryByIdQuery(int territoryId) : IQuery<SalesTerritoryResponse>;
