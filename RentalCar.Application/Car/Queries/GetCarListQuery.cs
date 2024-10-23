using AutoMapper;
using MediatR;
using MongoDB.Entities;
using RentalCar.Application.common.Dto.Cars;

namespace RentalCar.Application.Car.Queries;

public record GetCarListQuery() : IRequest<CarListDto>;

public class GetCarListQueryHandler : IRequestHandler<GetCarListQuery, CarListDto>
{
    private readonly IMapper _mapper;

    public GetCarListQueryHandler(IMapper mapper)
    {
        _mapper = mapper;
    }
    
    public async Task<CarListDto> Handle(GetCarListQuery request, CancellationToken cancellationToken)
    {
        var cars = await DB.Find<Domain.Entities.Car>().ExecuteAsync(cancellationToken);
        var carDtos = cars.Select(x => _mapper.Map<Task<CarDetailsDto>>(x).Result).ToList();
        return new CarListDto(carDtos);
    }
}