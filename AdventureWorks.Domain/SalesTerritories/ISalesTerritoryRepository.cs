using AdventureWorks.Domain.Entities;

namespace AdventureWorks.Domain.SalesTerritories;
public interface ISalesTerritoryRepository
{
    Task<SalesTerritory?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    void Add(SalesTerritory salesTerritory);

    void Update(SalesTerritory salesTerritory);

    void Delete(Guid id);

    void AddHistory(SalesTerritoryHistory salesTerritoryHistory);
}
