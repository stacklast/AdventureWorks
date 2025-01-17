﻿using AdventureWorks.Application.Abstractions.Data;
using AdventureWorks.Domain.SalesTerritories;
using AdventureWorks.Infrastructure.Persistence;
using AdventureWorks.Infrastructure.Persistence.DbContexts;
using AdventureWorks.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AdventureWorks.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("Database") ??
                                  throw new ArgumentNullException(nameof(configuration));

        services.AddSingleton<IDbConnectionFactory>(_ =>
            new DbConnectionFactory(connectionString));


        services.AddDbContext<AdventureWorks2022Context>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped<ISalesTerritoryRepository, SalesTerritoryRepository>();

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<AdventureWorks2022Context>());

        return services;
    }
}
