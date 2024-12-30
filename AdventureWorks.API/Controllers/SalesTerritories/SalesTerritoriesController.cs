using AdventureWorks.Application.SalesTerritories.Create;
using AdventureWorks.Application.SalesTerritories.GetById;
using AdventureWorks.Application.SalesTerritories.Remove;
using AdventureWorks.Application.SalesTerritories.Update;
using AdventureWorks.Shared;
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
        Result<SalesTerritoryResponse> result = await Mediator.Send(query, cancellationToken);

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

        Result<Guid> result = await Mediator.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }

    [AllowAnonymous]
    [HttpPut("update")]
    public async Task<IActionResult> Update(
        UpdateSalesTerritoryRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateSalesTerritoryCommand(
            request.Name,
            request.CountryRegionCode,
            request.Group,
            request.SalesYtd,
            request.SalesLastYear,
            request.CostYtd,
            request.CostLastYear,
            request.Rowguid);

        Result<Guid> result = await Mediator.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }

    [AllowAnonymous]
    [HttpDelete("delete")]
    public async Task<IActionResult> Remove(
        RemoveSalesTerritoryRequest request,
        CancellationToken cancellationToken)
    {
        var command = new RemoveSalesTerritoryCommand(
            request.Rowguid);

        Result<Guid> result = await Mediator.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }
}
