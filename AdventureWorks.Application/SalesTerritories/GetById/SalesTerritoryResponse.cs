namespace AdventureWorks.Application.SalesTerritories.GetById;
public sealed class SalesTerritoryResponse
{
    public int TerritoryId { get; init; }
    public string Name { get; init; }
    public string CountryRegionCode { get; init; }
    public string Group { get; init; }
    public decimal SalesYtd { get; init; }
    public decimal SalesLastYear { get; init; }
    public decimal CostYtd { get; init; }
    public decimal CostLastYear { get; init; }
    public Guid Rowguid { get; init; }
    public DateTime ModifiedDate { get; init; }
}
