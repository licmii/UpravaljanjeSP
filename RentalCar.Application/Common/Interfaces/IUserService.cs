using RentalCar.Application.Common.Dto.Users;
using RentalCar.Domain.Entities;

namespace RentalCar.Application.common.Interfaces;

public interface IUserService
{
    Task CreateUserAsync(CreateUserDto createUserDto, List<string> roles);
    //Task<bool> AddUserToRoleAsync(ApplicationUser user, string role);
    Task<ApplicationUser?> GetUserAsync(string id);
    Task<ApplicationUser?> GetUserByEmailAsync(string id);
    Task<bool> IsInRoleAsync(ApplicationUser user, string roleName);
}