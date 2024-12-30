using AdventureWorks.Application.Abstractions.Data;
using AdventureWorks.Application.Reports;
using AdventureWorks.Application.Reports.Sales.GetLast10SalesWithCustomerAddress;
using FluentAssertions;
using Moq;

namespace AdventureWorks.Application.UnitTests.Reports.Sales;

public class GetLast10OrdersHandlerTests
{
    private readonly Mock<IDbConnectionFactory> _mockConnectionFactory;
    private readonly Mock<IDapperWrapper> _mockDapperWrapper;
    private readonly Mock<IGetLast10SalesWithCustomerAddressQueryHandler> _queryHandler;

    public GetLast10OrdersHandlerTests()
    {
        _mockConnectionFactory = new Mock<IDbConnectionFactory>();
        _mockDapperWrapper = new Mock<IDapperWrapper>();
        _queryHandler = new Mock<IGetLast10SalesWithCustomerAddressQueryHandler>();
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenSalesOrdersExist()
    {
        // Arrange
        var request = new GetLast10SalesWithCustomerAddressQuery();
        var salesOrders = new List<SalesOrderWithCustomerAddressResponse>
        {
            new SalesOrderWithCustomerAddressResponse
            {
                SalesOrderID = 1,
                OrderDate = DateTime.UtcNow,
                TotalDue = 100.0m,
                CustomerID = 1,
                CustomerName = "John Doe",
                EmailAddress = "john.doe@example.com",
                AddressLine1 = "123 Main St",
                City = "Metropolis",
                StateProvinceID = 1,
                PostalCode = "12345"
            }
        };

        // Set up the mock behavior
        _queryHandler
            .Setup(handler => handler.Handle(request, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Shared.Result.Success(salesOrders));

        // Act
        Shared.Result<List<SalesOrderWithCustomerAddressResponse>> result = await _queryHandler.Object.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Single(result.Value);
        Assert.Equal(salesOrders, result.Value);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenNoSalesOrdersExist()
    {
        // Arrange
        var request = new GetLast10SalesWithCustomerAddressQuery();
        var salesOrders = new List<SalesOrderWithCustomerAddressResponse>();

        // Set up the mock behavior
        _queryHandler
            .Setup(handler => handler.Handle(request, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Shared.Result.Failure<List<SalesOrderWithCustomerAddressResponse>>(
                ReportErrors.NotFound("uspGetLast10SalesWithCustomerAddress")));

        // Act
        Shared.Result<List<SalesOrderWithCustomerAddressResponse>> result = await _queryHandler.Object.Handle(request, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        result.Error.Should().Be(ReportErrors.NotFound("uspGetLast10SalesWithCustomerAddress"));
    }
}
