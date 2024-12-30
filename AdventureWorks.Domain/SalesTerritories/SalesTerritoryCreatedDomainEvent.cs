using AdventureWorks.Shared;

namespace AdventureWorks.Domain.SalesTerritories;
public sealed record SalesTerritoryCreatedDomainEvent(Guid SalesTerritoryRowguid) : IDomainEvent;
