using AdventureWorks.Application.Abstractions.Behaviors;
using AdventureWorks.Application.Reports.Sales.GetLast10SalesWithCustomerAddress;
using AdventureWorks.Application.SalesTerritories.Create;
using AdventureWorks.Application.SalesTerritories.Remove;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace AdventureWorks.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            configuration.AddOpenBehavior(typeof(LoggingBehavior<,>));
            configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        services.AddTransient<IDapperWrapper, DapperWrapper>();
        services.AddTransient<IGetLast10SalesWithCustomerAddressQueryHandler, GetLast10SalesWithCustomerAddressQueryHandler>();
        services.AddTransient<ICreateSalesTerritoryCommandHandler, CreateSalesTerritoryCommandHandler>();
        services.AddTransient<IRemoveSalesTerritoryCommandHandler, RemoveSalesTerritoryCommandHandler>();
        services.AddTransient<IRemoveSalesTerritoryCommandHandler, RemoveSalesTerritoryCommandHandler>();

        //used to handle CommandValidator
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        return services;
    }
}
