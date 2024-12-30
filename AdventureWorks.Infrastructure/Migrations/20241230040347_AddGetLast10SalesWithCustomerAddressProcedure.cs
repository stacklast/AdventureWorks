using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdventureWorks.Infrastructure.Migrations;

/// <inheritdoc />
public partial class AddGetLast10SalesWithCustomerAddressProcedure : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql(@"
                CREATE PROCEDURE uspGetLast10SalesWithCustomerAddress
                AS
                BEGIN
                SET NOCOUNT ON;

                SELECT TOP 10 
                    soh.SalesOrderID,
                    soh.OrderDate,
                    soh.TotalDue,
                    c.CustomerID,
                    p.FirstName + ' ' + p.LastName AS CustomerName,
                    ea.EmailAddress,
                    a.AddressLine1,
                    a.City,
                    a.StateProvinceID,
                    a.PostalCode
                FROM Sales.SalesOrderHeader AS soh
                INNER JOIN Sales.Customer AS c ON soh.CustomerID = c.CustomerID
                INNER JOIN Person.Person AS p ON c.PersonID = p.BusinessEntityID
                LEFT JOIN Person.EmailAddress AS ea ON p.BusinessEntityID = ea.BusinessEntityID
                INNER JOIN Person.BusinessEntityAddress AS bea ON c.PersonID = bea.BusinessEntityID
                INNER JOIN Person.Address AS a ON bea.AddressID = a.AddressID
                ORDER BY soh.OrderDate DESC;
            END;
            ");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql("DROP PROCEDURE IF EXISTS uspGetLast10SalesWithCustomerAddress");
    }
}
