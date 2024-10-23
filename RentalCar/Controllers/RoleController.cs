using Microsoft.AspNetCore.Mvc;
using RentalCar.Application.Role.Commands;

namespace RentalCar.Controllers;

[Route("roles")]
public class RoleController : ApiControllerBase
{
    [HttpPost("CreateRole")]
    public async Task<ActionResult> CreateRole(CreateRoleCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }
}