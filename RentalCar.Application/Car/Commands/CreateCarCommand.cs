using MediatR;
using MongoDB.Entities;
using RentalCar.Application.common.Dto.Cars;


namespace RentalCar.Application.Car.Commands;

public record CreateCarCommand(CreateCarCommandDto CreateCarDto) : IRequest;

public class CreateCarCommandHandler : IRequestHandler<CreateCarCommand>
{
    public async Task Handle(CreateCarCommand request, CancellationToken cancellationToken)
    {
        
        var vendor = await DB.Find<Domain.Entities.Vendor>()
            .OneAsync(request.CreateCarDto.VendorId, cancellationToken);

        if (vendor == null)
            throw new Exception("Vendor not found");

        var car = new Domain.Entities.Car
        {
            CarModel = request.CreateCarDto.CarModel,
            YearProduction = request.CreateCarDto.YearProduction,
            EngineDisplacement = request.CreateCarDto.EngineDisplacement
        };

        car.Addvendor(new One<Domain.Entities.Vendor>(vendor));

        await car.SaveAsync(cancellation: cancellationToken);
    }
}  