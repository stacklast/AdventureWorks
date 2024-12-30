using AdventureWorks.Application.SalesTerritories.Create;
using AdventureWorks.Application.SalesTerritories.GetById;
using Microsoft.AspNetCore.Authorization;
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

    [AllowAnonymous]
    [HttpPost("add")]
    public async Task<IActionResult> Add(
        CreateSalesTerritoryRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateSalesTerritoryCommand(
            request.Name,
            request.CountryRegionCode,
            request.Group,
            request.SalesYtd,
            request.SalesLastYear,
            request.CostYtd,
            request.CostLastYear);

        Shared.Result<Guid> result = await Mediator.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }
}
