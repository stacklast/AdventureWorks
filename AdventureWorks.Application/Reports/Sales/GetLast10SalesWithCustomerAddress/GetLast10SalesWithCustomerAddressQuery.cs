using AdventureWorks.Application.Abstractions.Messaging;

namespace AdventureWorks.Application.Reports.Sales.GetLast10SalesWithCustomerAddress;
public sealed record GetLast10SalesWithCustomerAddressQuery
    : IQuery<List<SalesOrderWithCustomerAddressResponse>>;
