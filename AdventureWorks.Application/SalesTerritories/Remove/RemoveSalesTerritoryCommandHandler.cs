using AdventureWorks.Application.Abstractions.Data;
using AdventureWorks.Application.Abstractions.Messaging;
using AdventureWorks.Domain.Entities;
using AdventureWorks.Domain.SalesTerritories;
using AdventureWorks.Shared;

namespace AdventureWorks.Application.SalesTerritories.Remove;

public interface IRemoveSalesTerritoryCommandHandler
    : ICommandHandler<RemoveSalesTerritoryCommand, Guid>
{
    new Task<Result<Guid>> Handle(RemoveSalesTerritoryCommand request, CancellationToken cancellationToken);
}

internal sealed class RemoveSalesTerritoryCommandHandler
    : SalesTerritoryCommandHandlerBase, IRemoveSalesTerritoryCommandHandler, ICommandHandler<RemoveSalesTerritoryCommand, Guid>
{
    private readonly ISalesTerritoryRepository _salesTerritoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveSalesTerritoryCommandHandler(ISalesTerritoryRepository salesTerritoryRepository, IUnitOfWork unitOfWork)
    {
        _salesTerritoryRepository = salesTerritoryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(RemoveSalesTerritoryCommand request, CancellationToken cancellationToken)
    {
        SalesTerritory? salesTerritory = await _salesTerritoryRepository.GetByIdAsync(request.Rowguid);

        if (salesTerritory == null)
        {
            return Result.Failure<Guid>(SalesTerritoryErrors.NotFound(request.Rowguid));
        }

        _salesTerritoryRepository.Delete(salesTerritory.Rowguid);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await DeleteSalesTerritoryHistoryAsync(salesTerritory.TerritoryId, _salesTerritoryRepository, _unitOfWork, cancellationToken);

        return Result.Success(request.Rowguid);
    }
}
