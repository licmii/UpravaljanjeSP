using MediatR;
using MongoDB.Driver;
using MongoDB.Entities;
using RentalCar.Application.common.Dto.Cars;

namespace RentalCar.Application.Car.Queries;

public record GetOneCarQuery(string CarId) : IRequest<CarDetailsDto>;

public class GetOneCarQueryHandler : IRequestHandler<GetOneCarQuery, CarDetailsDto>
{
    
    public async Task<CarDetailsDto> Handle(GetOneCarQuery request, CancellationToken cancellationToken)
    {

        var car = await DB.Find<Domain.Entities.Car>().OneAsync(request.CarId, cancellationToken);

        if (car == null)
            throw new Exception("Car doesnt exist");

        return new CarDetailsDto(car.CarModel, car.YearProduction, car.EngineDisplacement, 
            (await car.Vendor.ToEntityAsync(cancellation: cancellationToken))!.Name);
    }
}