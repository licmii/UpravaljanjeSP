using FluentValidation;

namespace RentalCar.Application.Auth.Commands.CompleteLoginCommand;

public class CompleteLoginCommandValidator : AbstractValidator<CompleteLoginCommand>
{
    public CompleteLoginCommandValidator()
    {
        RuleFor(x => x.ValidationToken).NotEmpty();
    }
}