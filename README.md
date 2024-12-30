# AdventureWorks

This directory contains the source code for AdventureWorks API.

### **📃Download the source code**

1. Clone the repository or download the project files:

   ```bash
   git clone <repository-url>
   ```

2. Using SQL Server Restore a DB locally or Using Azure SQL Databases

    - FIle of the DB backup [AdventureWorks2022.bak](https://learn.microsoft.com/en-us/sql/samples/adventureworks-install-configure?view=sql-server-ver16&tabs=ssms) OLTP version.

3. Change the ConnectionStrings into **AdventureWorks.API/appsettings.json**

4. Run Entity Framework Migrations

   - You can run AdventureWorks.API as Start Up Project **AdventureWorks.API/Program.cs** has **ApplyMigrations()**
   - After migrations you can update the DB

```bash
    Package manager: Update-Database
```

```bash
    Command Line: dotnet ef database update
```

### **Project Structure**

```txt
AdventureWorks.sln/
│   ├── AdventureWorks.API/            # Presentation Layer (Start Up)
│   │   ├── Controllers                # Endpoints
│   │
│   ├── AdventureWorks.Domain/         # Domain Model Layer
│   │   └── Entities
│   │
│   ├── AdventureWorks.Application/         # Application Layer (CQRS)
│   │   └── Abstractions                    # Interface Segregation Pinciple
│   │   └── DependencyInjection.cs          # DI Configuration / Mediatr
│   │
│   ├── AdventureWorks.Infrastructure/      # Infrastructure Layer
│   │   └── Migrations                      # EF Core
│   │   └── Persistence                     # DBContext
│   │   └── Repositories                    # Repository Pattern
│   ├── test
│   │   └──AdventureWorks.Application.UnitTests  #xUnit Moq FluentAssertions
├── .editorconfig              # Environmen VS conventions
└── README.md                  # Project docs
```
