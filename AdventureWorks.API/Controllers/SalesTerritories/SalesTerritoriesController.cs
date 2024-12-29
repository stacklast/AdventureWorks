using AdventureWorks.Application.SalesTerritories.GetById;
using Microsoft.AspNetCore.Mvc;

namespace AdventureWorks.API.Controllers.SalesTerritories;

[Route("api/salesterritories")]
[ApiController]
public class SalesTerritoriesController : ApiControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetSalesTerritories(int id, CancellationToken cancellationToken)
    {
        var query = new GetSalesTerritoryByIdQuery(id);
        Shared.Result<SalesTerritoryResponse> result = await Mediator.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : NotFound();
    }
}
