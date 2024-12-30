using AdventureWorks.Application.Reports.Sales.GetLast10SalesWithCustomerAddress;
using AdventureWorks.Shared;
using Microsoft.AspNetCore.Mvc;

namespace AdventureWorks.API.Controllers.SalesOrders;

[Route("api/reports")]
[ApiController]
public class ReportsController : ApiControllerBase
{
    [HttpGet("last10sales")]
    public async Task<IActionResult> GetLast10SalesWithCustomerAddress(CancellationToken cancellationToken)
    {
        var query = new GetLast10SalesWithCustomerAddressQuery();
        Result<List<SalesOrderWithCustomerAddressResponse>> sales = await Mediator.Send(query, cancellationToken);
        return Ok(sales);
    }
}
