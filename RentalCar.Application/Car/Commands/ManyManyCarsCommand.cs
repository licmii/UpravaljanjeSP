using MediatR;
using MongoDB.Entities;
using RentalCar.Application.common.Dto.Cars;

namespace RentalCar.Application.Car.Commands;

public record ManyManyCarsCommand(CreateCarCommandDto CreateCarDto, List<string> OtherUsersIds) : IRequest;

public class ManyManyCarsCommandHandler : IRequestHandler<ManyManyCarsCommand>
{
    public async Task Handle(ManyManyCarsCommand request, CancellationToken cancellationToken)
    {
        var vendor = await DB.Find<Domain.Entities.Vendor>()
            .OneAsync(request.CreateCarDto.VendorId, cancellationToken);
        
        if (vendor == null)
        {
            throw new Exception("Vendor notfound");
        }

        var users = new List<Domain.Entities.User>();

        foreach (var item in request.OtherUsersIds)
        {
            var user = await DB.Find<Domain.Entities.User>()
                .OneAsync(item, cancellationToken);
            
            if (user == null)
            {
                throw new Exception("User notfound");
            }
            
            users.Add(user);
        }
        var data = new Domain.Entities.Car
        {
            CarModel = request.CreateCarDto.CarModel,
            YearProduction = request.CreateCarDto.YearProduction,
            EngineDisplacement = request.CreateCarDto.EngineDisplacement,
            Vendor = new One<Domain.Entities.Vendor>(vendor)
        };
        
        await data.SaveAsync(cancellation: cancellationToken);
        await data.OtherUsers.AddAsync(users, cancellation: cancellationToken);
        
    }
} 