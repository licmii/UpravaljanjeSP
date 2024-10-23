using FluentValidation;

namespace RentalCar.Application.Car.Queries;

public class GetOneCarQueryValidator : AbstractValidator<GetOneCarQuery>
{
    public GetOneCarQueryValidator()
    {
        RuleFor(x => x.CarId).NotEmpty().WithMessage("Required field");
    }
}