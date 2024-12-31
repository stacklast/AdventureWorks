# AdventureWorks

This directory contains the source code for AdventureWorks API.

### **📃Download the source code**

1. Clone the repository or download the project files:

   ```bash
   git clone <repository-url>
   ```
---
2. Using SQL Server Restore a DB locally or Using Azure SQL Databases

    - FIle of the DB backup [AdventureWorks2022.bak](https://learn.microsoft.com/en-us/sql/samples/adventureworks-install-configure?view=sql-server-ver16&tabs=ssms) OLTP version.
---    

3. Change the ConnectionStrings into **AdventureWorks.API/appsettings.json**
---

4. Run Entity Framework Migrations

   - You can run AdventureWorks.API as Start Up Project **AdventureWorks.API/Program.cs** has **ApplyMigrations()**
   - After migrations you can update the DB

```bash
    Package Manager Console: Update-Database
```

```bash
    Command Line: dotnet ef database update
```

---

5. Steps to handle new migrations
    1. **Using Package Manager Console (PMC)**
        - **Open PMC**: In Visual Studio, go to **Tools > NuGet Package Manager > Package Manager Console**.
        - **Select the Project**: Ensure that the Default Project dropdown in the PMC is set to the project containing your DbContext in this case **AdventureWorks.Infrastructure**
        - Run the Migration Command:

            ```bash
            Add-Migration MigrationName
            ```

        - Review the Generated Migration under  **AdventureWorks.Infrastructure/Migrations**
        - Apply the Migration

            ```bash
                Update-Database
            ```

    2. Using Command line
        - **Open a Terminal or Command Prompt:** Navigate to the root folder of the project.
            - Ensure the .NET CLI tools are installed and accessible. Use **dotnet --version** to confirm
        - Run the Migration Command:

            ```bash
                dotnet ef migrations add MigrationName
            ```

        - Review the Generated Migration under  **AdventureWorks.Infrastructure/Migrations**
        - Apply the Migration

            ```bash
                dotnet ef database update
            ```

    3. Review the **__EFMigrationsHistory** table into the DB and check the changes.

### **Project Structure**

```txt
AdventureWorks.sln/
│   ├── AdventureWorks.API/            # Presentation Layer (Start Up)
│   │   ├── Controllers                # Endpoints
│   │   ├── Middleware                 # Middlewares: Handler global Exceptions 
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
├── .editorconfig              # Environment VS conventions
└── README.md                  # Project docs
```
