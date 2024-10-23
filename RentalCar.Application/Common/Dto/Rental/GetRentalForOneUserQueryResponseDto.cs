namespace RentalCar.Application.Common.Dto.Rental;

public record GetRentalForOneUserQueryResponseDto(Domain.Entities.User User, List<Domain.Entities.Car>? RentedCarsForUser);