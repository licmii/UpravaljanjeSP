using FluentValidation;

namespace RentalCar.Application.Rental.Commands;

public class CreateRentalCommandValidator : AbstractValidator<CreateRentalCommand>
{
    public CreateRentalCommandValidator()
    {
        RuleFor(x => x.CreateRentalDto)
            .NotEmpty().WithMessage("This field is required");
    }
}