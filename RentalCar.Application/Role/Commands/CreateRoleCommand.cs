using MediatR;
using MongoDB.Entities;

namespace RentalCar.Application.Role.Commands;

public record CreateRoleCommand(string RoleName) : IRequest;

public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand>
{
    public async Task Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var roleExists = await DB.Find<Domain.Entities.Role>()
            .Match(x => x.RoleName.Equals("customer"))
            .ExecuteSingleAsync(cancellationToken);

        if (roleExists != null)
            throw new Exception("This role already exists");

        var role = new Domain.Entities.Role
        {
            RoleName = request.RoleName
        };

        await role.SaveAsync(cancellation: cancellationToken);
    }
} 