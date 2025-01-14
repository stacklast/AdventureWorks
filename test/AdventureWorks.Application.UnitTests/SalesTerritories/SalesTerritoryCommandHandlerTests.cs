using AdventureWorks.Application.Abstractions.Data;
using AdventureWorks.Application.SalesTerritories;
using AdventureWorks.Application.SalesTerritories.Create;
using AdventureWorks.Domain.Entities;
using AdventureWorks.Domain.SalesTerritories;
using AdventureWorks.Shared;
using NSubstitute;

namespace AdventureWorks.Application.UnitTests.SalesTerritories;
public class SalesTerritoryCommandHandlerTests : SalesTerritoryCommandHandlerBase
{
    private readonly ISalesTerritoryRepository _mockRepository;
    private readonly IUnitOfWork _mockUnitOfWork;
    private readonly CreateSalesTerritoryCommandHandler _createHandler;

    private static readonly CreateSalesTerritoryCommand Command = new(
       "Test Territory",
       "US",
       "North America",
       1000000,
       500000,
       300000,
       200000);

    public SalesTerritoryCommandHandlerTests()
    {
        _mockRepository = Substitute.For<ISalesTerritoryRepository>();
        _mockUnitOfWork = Substitute.For<IUnitOfWork>();
        _createHandler = new CreateSalesTerritoryCommandHandler(
            _mockRepository,
            _mockUnitOfWork);
    }

    [Fact]
    public async Task Handle_CreateSalesTerritoryCommand_ShouldReturnSuccess()
    {
        // Arrange
        var salesTerritory = new SalesTerritory
        {
            Name = Command.Name,
            CountryRegionCode = Command.CountryRegionCode,
            Group = Command.Group,
            SalesYtd = Command.SalesYtd,
            SalesLastYear = Command.SalesLastYear,
            CostYtd = Command.CostYtd,
            CostLastYear = Command.CostLastYear
        };
        _mockRepository.Add(salesTerritory);
        await _mockUnitOfWork.SaveChangesAsync();
        await InsertSalesTerritoryHistory(salesTerritory, _mockRepository, _mockUnitOfWork, default);

        // Act
        Result<Guid> result = await _createHandler.Handle(Command, default);

        // Assert
        Assert.NotNull(result); // Ensure result is not null
        Assert.True(result.IsSuccess);  // Check if the operation was successful
    }
}
