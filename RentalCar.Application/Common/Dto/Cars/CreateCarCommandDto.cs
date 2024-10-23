namespace RentalCar.Application.common.Dto.Cars;

public record CreateCarCommandDto(string CarModel, string VendorId, int YearProduction, double EngineDisplacement);