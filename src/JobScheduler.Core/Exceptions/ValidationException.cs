namespace JobScheduler.Core.Exceptions;

public class ValidationException(IEnumerable<string> validationErrors) : Exception
{
    public IEnumerable<string> ValidationErrors { get; } = validationErrors;
}