using FluentValidation;
using RentalCar.Application.Common.Dto.Rental;

namespace RentalCar.Application.Common.Validators.Rental;

public class CreateRentalCommandDtoValidator : AbstractValidator<CreateRentalCommandDto>
{
    public CreateRentalCommandDtoValidator()
    {
        RuleFor(x => x.CarId)
            .NotEmpty().WithMessage("CarId is required.");
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required.");
    }
}