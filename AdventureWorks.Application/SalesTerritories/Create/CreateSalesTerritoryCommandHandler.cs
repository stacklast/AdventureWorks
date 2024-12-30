using AdventureWorks.Application.Abstractions.Data;
using AdventureWorks.Application.Abstractions.Messaging;
using AdventureWorks.Domain.Entities;
using AdventureWorks.Domain.SalesTerritories;
using AdventureWorks.Shared;

namespace AdventureWorks.Application.SalesTerritories.Create;

public interface ICreateSalesTerritoryCommandHandler
    : ICommandHandler<CreateSalesTerritoryCommand, Guid>
{
    new Task<Result<Guid>> Handle(
        CreateSalesTerritoryCommand request,
        CancellationToken cancellationToken);
}
internal sealed class CreateSalesTerritoryCommandHandler
    : SalesTerritoryCommandHandlerBase, ICreateSalesTerritoryCommandHandler, ICommandHandler<CreateSalesTerritoryCommand, Guid>
{
    private readonly ISalesTerritoryRepository _salesTerritoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateSalesTerritoryCommandHandler(
        ISalesTerritoryRepository salesTerritoryRepository,
        IUnitOfWork unitOfWork)
    {
        _salesTerritoryRepository = salesTerritoryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(
        CreateSalesTerritoryCommand request,
        CancellationToken cancellationToken)
    {

        var salesTerritory = new SalesTerritory
        {
            Name = request.Name,
            CountryRegionCode = request.CountryRegionCode,
            Group = request.Group,
            SalesYtd = request.SalesYtd,
            SalesLastYear = request.SalesLastYear,
            CostYtd = request.CostYtd,
            CostLastYear = request.CostLastYear
        };

        _salesTerritoryRepository.Add(salesTerritory);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await InsertSalesTerritoryHistory(salesTerritory, _salesTerritoryRepository, _unitOfWork, cancellationToken);

        return Result.Success(salesTerritory.Rowguid);
    }
}
