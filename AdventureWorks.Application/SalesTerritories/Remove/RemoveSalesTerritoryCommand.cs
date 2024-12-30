using AdventureWorks.Application.Abstractions.Messaging;

namespace AdventureWorks.Application.SalesTerritories.Remove;
public sealed record RemoveSalesTerritoryCommand(
    Guid Rowguid) : ICommand<Guid>;
