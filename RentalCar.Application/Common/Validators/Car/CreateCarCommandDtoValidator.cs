using FluentValidation;
using RentalCar.Application.common.Dto.Cars;

namespace RentalCar.Application.Common.Validators.Car;

public class CreateCarCommandDtoValidator : AbstractValidator<CreateCarCommandDto>
{
    public CreateCarCommandDtoValidator()
    {
        RuleFor(x => x.CarModel)
            .NotEmpty().WithMessage("Car model is required.")
            .Length(1, 100).WithMessage("Car model must be between 1 and 100 characters.");

        RuleFor(x => x.VendorId)
            .NotEmpty().WithMessage("Vendor ID is required.");
        
        RuleFor(x => x.YearProduction)
            .InclusiveBetween(1886, DateTime.Now.Year).WithMessage("Year of production must be between 1886 and the current year.");

        RuleFor(x => x.EngineDisplacement)
            .GreaterThan(0).WithMessage("Engine displacement must be greater than zero.");
    }
}