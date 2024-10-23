using MediatR;
using MongoDB.Entities;

namespace RentalCar.Application.Vendor.commands;

public record CreateVendorCommand(string Name) : IRequest;

public class CreateVendorHandler : IRequestHandler<CreateVendorCommand>
{
    public async Task Handle(CreateVendorCommand request, CancellationToken cancellationToken)
    {
        var data = new Domain.Entities.Vendor(request.Name);

        await data.SaveAsync(cancellation: cancellationToken);
    }
}