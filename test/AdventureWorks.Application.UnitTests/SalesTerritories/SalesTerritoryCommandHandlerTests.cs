using AdventureWorks.Application.Abstractions.Data;
using AdventureWorks.Application.SalesTerritories.Create;
using AdventureWorks.Application.SalesTerritories.Remove;
using AdventureWorks.Domain.Entities;
using AdventureWorks.Domain.SalesTerritories;
using AdventureWorks.Shared;
using Moq;

namespace AdventureWorks.Application.UnitTests.SalesTerritories;

public class SalesTerritoryCommandHandlerTests
{
    private readonly Mock<ISalesTerritoryRepository> _mockRepository;
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly Mock<ICreateSalesTerritoryCommandHandler> _mockCreateHandler;
    private readonly Mock<IRemoveSalesTerritoryCommandHandler> _mockRemoveHandler;

    public SalesTerritoryCommandHandlerTests()
    {
        _mockRepository = new Mock<ISalesTerritoryRepository>();
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _mockCreateHandler = new Mock<ICreateSalesTerritoryCommandHandler>();
        _mockRemoveHandler = new Mock<IRemoveSalesTerritoryCommandHandler>();
    }

    [Fact]
    public async Task Handle_CreateSalesTerritoryCommand_ShouldReturnSuccess()
    {
        // Arrange
        CreateSalesTerritoryCommand request = new(
            "Test Territory",
            "US",
            "North America",
            1000000,
            500000,
            300000,
            200000
        );


        _mockRepository.Setup(repo => repo.Add(It.IsAny<SalesTerritory>()));
        _mockUnitOfWork.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);
        _mockCreateHandler.Setup(handler => handler.Handle(request, It.IsAny<CancellationToken>()))
                      .ReturnsAsync(Result.Success(Guid.NewGuid()));
        // Act
        Result<Guid> result = await _mockCreateHandler.Object.Handle(request, default);

        // Assert
        Assert.NotNull(result); // Ensure result is not null
        Assert.True(result.IsSuccess);  // Check if the operation was successful
    }

    [Fact]
    public async Task Handle_RemoveSalesTerritoryCommand_ShouldReturnSuccess()
    {
        // Arrange
        var salesTerritory = new SalesTerritory
        {
            Rowguid = Guid.NewGuid(),
            Name = "Test Territory",
            CountryRegionCode = "US",
            Group = "North America",
            SalesYtd = 1000000,
            SalesLastYear = 500000,
            CostYtd = 300000,
            CostLastYear = 200000
        };

        RemoveSalesTerritoryCommand request = new(salesTerritory.Rowguid);

        _mockRepository
            .Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(salesTerritory);

        _mockRepository
            .Setup(repo => repo.Delete(It.IsAny<Guid>()));

        _mockUnitOfWork
            .Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        _mockRemoveHandler
            .Setup(handler => handler.Handle(It.IsAny<RemoveSalesTerritoryCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success(salesTerritory.Rowguid));

        // Act
        Result<Guid> result = await _mockRemoveHandler.Object.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result); // Ensure result is not null
        Assert.True(result.IsSuccess);
        Assert.Equal(salesTerritory.Rowguid, result.Value);
    }


    [Fact]
    public async Task Handle_RemoveSalesTerritoryCommand_ShouldReturnFailure_WhenTerritoryNotFound()
    {
        // Arrange
        RemoveSalesTerritoryCommand request = new(Guid.NewGuid());

        _mockRepository.Setup(r => r.GetByIdAsync(request.Rowguid, It.IsAny<CancellationToken>()))
                       .ReturnsAsync((SalesTerritory)null);

        _mockRemoveHandler.Setup(handler => handler.Handle(request, It.IsAny<CancellationToken>()))
                          .ReturnsAsync(Result.Failure<Guid>(SalesTerritoryErrors.NotFound(request.Rowguid)));

        Result<Guid> result = await _mockRemoveHandler.Object.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
        Assert.Equal(SalesTerritoryErrors.NotFound(request.Rowguid), result.Error);
    }
}
