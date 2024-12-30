using AdventureWorks.Application.Abstractions.Data;
using AdventureWorks.Domain.Entities;
using AdventureWorks.Domain.SalesTerritories;

namespace AdventureWorks.Application.SalesTerritories;
internal class SalesTerritoryCommandHandlerBase
{
    protected async Task InsertSalesTerritoryHistory(
        SalesTerritory salesTerritory,
        ISalesTerritoryRepository repository,
        IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
    {
        var salesTerritoryHistory = new SalesTerritoryHistory
        {
            BusinessEntityId = 274, // TODO: FK_SalesTerritoryHistory_SalesPerson_BusinessEntityID
            TerritoryId = salesTerritory.TerritoryId,
            StartDate = DateTime.Now,
            EndDate = null,
            //Rowguid = Guid.NewGuid(), // Generate a new unique identifier
            ModifiedDate = DateTime.Now
        };

        repository.AddHistory(salesTerritoryHistory);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}

