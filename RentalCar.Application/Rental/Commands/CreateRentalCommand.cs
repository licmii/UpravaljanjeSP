using MediatR;
using MongoDB.Entities;
using RentalCar.Application.Common.Dto.Rental;

namespace RentalCar.Application.Rental.Commands;

public record CreateRentalCommand(CreateRentalCommandDto CreateRentalDto) : IRequest;

public class CreateRentalCommandHandler : IRequestHandler<CreateRentalCommand>
{
    public async Task Handle(CreateRentalCommand request, CancellationToken cancellationToken)
    {
        var user = await DB.Find<Domain.Entities.User>()
            .OneAsync(request.CreateRentalDto.UserId, cancellationToken);
        
        if (user == null)
            throw new Exception("User not found");
        
        var car = await DB.Find<Domain.Entities.Car>()
            .OneAsync(request.CreateRentalDto.CarId, cancellationToken);
        
        if (car == null)
            throw new Exception("Car not found");

        var rental = new Domain.Entities.Rental
        {
            CarId = car.ID!,
            Active = true
        };

        await rental.SaveAsync(cancellation: cancellationToken);
        await user.Rentals.AddAsync(rental, cancellation: cancellationToken);
    }
} 