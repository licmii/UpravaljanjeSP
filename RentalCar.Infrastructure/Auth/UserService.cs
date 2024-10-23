using System.Security.Authentication;
using System.Security.Claims;
using AspNetCore.Identity.MongoDbCore.Models;
using Microsoft.AspNetCore.Identity;
using RentalCar.Application.Common.Dto.Users;
using RentalCar.Application.common.Interfaces;
using RentalCar.Domain.Entities;
using RentalCar.Infrastructure.Exceptions;

namespace RentalCar.Infrastructure.Auth;

public class UserService : IUserService
{

    private readonly ApplicationUserManager _userManager;
    //private readonly ApplicationRoleManager _roleManager;

    public UserService(ApplicationUserManager userManager)
    {
        _userManager = userManager;
        //_roleManager = roleManager;
    }

    public async Task CreateUserAsync(CreateUserDto userDto, List<string> roles)
    {

        var alreadyExists = await _userManager.FindByEmailAsync(userDto.Email);

        if (alreadyExists != null)
            return;

        var user = new ApplicationUser
        {
            FirstName = userDto.Firstname,
            LastName = userDto.LastName,
            Email = userDto.Email,
            UserName = userDto.Email,
            YearOfBirth = userDto.YearOfBirth
        };

        try
        {
            
            user.Claims.Add(new MongoClaim{Type = ClaimTypes.Email, Value = userDto.Email});
            user.Claims.AddRange(roles.Select(userRole => new MongoClaim
            {
                Type = ClaimTypes.Role, Value = userRole
            }));

            var result = await _userManager.CreateAsync(user);

            if (!result.Succeeded)
            {
                throw new AuthException("Could now create a new user", new {Errors = result.Errors.ToList()});
            }

            var rolesResult = await _userManager.AddToRolesAsync(user, roles.Select(nr => nr.ToUpper()));

            if (!rolesResult.Succeeded)
            {
                await _userManager.DeleteAsync(user);
                
                throw new AuthException("Could not add roles to user",
                    new { Errors = rolesResult.Errors.ToList() });
            }

        }
        catch (Exception e)
        {
            await _userManager.DeleteAsync(user);
            throw new AuthException("could not create a new user", e);
        }
    }

    /*public async Task<bool> AddUserToRoleAsync(ApplicationUser user, string roleName)
    {
        var roleExists = await _roleManager.RoleExistsAsync(roleName.ToUpper());

        if (!roleExists)
        {
            var result = await _roleManager.CreateAsync(new IdentityRole(roleName.ToUpper()));
            if (!result.Succeeded)
            {
                return false;
            }
        }

        var addToRoleResult = await _userManager.AddToRoleAsync(user, roleName.ToUpper());
        if (!addToRoleResult.Succeeded)
        {
            return false;
        }

        return true; 
    }*/

    public Task<ApplicationUser?> GetUserAsync(string id)
    {
        return _userManager.FindByIdAsync(id);
    }

    public Task<ApplicationUser?> GetUserByEmailAsync(string id)
    {
        return _userManager.FindByEmailAsync(id);
    }

    public Task<bool> IsInRoleAsync(ApplicationUser user, string roleName)
    {
        return _userManager.IsInRoleAsync(user, roleName);
    }
}