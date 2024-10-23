using AutoMapper;
using RentalCar.Application.common.Dto.Cars;

namespace RentalCar.Application.Mappers.Cars;

public class MappingProfile : Profile
{

    public MappingProfile()
    {
        CreateMap<Domain.Entities.Car, Task<CarDetailsDto>>()
            .ConstructUsing(x => GetCarDetails(x));
        
        CreateMap<List<Domain.Entities.Car>, CarListDto>()
            .ConstructUsing(x => ToCarList(x));
    }

    private static async Task<CarDetailsDto> GetCarDetails(Domain.Entities.Car car)
    {
        return new CarDetailsDto(car.CarModel, car.YearProduction, car.EngineDisplacement, (await car.Vendor.ToEntityAsync())!.Name);
    }
    
    private static CarListDto ToCarList(IEnumerable<Domain.Entities.Car> cars)
    {
        var carDtos = cars.Select(x => GetCarDetails(x).Result).ToList();
        return new CarListDto(carDtos);
    }
}