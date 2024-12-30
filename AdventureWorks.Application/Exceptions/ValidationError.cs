namespace AdventureWorks.Application.Exceptions;
public record ValidationError(string Property, string ErrorMessage);
