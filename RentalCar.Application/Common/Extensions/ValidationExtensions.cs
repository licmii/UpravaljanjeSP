using FluentValidation.Results;

namespace RentalCar.Application.Common.Extensions;

public static class ValidationExtensions
{
    public static IDictionary<string, string[]> ToGroup(this List<ValidationFailure> validationFailures)
        => validationFailures.GroupBy(e => e.PropertyName,
                e => e.ErrorMessage)
            .ToDictionary(failureGroup => failureGroup.Key,
                failureGroup => failureGroup.ToArray());

}
