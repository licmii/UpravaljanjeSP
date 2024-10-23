using MediatR;
using MongoDB.Entities;
using RentalCar.Application.Common.Dto.Rental;

namespace RentalCar.Application.Rental.Queries;

public record GetRentalForOneUserQuery(string UserId) : IRequest<GetRentalForOneUserQueryResponseDto>;

public class GetRentalForOneUserQueryHandler : IRequestHandler<GetRentalForOneUserQuery, GetRentalForOneUserQueryResponseDto>
{
    public async Task<GetRentalForOneUserQueryResponseDto> Handle(GetRentalForOneUserQuery request, CancellationToken cancellationToken)
    {
        var user = await DB.Find<Domain.Entities.User>()
            .OneAsync(request.UserId, cancellationToken);

        if (user == null)
            throw new Exception("User not found");
        
        
        var rentalsForUser = new List<Domain.Entities.Rental>();
        
        rentalsForUser = user.Rentals.ToList();
        
        var carIds = rentalsForUser.Select(r => r.CarId).ToList();

        var cars = new List<Domain.Entities.Car>();
        
        foreach (var carId in carIds)
        {
            var car = await DB.Find<Domain.Entities.Car>().OneAsync(carId, cancellationToken);
            
            cars.Add(car);
        }

        return new GetRentalForOneUserQueryResponseDto(user, cars);
    }
} 