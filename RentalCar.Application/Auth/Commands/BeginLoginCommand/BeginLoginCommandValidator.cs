using FluentValidation;

namespace RentalCar.Application.Auth.Commands.BeginLoginCommand;

public class BeginLoginCommandValidator : AbstractValidator<BeginLoginCommand>
{
    public BeginLoginCommandValidator()
    {
        RuleFor(x => x.EmailAddress).EmailAddress();
    }
}