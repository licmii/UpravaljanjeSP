using MediatR;
using MongoDB.Entities;
using RentalCar.Application.Common.Constants;
using RentalCar.Application.Common.Dto.Users;
using RentalCar.Application.common.Interfaces;
using RentalCar.Domain.Entities;

namespace RentalCar.Application.User.Commands;

public record CreateUserCommand(CreateUserDto User) : IRequest;

public class CreateUserCommandHandler(IUserService userService) : IRequestHandler<CreateUserCommand>
{
    public async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var userAlreadyExists = await DB.Find<Domain.Entities.User>()
            .OneAsync(request.User.Email, cancellationToken);

        if (userAlreadyExists != null)
            throw new Exception("This user already exists");

        var user = new Domain.Entities.User
        {
            FirstName = request.User.Firstname,
            LastName = request.User.LastName,
            Email = request.User.Email,
            YearOfBirth = request.User.YearOfBirth,
        };

        var role = await DB.Find<Domain.Entities.Role>()
            .Match(x => x.RoleName.Equals("customer"))
            .ExecuteSingleAsync(cancellationToken);

        if (role == null)
        {
            throw new Exception("This role doesnt exist");
        }

        await user.SaveAsync(cancellation: cancellationToken);
        await role.Users.AddAsync(user, cancellation: cancellationToken);

        /*await userService.CreateUserAsync(request.User,
            new List<string> { RentalCarAuthorization.Customer });*/
    }
} 