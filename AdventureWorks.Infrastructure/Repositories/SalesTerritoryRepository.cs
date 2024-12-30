using AdventureWorks.Domain.Entities;
using AdventureWorks.Domain.SalesTerritories;
using AdventureWorks.Infrastructure.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorks.Infrastructure.Repositories;
internal sealed class SalesTerritoryRepository(AdventureWorks2022Context dbContext) : ISalesTerritoryRepository
{
    public void Add(SalesTerritory salesTerritory)
    {
        dbContext.Add(salesTerritory);
    }

    public void Update(SalesTerritory salesTerritory)
    {
        dbContext.Update(salesTerritory);
    }

    public void Delete(Guid rowguid)
    {
        SalesTerritory? record = dbContext.SalesTerritories.FirstOrDefault(u => u.Rowguid == rowguid);

        if (record != null)
        {
            dbContext.Remove(record);
        }
    }

    public Task<SalesTerritory?> GetByIdAsync(Guid rowguid, CancellationToken cancellationToken = default)
    {
        return dbContext.SalesTerritories.FirstOrDefaultAsync(u => u.Rowguid == rowguid, cancellationToken);
    }

    public void AddHistory(SalesTerritoryHistory salesTerritoryHistory)
    {
        dbContext.Add(salesTerritoryHistory);
    }

    public void DeleteHistoryByTerritoryId(int territoryId)
    {
        var records = dbContext.SalesTerritoryHistories
            .Where(u => u.TerritoryId == territoryId)
            .ToList();

        if (records.Count > 0)
        {
            dbContext.RemoveRange(records);
        }
    }
}
