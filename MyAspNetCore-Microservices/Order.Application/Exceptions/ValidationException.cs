using FluentValidation.Results;

namespace Order.Application.Exceptions;

public class ValidationException : ApplicationException
{
    public ValidationException()
        : base("One or more validation failures have occured.")
    {
        Errors = new Dictionary<string, string[]>();
    }

    public ValidationException(IEnumerable<ValidationFailure> failures)
    {
        Errors = failures.GroupBy(g => g.PropertyName, e => e.ErrorMessage)
            .ToDictionary(f => f.Key, f => f.ToArray());
    }
    
    
    public IDictionary<string, string[]> Errors { get; }
}