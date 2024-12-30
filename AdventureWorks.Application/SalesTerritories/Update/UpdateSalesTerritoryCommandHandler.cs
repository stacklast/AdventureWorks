using AdventureWorks.Application.Abstractions.Data;
using AdventureWorks.Application.Abstractions.Messaging;
using AdventureWorks.Domain.Entities;
using AdventureWorks.Domain.SalesTerritories;
using AdventureWorks.Shared;

namespace AdventureWorks.Application.SalesTerritories.Update;
internal sealed class UpdateSalesTerritoryCommandHandler
    : SalesTerritoryCommandHandlerBase, ICommandHandler<UpdateSalesTerritoryCommand, Guid>
{
    private readonly ISalesTerritoryRepository _salesTerritoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateSalesTerritoryCommandHandler(
        ISalesTerritoryRepository salesTerritoryRepository,
        IUnitOfWork unitOfWork)
    {
        _salesTerritoryRepository = salesTerritoryRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<Guid>> Handle(
        UpdateSalesTerritoryCommand request,
        CancellationToken cancellationToken)
    {
        SalesTerritory salesTerritory = await _salesTerritoryRepository.GetByIdAsync(request.Rowguid, cancellationToken);

        if (salesTerritory == null)
        {
            return Result.Failure<Guid>(SalesTerritoryErrors.NotFound(request.Rowguid));
        }

        salesTerritory.Name = request.Name;
        salesTerritory.CostLastYear = request.CostLastYear;
        salesTerritory.SalesLastYear = request.SalesLastYear;
        salesTerritory.CostYtd = request.CostYtd;
        salesTerritory.CountryRegionCode = request.CountryRegionCode;
        salesTerritory.Group = request.Group;

        _salesTerritoryRepository.Update(salesTerritory);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await InsertSalesTerritoryHistory(salesTerritory, _salesTerritoryRepository, _unitOfWork, cancellationToken);

        return Result.Success(request.Rowguid);
    }
}
