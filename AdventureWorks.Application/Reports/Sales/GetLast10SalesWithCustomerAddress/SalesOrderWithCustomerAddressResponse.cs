namespace AdventureWorks.Application.Reports.Sales.GetLast10SalesWithCustomerAddress;
public sealed class SalesOrderWithCustomerAddressResponse
{
    public int SalesOrderID { get; init; }
    public DateTime OrderDate { get; init; }
    public decimal SubTotal { get; init; }
    public decimal TotalDue { get; init; }
    public int CustomerID { get; init; }
    public string CustomerName { get; set; }
    public string EmailAddress { get; init; }
    public string AddressLine1 { get; init; }
    public string City { get; init; }
    public int StateProvinceID { get; init; }
    public string PostalCode { get; init; }
}
