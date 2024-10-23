using FluentValidation;
using RentalCar.Application.Common.Validators.Car;

namespace RentalCar.Application.Car.Commands;

public class CreateCarCommandValidator : AbstractValidator<CreateCarCommand>
{
    public CreateCarCommandValidator()
    {
        RuleFor(x => x.CreateCarDto)
            .SetValidator(new CreateCarCommandDtoValidator());
    }
}