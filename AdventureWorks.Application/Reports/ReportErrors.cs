using AdventureWorks.Shared;

namespace AdventureWorks.Application.Reports;
public static class ReportErrors
{
    public static Error NotFound(string spName) => Error.NotFound(
        "Reports.NotFound",
        $"The Report with the name = '{spName}' was not found");

}
