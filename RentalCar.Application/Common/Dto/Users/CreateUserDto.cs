namespace RentalCar.Application.Common.Dto.Users;

public record CreateUserDto(string Firstname, string LastName, string Email, int YearOfBirth);